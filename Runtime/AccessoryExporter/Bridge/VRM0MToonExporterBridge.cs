using System.Collections.Generic;
using UnityEngine;
using VGltf.Unity;

namespace ClusterVR.CreatorKit.AccessoryExporter.Bridge
{
    public sealed class VRM0MToonExporterBridge
    {
        const string MToonUtilsShaderName = "VRM/MToon";

        readonly VGltf.Ext.Vrm0.Unity.DefaultMaterialExporterBridge defaultMaterialBridge = new();

        public VGltf.Ext.Vrm0.Types.Material CreateMaterialProp(IExporterContext context, Material mat)
        {
            switch (mat.shader.name)
            {
                case MToonUtilsShaderName:
                    return CreateMaterialPropForMToon(context, mat);
                default:
                    return defaultMaterialBridge.CreateMaterialProp(context, mat);
            }
        }

        VGltf.Ext.Vrm0.Types.Material CreateMaterialPropForMToon(IExporterContext context, Material mat)
        {
            var vrmMat = new VGltf.Ext.Vrm0.Types.Material
            {
                Name = mat.name,
                Shader = MToonUtilsShaderName,
                RenderQueue = mat.renderQueue
            };

            foreach (var keyword in mat.shaderKeywords)
            {
                vrmMat.KeywordMap.Add(keyword, mat.IsKeywordEnabled(keyword));
            }

            foreach (var tag in MToonProps.Tags)
            {
                var v = mat.GetTag(tag, false);
                if (!string.IsNullOrEmpty(v))
                {
                    vrmMat.TagMap.Add(tag, v);
                }
            }

            foreach (var prop in MToonProps.Props)
            {
                switch (prop.Value)
                {
                    case MToonProps.PropKind.Float:
                        {
                            var v = mat.GetFloat(prop.Key);
                            vrmMat.FloatProperties.Add(prop.Key, v);
                            break;
                        }

                    case MToonProps.PropKind.Color:
                        {
                            var v = mat.GetColor(prop.Key);
                            vrmMat.VectorProperties.Add(prop.Key, new[] { v.r, v.g, v.b, v.a });
                            break;
                        }

                    case MToonProps.PropKind.Tex:
                        {
                            var v = mat.GetTexture(prop.Key);
                            if (v == null)
                            {
                                continue;
                            }

                            var vRes = context.Exporters.Textures.Export(v);
                            vrmMat.TextureProperties.Add(prop.Key, vRes.Index);
                            break;
                        }
                }
            }

            return vrmMat;
        }

        static class MToonProps
        {
            public enum PropKind
            {
                Float,
                Color,
                Tex,
            }

            public readonly struct KV
            {
                public KV(string k, PropKind v)
                {
                    Key = k;
                    Value = v;
                }

                public readonly string Key;
                public readonly PropKind Value;
            }

            public static readonly KV[] Props =
            {
                new KV("_Cutoff", PropKind.Float),
                new KV("_Color", PropKind.Color),
                new KV("_ShadeColor", PropKind.Color),
                new KV("_MainTex", PropKind.Tex),
                new KV("_ShadeTexture", PropKind.Tex),
                new KV("_BumpScale", PropKind.Float),
                new KV("_BumpMap", PropKind.Tex), // normal
                new KV("_ReceiveShadowRate", PropKind.Float),
                new KV("_ReceiveShadowTexture", PropKind.Tex),
                new KV("_ShadingGradeRate", PropKind.Float),
                new KV("_ShadingGradeTexture", PropKind.Tex),
                new KV("_ShadeShift", PropKind.Float),
                new KV("_ShadeToony", PropKind.Float),
                new KV("_LightColorAttenuation", PropKind.Float),
                new KV("_IndirectLightIntensity", PropKind.Float),

                new KV("_RimColor", PropKind.Color),
                new KV("_RimTexture", PropKind.Tex),
                new KV("_RimLightingMix", PropKind.Float),
                new KV("_RimFresnelPower", PropKind.Float),
                new KV("_RimLift", PropKind.Float),

                new KV("_SphereAdd", PropKind.Tex),
                new KV("_EmissionColor", PropKind.Color),
                new KV("_EmissionMap", PropKind.Tex),
                new KV("_OutlineWidthTexture", PropKind.Tex),
                new KV("_OutlineWidth", PropKind.Float),
                new KV("_OutlineScaledMaxDistance", PropKind.Float),
                new KV("_OutlineColor", PropKind.Color),
                new KV("_OutlineLightingMix", PropKind.Float),

                new KV("_UvAnimMaskTexture", PropKind.Tex),
                new KV("_UvAnimScrollX", PropKind.Float),
                new KV("_UvAnimScrollY", PropKind.Float),
                new KV("_UvAnimRotation", PropKind.Float),

                new KV("_MToonVersion", PropKind.Float),
                new KV("_DebugMode", PropKind.Float),
                new KV("_BlendMode", PropKind.Float),
                new KV("_OutlineWidthMode", PropKind.Float),
                new KV("_OutlineColorMode", PropKind.Float),
                new KV("_CullMode", PropKind.Float),
                new KV("_OutlineCullMode", PropKind.Float),
                new KV("_SrcBlend", PropKind.Float),
                new KV("_DstBlend", PropKind.Float),
                new KV("_ZWrite", PropKind.Float),
                new KV("_AlphaToMask", PropKind.Float),
            };

            public static readonly List<string> Tags = new()
            {
                "RenderType",
            };
        }
    }
}
