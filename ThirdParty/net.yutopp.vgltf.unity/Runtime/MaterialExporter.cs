//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using VGltf.Ext.KhrMaterialsEmissiveStrength.Types;
using VGltf.Types.Extensions;

namespace VGltf.Unity
{
    public abstract class MaterialExporterHook
    {
        public abstract IndexedResource<Material> Export(IExporterContext context, Material mat);
    }

    public sealed class MaterialExporter : ExporterRefHookable<MaterialExporterHook>
    {
        public sealed class Config
        {
            public string ConvertingNormalTexShaderName;

            public string ConvertingOcclusionTexShaderName;

            public string ConvertingMetallicRoughnessTexShaderName;
        }

        public override IExporterContext Context { get; }

        readonly Shader _convertingNormalTexShader;
        readonly Shader _convertingOcclusionTexShader;
        readonly Shader _convertingMetallicRoughnessTexShader;

        public MaterialExporter(IExporterContext context, Config config)
        {
            Context = context;

            _convertingNormalTexShader = Shader.Find(config.ConvertingNormalTexShaderName);
            _convertingOcclusionTexShader = Shader.Find(config.ConvertingOcclusionTexShaderName);
            _convertingMetallicRoughnessTexShader = Shader.Find(config.ConvertingMetallicRoughnessTexShaderName);
        }

        public IndexedResource<Material> Export(Material mat)
        {
            return Context.Resources.Materials.GetOrCall(mat, () =>
            {
                return ForceExport(mat);
            });
        }

        public IndexedResource<Material> ForceExport(Material mat)
        {
            foreach (var h in Hooks)
            {
                var r = h.Export(Context, mat);
                if (r != null)
                {
                    return r;
                }
            }

            // TODO: Support various shaders
            switch (mat.shader.name)
            {
                case "Unlit/Texture":
                    return ForceExportUnlit(mat);
                default:
                    return ForceExportStandard(mat);
            }
        }

        public IndexedResource<Material> ForceExportUnlit(Material mat)
        {
            var tex = mat.GetTexture("_MainTex");
            IndexedResource<Texture> textureResource = null;
            if (tex != null)
            {
                textureResource = Context.Exporters.Textures.Export(tex);
            }

            var gltfMaterial = new Types.Material
            {
                Name = mat.name,

                PbrMetallicRoughness = new Types.Material.PbrMetallicRoughnessType
                {
                    BaseColorTexture = textureResource != null ? new Types.Material.BaseColorTextureInfoType
                    {
                        Index = textureResource.Index,
                        TexCoord = 0, // NOTE: mesh.primitive must have TEXCOORD_<TexCoord>.
                    } : null, // TODO: fix
                    BaseColorFactor = new float[] { 1.0f, 1.0f, 1.0f, 1.0f }, // while defaultly
                },
            };
            gltfMaterial.AddExtension(
                VGltf.Ext.KhrMaterialsUnlit.Types.KhrMaterialsUnlit.ExtensionName,
                new VGltf.Ext.KhrMaterialsUnlit.Types.KhrMaterialsUnlit { }
                );

            var matIndex = Context.Gltf.AddMaterial(gltfMaterial);
            var resource = Context.Resources.Materials.Add(mat, matIndex, mat.name, mat);

            // Mark an extension as used
            Context.Gltf.AddExtensionUsed(VGltf.Ext.KhrMaterialsUnlit.Types.KhrMaterialsUnlit.ExtensionName);

            return resource;
        }

        public IndexedResource<Material> ForceExportStandard(Material mat)
        {
            // Maybe, Standard shader...
            // TODO: Support various shaders

            mat.TryGetColorOrDefault("_Color", Color.white, out var mainColor);
            var mainTexIndex = ExportTextureIfExist(mat, "_MainTex");

            mat.TryGetFloatOrDefault("_Metallic", 1.0f, out var metallic);
            mat.TryGetFloatOrDefault("_Glossiness", 0.0f, out var smoothness);
            var metallicRoughnessTexIndex = ExportMetallicRoughnessTextureIfExist(mat, "_MetallicGlossMap", metallic, smoothness);

            var roughness = ValueConv.SmoothnessToRoughness(smoothness);
            if (metallicRoughnessTexIndex != null)
            {
                // Values are already baked into metallicRoughnessTexIndex
                metallic = 1.0f;
                roughness = 1.0f;
            }

            var normalMapIndex = ExportNormalTextureIfExist(mat, "_BumpMap");

            var occlusionTexIndex = ExportOcclusionTextureIfExist(mat, "_OcclusionMap");
            mat.TryGetFloatOrDefault("_OcclusionStrength", 1.0f, out var occlutionStrength);

            var emissionColorLinear = Vector3.zero; // black
            var emissionTexIndex = default(int?);
            if ((mat.globalIlluminationFlags & MaterialGlobalIlluminationFlags.EmissiveIsBlack) == 0)
            {
                if (mat.TryGetColorOrDefault("_EmissionColor", Color.black, out var emissionColor))
                {
                    emissionColorLinear = ValueConv.ColorToLinearRGB(emissionColor);
                }
                emissionTexIndex = ExportTextureIfExist(mat, "_EmissionMap");
            }

            var alphaMode = GetAlphaMode(mat);
            mat.TryGetFloatOrDefault("_Cutoff", 0.0f, out var alphaCutoff);
            if (alphaMode != Types.Material.AlphaModeEnum.Mask)
            {
                alphaCutoff = 0.0f;
            }

            var gltfMaterial = new Types.Material
            {
                Name = mat.name,

                PbrMetallicRoughness = new Types.Material.PbrMetallicRoughnessType
                {
                    BaseColorFactor = PrimitiveExporter.AsArray(ValueConv.ColorToLinear(mainColor)),
                    BaseColorTexture = mainTexIndex != null ? new Types.Material.BaseColorTextureInfoType
                    {
                        Index = mainTexIndex.Value,
                        TexCoord = 0, // NOTE: mesh.primitive must have TEXCOORD_<TexCoord>.
                    } : null,
                    MetallicFactor = metallic,
                    RoughnessFactor = roughness,
                    MetallicRoughnessTexture = metallicRoughnessTexIndex != null ? new Types.Material.MetallicRoughnessTextureInfoType
                    {
                        Index = metallicRoughnessTexIndex.Value,
                        TexCoord = 0, // NOTE: mesh.primitive must have TEXCOORD_<TexCoord>.
                    } : null,
                },

                NormalTexture = normalMapIndex != null ? new Types.Material.NormalTextureInfoType
                {
                    Index = normalMapIndex.Value,
                    TexCoord = 0, // NOTE: mesh.primitive must have TEXCOORD_<TexCoord>.
                } : null,

                OcclusionTexture = occlusionTexIndex != null ? new Types.Material.OcclusionTextureInfoType
                {
                    Index = occlusionTexIndex.Value,
                    TexCoord = 0, // NOTE: mesh.primitive must have TEXCOORD_<TexCoord>.
                    Strength = occlutionStrength,
                } : null,

                EmissiveFactor = emissionColorLinear != Vector3.zero
                   ? PrimitiveExporter.AsArray(emissionColorLinear)
                   : null,
                EmissiveTexture = emissionTexIndex != null ? new Types.Material.EmissiveTextureInfoType
                {
                    Index = emissionTexIndex.Value,
                    TexCoord = 0, // NOTE: mesh.primitive must have TEXCOORD_<TexCoord>.
                } : null,

                AlphaMode = alphaMode,
                AlphaCutoff = alphaCutoff,

                // DoubleSided = // Not supported
            };

            // emission HDR
            if (Mathf.Max(Mathf.Max(emissionColorLinear.x, emissionColorLinear.y), emissionColorLinear.z) > 1.0f)
            {
                var strength = emissionColorLinear.magnitude;

                var emissiveStrengthExtName = KhrMaterialsEmissiveStrength.ExtensionName;
                gltfMaterial.AddExtension<KhrMaterialsEmissiveStrength>(emissiveStrengthExtName, new KhrMaterialsEmissiveStrength
                {
                    EmissiveStrength = strength,
                });
                Context.Gltf.AddExtensionUsed(emissiveStrengthExtName);

                gltfMaterial.EmissiveFactor = PrimitiveExporter.AsArray(emissionColorLinear.normalized);
            }

            var matIndex = Context.Gltf.AddMaterial(gltfMaterial);
            var resource = Context.Resources.Materials.Add(mat, matIndex, mat.name, mat);

            return resource;
        }

        static Types.Material.AlphaModeEnum GetAlphaMode(Material mat)
        {
            mat.TryGetFloatOrDefault("_Mode", 0, out var modeValue);

            if (modeValue == 0)
            {
                return Types.Material.AlphaModeEnum.Opaque;
            }
            else if (modeValue == 1)
            {
                return Types.Material.AlphaModeEnum.Mask;
            }
            else if (modeValue == 2 || modeValue == 3)
            {
                return Types.Material.AlphaModeEnum.Blend; // TODO: Fade support
            }

            return Types.Material.AlphaModeEnum.Opaque; // fallback
        }

        int? ExportTextureIfExist(Material mat, string name, bool isLinear = false)
        {
            var tex = FindTex(mat, name);
            if (tex == null)
            {
                return null;
            }

            var res = Context.Exporters.Textures.Export(tex, isLinear);
            return res.Index;
        }

        int? ExportNormalTextureIfExist(Material texMat, string name)
        {
            var tex = FindTex(texMat, name);
            if (tex == null)
            {
                return null;
            }

            // NormalMap is not color (= as is (linear))
            // NOTE: If UNITY_NO_DXT5nm is enabled, NO modification is required
            if (GraphicsSettings.HasShaderDefine(BuiltinShaderDefine.UNITY_NO_DXT5nm))
            {
                return Context.Exporters.Textures.RawExport(tex, true);
            }
            else
            {
                using (var mat = new Utils.DestroyOnDispose<Material>(new Material(_convertingNormalTexShader)))
                {
                    return Context.Exporters.Textures.RawExport(tex, true, mat.Value);
                }
            }
        }

        int? ExportOcclusionTextureIfExist(Material texMat, string name)
        {
            var tex = FindTex(texMat, name);
            if (tex == null)
            {
                return null;
            }

            // OcclusionMap is sRGB
            using (var mat = new Utils.DestroyOnDispose<Material>(new Material(_convertingOcclusionTexShader)))
            {
                return Context.Exporters.Textures.RawExport(tex, false, mat.Value);
            }
        }

        static readonly int MetallicProp = Shader.PropertyToID("_Metallic");
        static readonly int SmoothnessProp = Shader.PropertyToID("_Smoothness");

        int? ExportMetallicRoughnessTextureIfExist(Material texMat, string name, float metallic, float smoothness)
        {
            var tex = FindTex(texMat, name);
            if (tex == null)
            {
                return null;
            }

            // Linear
            using (var mat = new Utils.DestroyOnDispose<Material>(new Material(_convertingMetallicRoughnessTexShader)))
            {
                mat.Value.SetFloat(MetallicProp, metallic);
                mat.Value.SetFloat(SmoothnessProp, smoothness);

                return Context.Exporters.Textures.RawExport(tex, true, mat.Value);
            }
        }

        static Texture FindTex(Material mat, string name)
        {
            if (!mat.HasProperty(name))
            {
                return null;
            }

            var tex = mat.GetTexture(name);
            return tex;
        }
    }
}
