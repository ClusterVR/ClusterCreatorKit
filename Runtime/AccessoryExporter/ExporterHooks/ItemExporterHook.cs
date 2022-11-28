using System;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.ItemExporter.Utils;
using ClusterVR.CreatorKit.Proto;
using Google.Protobuf;
using UnityEngine;
using VGltf.Types.Extensions;
using VGltf.Unity;
using AttachCaseToAvatar = ClusterVR.CreatorKit.Proto.AttachCaseToAvatar;
using OffsetTransform = ClusterVR.CreatorKit.Proto.OffsetTransform;

namespace ClusterVR.CreatorKit.AccessoryExporter.ExporterHooks
{
    public sealed class ItemExporterHook : ExporterHookBase
    {
        const string LangCodeJa = "ja";

        public override void PostHook(Exporter exporter, GameObject go)
        {
            var item = go.GetComponent<IItem>();
            if (item == null)
            {
                return;
            }
            ValidateItemComponentContract(go);

            var proto = new Proto.Item
            {
                Meta = new ItemMeta
                {
                    Name =
                    {
                        new[]
                        {
                            new LocalizedText
                            {
                                LangCode = LangCodeJa,
                                Text = item.ItemName
                            }
                        }
                    },
                },
                AccessoryItem = ExtractAccessoryItemProto(exporter, go)
            };

            var extension = new GltfExtensions.ClusterItem
            {
                Item = proto.ToByteString().ToSafeBase64()
            };

            var gltf = exporter.Context.Gltf;
            gltf.AddExtension(GltfExtensions.ClusterItem.ExtensionName, extension);
            gltf.AddExtensionUsed(GltfExtensions.ClusterItem.ExtensionName);
        }

        void ValidateItemComponentContract(GameObject go)
        {
            var movableItemComponent = go.GetComponent<IMovableItem>();
            var ridableItemComponent = go.GetComponent<IRidableItem>();
            var grabbableItemComponent = go.GetComponent<IGrabbableItem>();
            var scriptableItemComponent = go.GetComponent<IScriptableItem>();

            if (movableItemComponent != null || ridableItemComponent != null || grabbableItemComponent != null || scriptableItemComponent != null)
            {
                throw new Exception("can not contains multiple Item components");
            }
        }

        AccessoryItem ExtractAccessoryItemProto(Exporter exporter, GameObject go)
        {
            var accessoryItem = go.GetComponent<IAccessoryItem>();
            if (accessoryItem == null)
            {
                return null;
            }

            var position = exporter.Context.CoordUtils.ConvertSpace(accessoryItem.DefaultAttachOffsetPosition);
            var rotation = exporter.Context.CoordUtils.ConvertSpace(accessoryItem.DefaultAttachOffsetRotation);
            var scale = UnityEngine.Vector3.one;
            return new AccessoryItem
            {
                DefaultOffsetTransform = new OffsetTransform
                {
                    TranslationRotationScale =
                    {
                        position.x,
                        position.y,
                        position.z,
                        rotation.x,
                        rotation.y,
                        rotation.z,
                        rotation.w,
                        scale.x,
                        scale.y,
                        scale.z
                    }
                },
                AttachCaseToAvatar = new AttachCaseToAvatar
                {
                    DefaultHumanBodyBoneName = accessoryItem.DefaultAttachBoneName.ToString(),
                }
            };
        }
    }
}
