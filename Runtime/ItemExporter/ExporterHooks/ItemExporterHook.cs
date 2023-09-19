using System;
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.ItemExporter.Utils;
using ClusterVR.CreatorKit.Proto;
using Google.Protobuf;
using Google.Protobuf.Collections;
using UnityEngine;
using VGltf.Types.Extensions;
using VGltf.Unity;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ClusterVR.CreatorKit.ItemExporter.ExporterHooks
{
    public sealed class ItemExporterHook : ExporterHookBase
    {
        const string LangCodeJa = "ja";

        readonly bool useDynamic;

        public ItemExporterHook(bool useDynamic)
        {
            this.useDynamic = useDynamic;
        }

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
                ScriptableItem = ExtractScriptableItemProto(go),
                ItemAudioSetList = { ExtractItemAudioSetListProto(go) },
                HumanoidAnimationList = { ExportAndExtractHumanoidAnimations(exporter, go) },
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
                var wouldHavePhysicalShape = go.GetComponentInChildren<Item.Implements.PhysicalShape>(true) != null ||
                    go.GetComponentsInChildren<Collider>(true).Any(c => !c.isTrigger && !c.TryGetComponent<IShape>(out _));
                var haveInteractableShape = go.GetComponentInChildren<Item.Implements.InteractableShape>(true) != null;
                if (!wouldHavePhysicalShape && !haveInteractableShape)
                {
                    throw new MissingComponentException(
                        "GrabbableItem and RidableItem require PhysicalShape or InteractableShape");
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
            return new MovableItem
            {
                IsDynamic = useDynamic && movableItemComponent.IsDynamic,
                Mass = movableItemComponent.Mass,
            };
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
                SourceCode = scriptableItemComponent.GetSourceCode(true)
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
                targetIndex = (uint)indexedResource.Index;
                return true;
            }
            return false;
        }

        IEnumerable<Proto.ItemAudioSet> ExtractItemAudioSetListProto(GameObject go)
        {
            var itemAudioSetList = go.GetComponent<IItemAudioSetList>();
            if (itemAudioSetList == null)
            {
                return Enumerable.Empty<Proto.ItemAudioSet>();
            }
            if (go.GetComponent<IScriptableItem>() == null)
            {
                return Enumerable.Empty<Proto.ItemAudioSet>();
            }

            return itemAudioSetList.ItemAudioSets.Select(Convert);
        }

        static Proto.ItemAudioSet Convert(Item.ItemAudioSet source)
        {
            var pcm = ExtractPcm(source.AudioClip, source.Id);
            return new Proto.ItemAudioSet
            {
                Id = source.Id,
                Audio = new Audio { Pcm = pcm },
                Loop = source.Loop
            };
        }

        static Pcm ExtractPcm(AudioClip audioClip, string id)
        {
            if (audioClip == null)
            {
                throw new MissingAudioClipException(id);
            }

            var data = ExtractAudioData(audioClip, id);
            var pcm = new Pcm()
            {
                Channels = (uint) audioClip.channels,
                SampleRate = (uint) audioClip.frequency,
            };
            pcm.Data.AddRange(data);
            return pcm;
        }

        static float[] ExtractAudioData(AudioClip clip, string id)
        {
            if (clip.loadType == AudioClipLoadType.DecompressOnLoad)
            {
                var data = new float[clip.samples * clip.channels];
                if (clip.GetData(data, 0))
                {
                    return data;
                }
                else
                {
                    throw new ExtractAudioDataFailedException(id);
                }
            }
#if UNITY_EDITOR
            var path = AssetDatabase.GetAssetPath(clip);
            if (string.IsNullOrEmpty(path))
            {
                throw new ExtractAudioDataFailedException(id);
            }
            var importer = AssetImporter.GetAtPath(path) as AudioImporter;
            if (importer == null)
            {
                throw new ExtractAudioDataFailedException(id);
            }
            var currentLoadInBackground = importer.loadInBackground;
            importer.loadInBackground = false;
            const string editorPlatform = "Standalone";
            var hasOverrideSampleSettings = importer.ContainsSampleSettingsOverride(editorPlatform);
            var currentSettings = importer.GetOverrideSampleSettings(editorPlatform);
            var newSettings = currentSettings;
            newSettings.loadType = AudioClipLoadType.DecompressOnLoad;
            if (!importer.SetOverrideSampleSettings(editorPlatform, newSettings))
            {
                throw new ExtractAudioDataFailedException(id);
            }
            try
            {
                importer.SaveAndReimport();
                var data = new float[clip.samples * clip.channels];
                if (clip.GetData(data, 0))
                {
                    return data;
                }
                else
                {
                    throw new ExtractAudioDataFailedException(id);
                }
            }
            finally
            {
                importer.loadInBackground = currentLoadInBackground;
                if (hasOverrideSampleSettings)
                {
                    importer.SetOverrideSampleSettings(editorPlatform, currentSettings);
                }
                else
                {
                    importer.ClearSampleSettingOverride(editorPlatform);
                }
                importer.SaveAndReimport();
            }
#else
            throw new ExtractAudioDataFailedException(id);
#endif
        }

        IEnumerable<HumanoidAnimation> ExportAndExtractHumanoidAnimations(Exporter exporter, GameObject go)
        {
            var humanoidAnimationList = go.GetComponent<IHumanoidAnimationList>();
            if (humanoidAnimationList == null || humanoidAnimationList.HumanoidAnimations == null)
            {
                return Enumerable.Empty<HumanoidAnimation>();
            }
            if (go.GetComponent<IScriptableItem>() == null)
            {
                return Enumerable.Empty<HumanoidAnimation>();
            }

            var humanoidAnimations = new HumanoidAnimation[humanoidAnimationList.HumanoidAnimations.Count];
            foreach (var (entry, index) in humanoidAnimationList.HumanoidAnimations.Select((h, i) => (h, i)))
            {
                var animationIndex = HumanoidAnimationExporter.Export(exporter, entry.Id, entry.HumanoidAnimation);
                humanoidAnimations[index] = new HumanoidAnimation { Id = entry.Id, Animation = (uint) animationIndex };
            }
            return humanoidAnimations;
        }
    }
}
