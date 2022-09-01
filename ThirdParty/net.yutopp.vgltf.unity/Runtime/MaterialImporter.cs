//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using VGltf.Ext.KhrMaterialsEmissiveStrength.Types;
using VGltf.Types.Extensions;

namespace VGltf.Unity
{
    public abstract class MaterialImporterHook
    {
        public abstract Task<IndexedResource<Material>> Import(IImporterContext context, int matIndex, CancellationToken ct);
    }

    public sealed class MaterialImporter : ImporterRefHookable<MaterialImporterHook>
    {
        public sealed class Config
        {
            public MaterialImporterHook StandardMaterialImporter;
        }

        public override IImporterContext Context { get; }
        readonly Config _config;

        public MaterialImporter(IImporterContext context, Config config)
        {
            Context = context;
            _config = config;
        }

        public async Task<IndexedResource<Material>> Import(int matIndex, CancellationToken ct)
        {
            var gltf = Context.Container.Gltf;
            var gltfMat = gltf.Materials[matIndex];

            return await Context.Resources.Materials.GetOrCallAsync(matIndex, async () =>
            {
                return await ForceImport(matIndex, ct);
            });
        }

        public async Task<IndexedResource<Material>> ForceImport(int matIndex, CancellationToken ct)
        {
            foreach (var h in Hooks)
            {
                var r = await h.Import(Context, matIndex, ct);
                if (r != null)
                {
                    return r;
                }
            }

            // Default import
            var standard = _config.StandardMaterialImporter;
            if (standard == null)
            {
                throw new Exception($"Importer for Standard shader is null");
            }

            var res = await standard.Import(Context, matIndex, ct);
            if (res == null)
            {
                throw new Exception($"Failed to import Standard material");
            }

            return res;
        }
    }

    public sealed class BuiltinStandardMaterialImporterHook : MaterialImporterHook
    {
        public sealed class Config
        {
            public string StandardShaderName = "Standard";

            public bool SkipConvertingNormalTex;
            public string ConvertingNormalTexShaderName;
            public bool? CompressNormalTexHighQual;

            public bool SkipConvertingOcclusionTex;
            public string ConvertingOcclusionTexShaderName;
            public bool? CompressOcclusionTexHighQual;

            public bool SkipConvertingMetallicRoughness;
            public string ConvertingMetallicRoughnessTexShaderName;
            public bool? CompressMetallicRoughnessTexHighQual;
        }

        readonly Config _config;

        readonly Shader _convertingNormalTexShader;
        readonly Shader _convertingOcclusionTexShader;
        readonly Shader _convertingMetallicRoughnessTexShader;

        public BuiltinStandardMaterialImporterHook(Config config)
        {
            _config = config;

            _convertingNormalTexShader = Shader.Find(_config.ConvertingNormalTexShaderName);
            _convertingOcclusionTexShader = Shader.Find(_config.ConvertingOcclusionTexShaderName);
            _convertingMetallicRoughnessTexShader = Shader.Find(_config.ConvertingMetallicRoughnessTexShaderName);
        }

        public override async Task<IndexedResource<Material>> Import(IImporterContext context, int matIndex, CancellationToken ct)
        {
            // Default import
            var gltf = context.Container.Gltf;
            var gltfMat = gltf.Materials[matIndex];

            var shader = Shader.Find(_config.StandardShaderName);
            if (shader == null)
            {
                throw new Exception($"Standard shader is not found: Name = {_config.StandardShaderName}");
            }

            var mat = new Material(shader);
            mat.name = gltfMat.Name;

            var resource = context.Resources.Materials.Add(matIndex, matIndex, mat.name, mat);

            await ImportStandardMaterialProps(context, mat, gltfMat, ct);

            return resource;
        }

        public async Task ImportStandardMaterialProps(IImporterContext context, Material mat, Types.Material gltfMat, CancellationToken ct)
        {
            if (gltfMat.DoubleSided)
            {
                // Not supported
            }

            // https://forum.unity.com/threads/standard-material-shader-ignoring-setfloat-property-_mode.344557/
            switch (gltfMat.AlphaMode)
            {
                case Types.Material.AlphaModeEnum.Opaque:
                    mat.SetFloat("_Mode", (float)0);
                    mat.SetOverrideTag("RenderType", "");
                    mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    mat.SetInt("_ZWrite", 1);
                    mat.DisableKeyword("_ALPHATEST_ON");
                    mat.DisableKeyword("_ALPHABLEND_ON");
                    mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    mat.renderQueue = -1;
                    break;

                case Types.Material.AlphaModeEnum.Blend:
                    mat.SetFloat("_Mode", (float)3);
                    mat.SetOverrideTag("RenderType", "Transparent");
                    mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    mat.SetInt("_ZWrite", 0);
                    mat.DisableKeyword("_ALPHATEST_ON");
                    mat.DisableKeyword("_ALPHABLEND_ON");
                    mat.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                    mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                    break;

                case Types.Material.AlphaModeEnum.Mask:
                    mat.SetFloat("_Mode", (float)1);
                    mat.SetOverrideTag("RenderType", "TransparentCutout");
                    mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    mat.SetInt("_ZWrite", 1);
                    mat.EnableKeyword("_ALPHATEST_ON");
                    mat.DisableKeyword("_ALPHABLEND_ON");
                    mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;

                    mat.SetFloat("_Cutoff", gltfMat.AlphaCutoff);
                    break;
            }

            // RGB component
            var emissionColorLinear = PrimitiveImporter.AsVector3(gltfMat.EmissiveFactor);
            if (emissionColorLinear != Vector3.zero) // NOT black
            {
                mat.EnableKeyword("_EMISSION");
                mat.globalIlluminationFlags &= ~MaterialGlobalIlluminationFlags.EmissiveIsBlack;

                // Support HDR
                var emissiveStrengthExtName = KhrMaterialsEmissiveStrength.ExtensionName;
                if (context.Container.Gltf.ContainsExtensionUsed(emissiveStrengthExtName))
                {
                    var reg = context.Container.JsonSchemas;
                    if (gltfMat.TryGetExtension<KhrMaterialsEmissiveStrength>(emissiveStrengthExtName, reg, out var gltfEmissiveStrength))
                    {
                        emissionColorLinear *= gltfEmissiveStrength.EmissiveStrength;
                    }
                }

                mat.SetColor("_EmissionColor", ValueConv.ColorFromLinear(emissionColorLinear));
            }

            if (gltfMat.EmissiveTexture != null)
            {
                var textureResource = await context.Importers.Textures.Import(gltfMat.EmissiveTexture.Index, false, ct);
                mat.SetTexture("_EmissionMap", textureResource.Value);
            }

            if (gltfMat.NormalTexture != null)
            {
                mat.EnableKeyword("_NORMALMAP");

                var texture = await NormalTextureImporter.Import(
                    context,
                    _config.SkipConvertingNormalTex,
                    _convertingNormalTexShader,
                    _config.CompressNormalTexHighQual,
                    gltfMat.NormalTexture.Index,
                    ct);
                mat.SetTexture("_BumpMap", texture);
            }

            if (gltfMat.OcclusionTexture != null)
            {
                var texture = await OcclusionTextureImporter.Import(
                    context,
                    _config.SkipConvertingOcclusionTex,
                    _convertingOcclusionTexShader,
                    _config.CompressOcclusionTexHighQual,
                    gltfMat.OcclusionTexture.Index,
                    ct);
                mat.SetTexture("_OcclusionMap", texture);

                mat.SetFloat("_OcclusionStrength", gltfMat.OcclusionTexture.Strength);
            }

            if (gltfMat.PbrMetallicRoughness != null)
            {
                var pbrMR = gltfMat.PbrMetallicRoughness;

                // baseColorFactor is linear. See: https://github.com/KhronosGroup/glTF/issues/1638
                var baseColor = ValueConv.ColorFromLinear(PrimitiveImporter.AsVector4(pbrMR.BaseColorFactor));
                mat.SetColor("_Color", baseColor);

                if (pbrMR.BaseColorTexture != null)
                {
                    var textureResource = await context.Importers.Textures.Import(pbrMR.BaseColorTexture.Index, false, ct);
                    mat.SetTexture("_MainTex", textureResource.Value);

                    await context.TimeSlicer.Slice(ct);
                }

                if (pbrMR.MetallicRoughnessTexture != null)
                {
                    mat.EnableKeyword("_METALLICGLOSSMAP");

                    var texture = await MetallicRoughnessTextureImporter.Import(
                        context,
                        _config.SkipConvertingMetallicRoughness,
                        _convertingMetallicRoughnessTexShader,
                        _config.CompressMetallicRoughnessTexHighQual,
                        pbrMR.MetallicRoughnessTexture.Index,
                        pbrMR.MetallicFactor,
                        pbrMR.RoughnessFactor,
                        ct
                        );
                    mat.SetTexture("_MetallicGlossMap", texture);

                    // Values are already baked into textures, thus set 1.0 to make no effects.
                    mat.SetFloat("_Metallic", 1.0f);
                    mat.SetFloat("_Glossiness", 1.0f);
                }
                else
                {
                    mat.SetFloat("_Metallic", pbrMR.MetallicFactor);
                    mat.SetFloat("_Glossiness", ValueConv.RoughnessToSmoothness(pbrMR.RoughnessFactor));
                }
            }
        }

        public static class NormalTextureImporter
        {
            struct NormalTexKey
            {
                public int Index;
            }

            public static async Task<Texture2D> Import(
                IImporterContext context,
                bool skipConverting,
                Shader convertingShader,
                bool? compressHighQual,
                int index,
                CancellationToken ct)
            {
                if (skipConverting)
                {
                    var res = await context.Importers.Textures.Import(index, true, ct);
                    await context.TimeSlicer.Slice(ct);

                    return res.Value;
                }

                // NormalMap is not color (= as is (linear))
                // NOTE: If UNITY_NO_DXT5nm is enabled, NO modification is required
                if (GraphicsSettings.HasShaderDefine(BuiltinShaderDefine.UNITY_NO_DXT5nm))
                {
                    var res = await context.Importers.Textures.Import(index, true, ct);
                    await context.TimeSlicer.Slice(ct);

                    return res.Value;
                }
                else
                {
                    var src = await context.Importers.Textures.RawImport(index, true, ct);
                    using (var srcRes = new Utils.DestroyOnDispose<Texture2D>(src))
                    {
                        await context.TimeSlicer.Slice(ct);

                        var texture = await GenerateUnityDXT5nmFromGltfNormal(
                            src,
                            convertingShader,
                            context.ImportingSetting.TextureUpdateMipmaps,
                            context.ImportingSetting.TextureMakeNoLongerReadable,
                            compressHighQual);
                        context.Resources.AuxResources.Add(new NormalTexKey // TODO: support multi-set
                        {
                            Index = index,
                        }, new Utils.DestroyOnDispose<Texture2D>(texture));

                        await context.TimeSlicer.Slice(ct);

                        return texture;
                    }
                }
            }

            public static Task<Texture2D> GenerateUnityDXT5nmFromGltfNormal(
                Texture2D src,
                Shader convertingShader,
                bool updateMipmaps,
                bool makeNoLongerReadable,
                bool? compressHighQual = null)
            {
                var dst = new Texture2D(src.width, src.height, TextureFormat.RGBA32, 0, true);
                try
                {
                    using (var mat = new Utils.DestroyOnDispose<Material>(new Material(convertingShader)))
                    {
                        ImageUtils.BlitTex(src, dst, true, mat.Value);
                    }
                    if (compressHighQual != null)
                    {
                        dst.Compress(compressHighQual.Value);
                    }
                    dst.Apply(updateMipmaps, makeNoLongerReadable);
                }
                catch
                {
                    Utils.Destroy(dst);
                    throw;
                }

                return Task.FromResult(dst);
            }
        }

        public static class OcclusionTextureImporter
        {
            struct OcclusionTexKey
            {
                public int Index;
            }

            public static async Task<Texture2D> Import(
                IImporterContext context,
                bool skipConverting,
                Shader convertingShader,
                bool? compressHighQual,
                int index,
                CancellationToken ct)
            {
                if (skipConverting)
                {
                    var res = await context.Importers.Textures.Import(index, false, ct);
                    await context.TimeSlicer.Slice(ct);

                    return res.Value;
                }

                var src = await context.Importers.Textures.RawImport(index, false, ct);
                using (var srcRes = new Utils.DestroyOnDispose<Texture2D>(src))
                {
                    await context.TimeSlicer.Slice(ct);

                    var texture = await GenerateOcclusionFromGltf(
                        src,
                        convertingShader,
                        context.ImportingSetting.TextureUpdateMipmaps,
                        context.ImportingSetting.TextureMakeNoLongerReadable,
                        compressHighQual);
                    context.Resources.AuxResources.Add(new OcclusionTexKey // TODO: support multi-set
                    {
                        Index = index,
                    }, new Utils.DestroyOnDispose<Texture2D>(texture));

                    await context.TimeSlicer.Slice(ct);

                    return texture;
                }
            }

            public static Task<Texture2D> GenerateOcclusionFromGltf(
                Texture2D src,
                Shader convertingShader,
                bool updateMipmaps,
                bool makeNoLongerReadable,
                bool? compressHighQual = null)
            {
                // OcclusionMap uses G
                var fmt = SystemInfo.SupportsTextureFormat(TextureFormat.RG16) ? TextureFormat.RG16 : TextureFormat.RGBA32;
                var dst = new Texture2D(src.width, src.height, fmt, 0, false);
                try
                {
                    using (var mat = new Utils.DestroyOnDispose<Material>(new Material(convertingShader)))
                    {
                        ImageUtils.BlitTex(src, dst, false, mat.Value);
                    }
                    if (compressHighQual != null)
                    {
                        dst.Compress(compressHighQual.Value);
                    }
                    dst.Apply(updateMipmaps, makeNoLongerReadable);
                }
                catch
                {
                    Utils.Destroy(dst);
                    throw;
                }

                return Task.FromResult(dst);
            }
        }

        public static class MetallicRoughnessTextureImporter
        {
            static readonly int MetallicProp = Shader.PropertyToID("_Metallic");
            static readonly int RoughnessProp = Shader.PropertyToID("_Roughness");

            struct MetallicRoughnessTexKey
            {
                public int Index;
            }

            public static async Task<Texture2D> Import(
                IImporterContext context,
                bool skipConverting,
                Shader convertingShader,
                bool? compressHighQual,
                int index,
                float metallic,
                float roughness,
                CancellationToken ct)
            {
                if (skipConverting)
                {
                    var res = await context.Importers.Textures.Import(index, true, ct);
                    await context.TimeSlicer.Slice(ct);

                    return res.Value;
                }

                var src = await context.Importers.Textures.RawImport(index, true, ct);
                using (var srcRes = new Utils.DestroyOnDispose<Texture2D>(src))
                {
                    await context.TimeSlicer.Slice(ct);

                    var texture = await GenerateGlossMapFromGltfRoughnessMap(
                        src,
                        convertingShader,
                        metallic,
                        roughness,
                        context.ImportingSetting.TextureUpdateMipmaps,
                        context.ImportingSetting.TextureMakeNoLongerReadable,
                        compressHighQual);
                    context.Resources.AuxResources.Add(new MetallicRoughnessTexKey // TODO: support multi-set
                    {
                        Index = index,
                    }, new Utils.DestroyOnDispose<Texture2D>(texture));

                    await context.TimeSlicer.Slice(ct);

                    return texture;
                }
            }

            public static Task<Texture2D> GenerateGlossMapFromGltfRoughnessMap(
                Texture2D src,
                Shader convertingShader,
                float metallic,
                float roughness,
                bool updateMipmaps,
                bool makeNoLongerReadable,
                bool? compressHighQual = null)
            {
                // GlossMap uses R, A
                var dst = new Texture2D(src.width, src.height, TextureFormat.RGBA32, 0, true);
                try
                {
                    using (var mat = new Utils.DestroyOnDispose<Material>(new Material(convertingShader)))
                    {
                        mat.Value.SetFloat(MetallicProp, metallic);
                        mat.Value.SetFloat(RoughnessProp, roughness);

                        ImageUtils.BlitTex(src, dst, true, mat.Value);
                    }
                    if (compressHighQual != null)
                    {
                        dst.Compress(compressHighQual.Value);
                    }
                    dst.Apply(updateMipmaps, makeNoLongerReadable);
                }
                catch
                {
                    Utils.Destroy(dst);
                    throw;
                }

                return Task.FromResult(dst);
            }
        }
    }
}
