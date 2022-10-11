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

namespace VGltf.Unity
{
    public abstract class ImporterHookBase
    {
        public virtual Task PostHook(IImporterContext context, CancellationToken ct)
        {
            return Task.CompletedTask;
        }
    }

    public sealed class Importer : ImporterRefHookable<ImporterHookBase>, IDisposable
    {
        public sealed class Config
        {
            public bool FlipZAxisInsteadOfXAsix = false;

            public bool TextureUpdateMipmaps = true;
            public bool TextureMakeNoLongerReadable = true;

            public bool SkipConvertingNormalTex = false;
            public string ConvertingNormalTexShaderName = "Hidden/VGltfUnity/GltfNormalTexToUnityDXT5nm";
            public bool? CompressNormalTexHighQual;

            public bool SkipConvertingOcclusionTex = false;
            public string ConvertingOcclusionTexShaderName = "Hidden/VGltfUnity/GltfOcclusionTexToUnity";
            public bool? CompressOcclusionTexHighQual;

            public bool SkipConvertingMetallicRoughness = false;
            public string ConvertingMetallicRoughnessTexShaderName = "Hidden/VGltfUnity/GltfRoughnessMapToUnityGlossMap";
            public bool? CompressMetallicRoughnessTexHighQual;
        }

        sealed class InnerContext : IImporterContext
        {
            public GltfContainer Container { get; }
            public ResourcesStore GltfResources { get; }

            public ImporterRuntimeResources Resources { get; }
            public ITimeSlicer TimeSlicer { get; }
            public CoordUtils CoordUtils { get; }
            public ImportingSetting ImportingSetting { get; }

            public ResourceImporters Importers { get; }
            public SamplerApplier SamplerApplier { get; }

            public InnerContext(GltfContainer container, IResourceLoader loader, ITimeSlicer timeSlicer, Config config)
            {
                Container = container;
                GltfResources = new ResourcesStore(container, loader);

                Resources = new ImporterRuntimeResources();
                TimeSlicer = timeSlicer;
                CoordUtils = config.FlipZAxisInsteadOfXAsix ? new CoordUtils(new Vector3(1, 1, -1)) : new CoordUtils();
                ImportingSetting = new ImportingSetting
                {
                    TextureUpdateMipmaps = config.TextureUpdateMipmaps,
                    TextureMakeNoLongerReadable = config.TextureMakeNoLongerReadable,
                };

                // TODO: pass config directly
                var standardMatImporter = new BuiltinStandardMaterialImporterHook(new BuiltinStandardMaterialImporterHook.Config
                {
                    SkipConvertingNormalTex = config.SkipConvertingNormalTex,
                    ConvertingNormalTexShaderName = config.ConvertingNormalTexShaderName,
                    CompressNormalTexHighQual = config.CompressNormalTexHighQual,

                    SkipConvertingOcclusionTex = config.SkipConvertingOcclusionTex,
                    ConvertingOcclusionTexShaderName = config.ConvertingOcclusionTexShaderName,
                    CompressOcclusionTexHighQual = config.CompressOcclusionTexHighQual,

                    SkipConvertingMetallicRoughness = config.SkipConvertingMetallicRoughness,
                    ConvertingMetallicRoughnessTexShaderName = config.ConvertingMetallicRoughnessTexShaderName,
                    CompressMetallicRoughnessTexHighQual = config.CompressMetallicRoughnessTexHighQual,
                });
                var materialImporterConfig = new MaterialImporter.Config
                {
                    StandardMaterialImporter = standardMatImporter,
                };

                Importers = new ResourceImporters
                {
                    Nodes = new NodeImporter(this),
                    Meshes = new MeshImporter(this),
                    Materials = new MaterialImporter(this, materialImporterConfig),
                    Textures = new TextureImporter(this),
                    Images = new ImageImporter(this),
                };

                SamplerApplier = new SamplerApplier(this);
            }

            public void Dispose()
            {
                Resources.Dispose();
            }

            // helper functions

            public void SetRendererEnebled(bool value)
            {
                foreach (var go in Resources.Nodes.Map(r => r.Value))
                {
                    var r = go.GetComponent<MeshRenderer>();
                    if (r != null)
                    {
                        r.enabled = value;
                    }

                    var smr = go.GetComponent<SkinnedMeshRenderer>();
                    if (smr != null)
                    {
                        smr.enabled = value;
                    }
                }
            }
        }

        InnerContext _context;

        public override IImporterContext Context { get => _context; }

        public Importer(GltfContainer container, IResourceLoader loader, ITimeSlicer timeSlicer, Config config = null)
        {
            if (config == null)
            {
                config = new Config();
            }

            _context = new InnerContext(container, loader, timeSlicer, config);
        }

        public Importer(GltfContainer container, ITimeSlicer timeSlicer, Config config = null)
            : this(container, new ResourceLoaderFromEmbedOnly(), timeSlicer, config)
        {
        }

        public async Task<IImporterContext> ImportSceneNodes(CancellationToken ct)
        {
            var gltf = Context.Container.Gltf;
            var gltfScene = VGltf.Types.Extensions.GltfExtensions.GetSceneObject(gltf);

            foreach (var nodeIndex in gltfScene.Nodes)
            {
                await Context.Importers.Nodes.ImportGameObjects(nodeIndex, null, ct);
                await _context.TimeSlicer.Slice(ct);
            }
            foreach (var nodeIndex in gltfScene.Nodes)
            {
                await Context.Importers.Nodes.ImportMeshesAndSkins(nodeIndex, ct);
                await _context.TimeSlicer.Slice(ct);
            }

            foreach (var hook in Hooks)
            {
                await hook.PostHook(Context, ct);
                await _context.TimeSlicer.Slice(ct);
            }

            _context.SetRendererEnebled(true);

            return TakeContext();
        }

        // Take ownership of Context from importer.
        public IImporterContext TakeContext()
        {
            var ctx = _context;
            _context = null;

            return ctx;
        }

        void IDisposable.Dispose()
        {
            _context?.Dispose();
            _context = null;
        }
    }
}
