using ClusterVR.CreatorKit.ItemExporter.Utils;
using ClusterVR.CreatorKit.Proto;
using Google.Protobuf;
using UnityEngine;
using VGltf.Types.Extensions;
using VGltf.Unity;

namespace ClusterVR.CreatorKit.AccessoryExporter.ExporterHooks
{
    public class ItemNodeExporterHook : NodeExporterHook
    {
        public override void PostHook(NodeExporter exporter, GameObject go, VGltf.Types.Node gltfNode)
        {
            var proto = new ItemNode
            {
                Disabled = !go.activeSelf
            };

            var extension = new GltfExtensions.ClusterItemNode
            {
                ItemNode = proto.ToByteString().ToSafeBase64()
            };

            exporter.Context.Gltf.AddExtensionUsed(GltfExtensions.ClusterItemNode.ExtensionName);
            gltfNode.AddExtension(GltfExtensions.ClusterItemNode.ExtensionName, extension);
        }
    }
}
