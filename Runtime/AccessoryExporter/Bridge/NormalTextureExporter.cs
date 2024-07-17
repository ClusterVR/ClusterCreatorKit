using UnityEngine;
using UnityEngine.Rendering;
using VGltf.Unity;

namespace ClusterVR.CreatorKit.AccessoryExporter.Bridge
{
    public sealed class NormalTextureExporter
    {
        readonly IExporterContext context;
        readonly string convertingNormalTexShaderName;

        public NormalTextureExporter(IExporterContext context, string convertingNormalTexShaderName)
        {
            this.context = context;
            this.convertingNormalTexShaderName = convertingNormalTexShaderName;
        }

        public IndexedResource<Texture> Export(Texture tex)
        {
            return context.Resources.Textures.GetOrCall(tex, () =>
            {
                var texIndex = RawExport(tex);

                var res = context.Resources.Textures.Add(tex, texIndex, tex.name, tex);
                return res;
            });
        }

        int RawExport(Texture tex)
        {
            if (GraphicsSettings.HasShaderDefine(BuiltinShaderDefine.UNITY_NO_DXT5nm))
            {
                return context.Exporters.Textures.RawExport(tex, true);
            }
            else
            {
                var shader = Shader.Find(convertingNormalTexShaderName);
                using (var mat = new Utils.DestroyOnDispose<Material>(new Material(shader)))
                {
                    return context.Exporters.Textures.RawExport(tex, true, mat.Value);
                }
            }
        }
    }
}
