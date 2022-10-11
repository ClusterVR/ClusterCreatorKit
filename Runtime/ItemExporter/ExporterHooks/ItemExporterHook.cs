using System;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.ItemExporter.Utils;
using ClusterVR.CreatorKit.Proto;
using Google.Protobuf;
using Google.Protobuf.Collections;
using UnityEngine;
using VGltf.Types.Extensions;
using VGltf.Unity;

namespace ClusterVR.CreatorKit.ItemExporter.ExporterHooks
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
                    Size =
                    {
                        new RepeatedField<uint>
                        {
                            (uint) item.Size[0],
                            (uint) item.Size[1],
                            (uint) item.Size[2]
                        }
                    }
                },
                MovableItem = ExtractMovableItemProto(go),
                RidableItem = ExtractRidableItemProto(exporter, go),
                GrabbableItem = ExtractGrabbableItemProto(exporter, go),
                ScriptableItem = ExtractScriptableItemProto(go)
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

            if (ridableItemComponent != null && grabbableItemComponent != null)
            {
                throw new Exception("can not contains multiple ContactableItem components");
            }
            if (movableItemComponent == null && grabbableItemComponent != null)
            {
                throw new MissingComponentException("GrabbableItem require MovableItem");
            }
            if (ridableItemComponent != null || grabbableItemComponent != null)
            {
                var colliderCount = go.GetComponentsInChildren<Collider>().Count(c => !c.isTrigger);
                if (colliderCount == 0)
                {
                    throw new MissingComponentException("GrabbableItem and RidableItem require Collider");
                }
            }
        }

        Proto.MovableItem ExtractMovableItemProto(GameObject go)
        {
            var movableItemComponent = go.GetComponent<IMovableItem>();
            if (movableItemComponent == null)
            {
                return null;
            }
            return new MovableItem();
        }

        Proto.RidableItem ExtractRidableItemProto(Exporter exporter, GameObject go)
        {
            var ridableItemComponent = go.GetComponent<IRidableItem>();
            if (ridableItemComponent == null)
            {
                return null;
            }
            var ridableItemProto = new RidableItem();
            if (TryGetNodeIndex(exporter, ridableItemComponent.Seat, out var seatIndex))
            {
                ridableItemProto.Seat = seatIndex;
            }
            if (TryGetNodeIndex(exporter, ridableItemComponent.ExitTransform, out var exitTransformIndex))
            {
                ridableItemProto.ExitTransform = exitTransformIndex;
                ridableItemProto.HasExitTransform = true;
            }
            if (TryGetNodeIndex(exporter, ridableItemComponent.RightGrip, out var rightGripIndex))
            {
                ridableItemProto.RightGrip = rightGripIndex;
                ridableItemProto.HasRightGrip = true;
            }
            if (TryGetNodeIndex(exporter, ridableItemComponent.LeftGrip, out var leftGripIndex))
            {
                ridableItemProto.LeftGrip = leftGripIndex;
                ridableItemProto.HasLeftGrip = true;
            }
            return ridableItemProto;
        }

        Proto.GrabbableItem ExtractGrabbableItemProto(Exporter exporter, GameObject go)
        {
            var grabbableItemComponent = go.GetComponent<IGrabbableItem>();
            if (grabbableItemComponent == null)
            {
                return null;
            }
            var grabbableItemProto = new GrabbableItem();

            if (TryGetNodeIndex(exporter, grabbableItemComponent.Grip, out var gripIndex))
            {
                grabbableItemProto.Grip = gripIndex;
                grabbableItemProto.HasGrip = true;
            }
            return grabbableItemProto;
        }

        Proto.ScriptableItem ExtractScriptableItemProto(GameObject go)
        {
            var scriptableItemComponent = go.GetComponent<IScriptableItem>();
            if (scriptableItemComponent == null)
            {
                return null;
            }
            return new ScriptableItem
            {
                SourceCode = scriptableItemComponent.GetSourceCode()
            };
        }

        bool TryGetNodeIndex(Exporter exporter, Transform targetTransform, out uint targetIndex)
        {
            targetIndex = 0;
            if (targetTransform == null)
            {
                return false;
            }

            foreach (var indexedResource in exporter.Context.Resources.Nodes.Map(value => value))
            {
                if (indexedResource.Value.transform != targetTransform)
                {
                    continue;
                }
                targetIndex = (uint) indexedResource.Index;
                return true;
            }
            return false;
        }
    }
}
