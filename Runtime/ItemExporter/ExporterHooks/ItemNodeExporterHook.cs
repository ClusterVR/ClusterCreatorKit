using System;
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.ItemExporter.Utils;
using ClusterVR.CreatorKit.Proto;
using ClusterVR.CreatorKit.World;
using Google.Protobuf;
using UnityEngine;
using VGltf.Types.Extensions;
using VGltf.Unity;

namespace ClusterVR.CreatorKit.ItemExporter.ExporterHooks
{
    public sealed class ItemNodeExporterHook : NodeExporterHook
    {
        public override void PostHook(NodeExporter exporter, GameObject go, VGltf.Types.Node gltfNode)
        {
            var coordUtils = new CoordUtils();

            var proto = new ItemNode
            {
                PhysicalShapes = { PhysicalShapes(go, coordUtils) },
                OverlapSourceShapes = { OverlapSourceShapes(go, coordUtils) },
                OverlapDetectorShapes = { OverlapDetectorShapes(go, coordUtils) },
                InteractableShapes = { InteractableShapes(go, coordUtils) },
                ItemSelectShapes = { ItemSelectShapes(go, coordUtils) },
                MainScreenView = TryGetMainScreenView(go),
                Disabled = !go.activeSelf,
                Mirror = TryGetMirror(go)
            };

            var extension = new GltfExtensions.ClusterItemNode
            {
                ItemNode = proto.ToByteString().ToSafeBase64()
            };

            exporter.Context.Gltf.AddExtensionUsed(GltfExtensions.ClusterItemNode.ExtensionName);
            gltfNode.AddExtension(GltfExtensions.ClusterItemNode.ExtensionName, extension);
        }

        static IEnumerable<Proto.PhysicalShape> PhysicalShapes(GameObject go, CoordUtils coordUtils)
        {
            var shape = go.GetComponent<IShape>();
            if (shape != null)
            {
                if (shape is IPhysicalShape)
                {
                    return go.GetComponents<Collider>()
                        .Select(c => ToPhysicalShape(c, coordUtils));
                }
                else
                {
                    return Array.Empty<Proto.PhysicalShape>();
                }
            }
            else
            {
                return go.GetComponents<Collider>()
                    .Where(c => !c.isTrigger)
                    .Select(c => ToPhysicalShape(c, coordUtils));
            }
        }

        static Proto.PhysicalShape ToPhysicalShape(Collider collider, CoordUtils coordUtils)
        {
            return new Proto.PhysicalShape { Shape = CreateShapeFrom(collider, coordUtils) };
        }


        static IEnumerable<Proto.OverlapSourceShape> OverlapSourceShapes(GameObject go, CoordUtils coordUtils)
        {
            return go.GetComponents<Collider>()
                .Where(c => c.TryGetComponent<Item.Implements.OverlapSourceShape>(out _))
                .Select(c => ToOverlapSourceShape(c, coordUtils));
        }

        static Proto.OverlapSourceShape ToOverlapSourceShape(Collider collider, CoordUtils coordUtils)
        {
            return new Proto.OverlapSourceShape { Shape = CreateShapeFrom(collider, coordUtils) };
        }

        static IEnumerable<Proto.OverlapDetectorShape> OverlapDetectorShapes(GameObject go, CoordUtils coordUtils)
        {
            return go.GetComponents<Collider>()
                .Where(c => c.TryGetComponent<Item.Implements.OverlapDetectorShape>(out _))
                .Select(c => ToOverlapDetectorShape(c, coordUtils));
        }

        static Proto.OverlapDetectorShape ToOverlapDetectorShape(Collider collider, CoordUtils coordUtils)
        {
            return new Proto.OverlapDetectorShape { Shape = CreateShapeFrom(collider, coordUtils) };
        }

        static IEnumerable<Proto.InteractableShape> InteractableShapes(GameObject go, CoordUtils coordUtils)
        {
            return go.GetComponents<Collider>()
                .Where(c => c.TryGetComponent<Item.Implements.InteractableShape>(out _))
                .Select(c => ToInteractableShape(c, coordUtils));
        }

        static Proto.InteractableShape ToInteractableShape(Collider collider, CoordUtils coordUtils)
        {
            return new Proto.InteractableShape { Shape = CreateShapeFrom(collider, coordUtils) };
        }

        static IEnumerable<Proto.ItemSelectShape> ItemSelectShapes(GameObject go, CoordUtils coordUtils)
        {
            return go.GetComponents<Collider>()
                .Where(c => c.TryGetComponent<Item.Implements.ItemSelectShape>(out _))
                .Select(c => ToItemSelectShape(c, coordUtils));
        }

        static Proto.ItemSelectShape ToItemSelectShape(Collider collider, CoordUtils coordUtils)
        {
            return new Proto.ItemSelectShape { Shape = CreateShapeFrom(collider, coordUtils) };
        }

        static Shape CreateShapeFrom(Collider collider, CoordUtils coordUtils)
        {
            switch (collider)
            {
                case BoxCollider boxCollider:
                    return new Shape
                    {
                        Box = new Box
                        {
                            Center = coordUtils.ConvertSpace(boxCollider.center).ToProto(),
                            Size = boxCollider.size.ToProto()
                        }
                    };
                case SphereCollider sphereCollider:
                    return new Shape
                    {
                        Sphere = new Sphere
                        {
                            Center = coordUtils.ConvertSpace(sphereCollider.center).ToProto(),
                            Radius = sphereCollider.radius
                        }
                    };
                case CapsuleCollider capsuleCollider:
                    return new Shape
                    {
                        Capsule = new Capsule
                        {
                            Center = coordUtils.ConvertSpace(capsuleCollider.center).ToProto(),
                            Direction = (Proto.Capsule.Types.Direction) capsuleCollider.direction,
                            Height = capsuleCollider.height,
                            Radius = capsuleCollider.radius
                        }
                    };
                case MeshCollider meshCollider:
                    var mesh = meshCollider.sharedMesh;
                    var triangles = mesh.triangles;
                    coordUtils.FlipIndices(triangles);
                    return new Shape
                    {
                        Mesh = new Proto.Mesh
                        {
                            VertexPositions = { mesh.vertices.Select(coordUtils.ConvertSpace).Flatten() },
                            Triangles = { triangles }
                        }
                    };
                default:
                    throw new ArgumentOutOfRangeException(nameof(collider), collider, null);
            }
        }

        MainScreenView TryGetMainScreenView(GameObject go)
        {
            var component = go.GetComponent<IMainScreenView>();
            if (component == null)
            {
                return null;
            }

            var result = new MainScreenView { ScreenAspectRatio = component.AspectRatio };
            TryAddMaterialProperty(result, go.GetComponent<Renderer>());

            return result;
        }

        void TryAddMaterialProperty(MainScreenView mainScreenView, Renderer renderer)
        {
            if (renderer != null)
            {
                var backgroundColorId = Shader.PropertyToID("_BackGroundColor");
                var material = renderer.sharedMaterial;
                if (material != null)
                {
                    var shader = material.shader;
                    if (shader.name == "ClusterVR/InternalSDK/MainScreen" ||
                        shader.name == "ClusterVR/UnlitNonTiledWithBackgroundColor")
                    {
                        var value = new UnlitNonTiledWithBackgroundColor();
                        value.BackgroundColor.AddRange(ColorToFloats(material.GetColor(backgroundColorId)));
                        mainScreenView.UnlitNonTiledWithBackgroundColor = value;
                    }
                }
            }
        }

        static Mirror TryGetMirror(GameObject go)
        {
            return go.TryGetComponent<IMirror>(out _) ? new Mirror() : null;
        }

        static IEnumerable<float> ColorToFloats(Color color)
        {
            return new[] { color.r, color.g, color.b, color.a };
        }
    }
}
