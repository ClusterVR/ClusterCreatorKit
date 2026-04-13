using UnityEngine;
using UnityEngine.Rendering;
using VGltf.Ext.KhrMaterialsEmissiveStrength.Types;
using VGltf.Types.Extensions;
using VGltf.Unity;
using Types = VGltf.Types;
using Vector3 = UnityEngine.Vector3;

namespace ClusterVR.CreatorKit.ItemExporter.ExporterHooks
{
    public sealed class LitMaterialExporterHook : MaterialExporterHook
    {
        readonly Shader _convertingNormalTexShader;
        readonly Shader _convertingOcclusionTexShader;
        readonly Shader _convertingMetallicRoughnessTexShader;

        public LitMaterialExporterHook(Exporter.Config config)
        {
            _convertingNormalTexShader = Shader.Find(config.ConvertingNormalTexShaderName);
            _convertingOcclusionTexShader = Shader.Find(config.ConvertingOcclusionTexShaderName);
            _convertingMetallicRoughnessTexShader = Shader.Find(config.ConvertingMetallicRoughnessTexShaderName);
        }

        public override IndexedResource<Material> Export(IExporterContext context, Material mat)
        {
            if (mat.shader.name != "Universal Render Pipeline/Lit")
            {
                return null;
            }

            mat.TryGetColorOrDefault("_BaseColor", Color.white, out var mainColor);
            var mainTexIndex = ExportTextureIfExist(context, mat, "_BaseMap");

            var metallicRoughnessTexIndex = ExportMetallicRoughnessTextureIfExist(context, mat, "_MetallicGlossMap", "_Smoothness");

            float metallicFactor, roughnessFactor;
            if (metallicRoughnessTexIndex != null)
            {
                metallicFactor = 1.0f;
                roughnessFactor = 1.0f;
            }
            else
            {
                mat.TryGetFloatOrDefault("_Metallic", 1.0f, out metallicFactor);
                mat.TryGetFloatOrDefault("_Smoothness", 0.0f, out var smoothness);
                roughnessFactor = ValueConv.SmoothnessToRoughness(smoothness);
            }

            var normalMapIndex = ExportNormalTextureIfExist(context, mat, "_BumpMap");

            var occlusionTexIndex = ExportOcclusionTextureIfExist(context, mat, "_OcclusionMap");
            mat.TryGetFloatOrDefault("_OcclusionStrength", 1.0f, out var occlutionStrength);

            var emissionColorLinear = Vector3.zero; // black
            var emissionTexIndex = default(int?);
            if ((mat.globalIlluminationFlags & MaterialGlobalIlluminationFlags.EmissiveIsBlack) == 0)
            {
                if (mat.TryGetColorOrDefault("_EmissionColor", Color.black, out var emissionColor))
                {
                    emissionColorLinear = ValueConv.ColorToLinearRGB(emissionColor);
                }
                emissionTexIndex = ExportTextureIfExist(context, mat, "_EmissionMap");
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
                    MetallicFactor = metallicFactor,
                    RoughnessFactor = roughnessFactor,
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

            };

            if (Mathf.Max(Mathf.Max(emissionColorLinear.x, emissionColorLinear.y), emissionColorLinear.z) > 1.0f)
            {
                var strength = emissionColorLinear.magnitude;

                var emissiveStrengthExtName = KhrMaterialsEmissiveStrength.ExtensionName;
                gltfMaterial.AddExtension<KhrMaterialsEmissiveStrength>(emissiveStrengthExtName, new KhrMaterialsEmissiveStrength
                {
                    EmissiveStrength = strength,
                });
                context.Gltf.AddExtensionUsed(emissiveStrengthExtName);

                gltfMaterial.EmissiveFactor = PrimitiveExporter.AsArray(emissionColorLinear.normalized);
            }

            var matIndex = context.Gltf.AddMaterial(gltfMaterial);
            var resource = context.Resources.Materials.Add(mat, matIndex, mat.name, mat);

            return resource;
        }

        static Types.Material.AlphaModeEnum GetAlphaMode(Material mat)
        {
            mat.TryGetFloatOrDefault("_Surface", 0, out var surface);

            if (surface == 0)
            {
                mat.TryGetFloatOrDefault("_AlphaClip", 0, out var alphaClip);
                if (alphaClip == 0)
                {
                    return Types.Material.AlphaModeEnum.Opaque;
                }
                else if (alphaClip == 1)
                {
                    return Types.Material.AlphaModeEnum.Mask;
                }
            }
            else if (surface == 1)
            {
                return Types.Material.AlphaModeEnum.Blend;
            }

            return Types.Material.AlphaModeEnum.Opaque; // fallback
        }

        int? ExportTextureIfExist(IExporterContext context, Material mat, string name, bool isLinear = false)
        {
            var tex = FindTex(mat, name);
            if (tex == null)
            {
                return null;
            }

            var res = context.Exporters.Textures.Export(tex, isLinear);
            return res.Index;
        }

        int? ExportNormalTextureIfExist(IExporterContext context, Material texMat, string name)
        {
            var tex = FindTex(texMat, name);
            if (tex == null)
            {
                return null;
            }

            if (GraphicsSettings.HasShaderDefine(BuiltinShaderDefine.UNITY_NO_DXT5nm))
            {
                return context.Exporters.Textures.RawExport(tex, true);
            }
            else
            {
                using (var mat = new VGltf.Unity.Utils.DestroyOnDispose<Material>(new Material(_convertingNormalTexShader)))
                {
                    return context.Exporters.Textures.RawExport(tex, true, mat.Value);
                }
            }
        }

        int? ExportOcclusionTextureIfExist(IExporterContext context, Material texMat, string name)
        {
            var tex = FindTex(texMat, name);
            if (tex == null)
            {
                return null;
            }

            using (var mat = new VGltf.Unity.Utils.DestroyOnDispose<Material>(new Material(_convertingOcclusionTexShader)))
            {
                return context.Exporters.Textures.RawExport(tex, false, mat.Value);
            }
        }

        static readonly int SmoothnessProp = Shader.PropertyToID("_Smoothness");

        int? ExportMetallicRoughnessTextureIfExist(IExporterContext context, Material texMat, string texturePropertyName, string smoothnessPropertyName)
        {
            var tex = FindTex(texMat, texturePropertyName);
            if (tex == null)
            {
                return null;
            }

            texMat.TryGetFloatOrDefault(smoothnessPropertyName, 0.0f, out var smoothness);

            using (var mat = new VGltf.Unity.Utils.DestroyOnDispose<Material>(new Material(_convertingMetallicRoughnessTexShader)))
            {
                mat.Value.SetFloat(SmoothnessProp, smoothness);

                return context.Exporters.Textures.RawExport(tex, true, mat.Value);
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
