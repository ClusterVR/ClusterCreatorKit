#if UNITY_EDITOR
using System.Collections.Generic;
using ClusterVR.CreatorKit.Item;
using UnityEngine;
using UnityEngine.Rendering;

namespace ClusterVR.CreatorKit.Preview.Item
{
    public static class ItemHighlightCommandBufferConfigurer
    {
        public sealed class Data
        {
            public IContactableItem targetItem;
            public IReadOnlyCollection<IContactableItem> contactableItems;
            public Material outlineStencilMaterial;
            public Material candidateOutlineMaterial;
            public Material grabbableOutlineMaterial;
            public Material interactableOutlineMaterial;
        }

        public static void AddCommands(CommandBuffer commandBuffer, Data data)
        {
            if (data.targetItem != null)
            {
                var objectToHighlight = data.targetItem.Item.gameObject;
                Draw(commandBuffer, objectToHighlight, data.outlineStencilMaterial);
                Draw(commandBuffer, objectToHighlight,
                    data.targetItem is IGrabbableItem ? data.grabbableOutlineMaterial : data.interactableOutlineMaterial);
            }

            foreach (var candidateItem in data.contactableItems)
            {
                var objectToHighlight = candidateItem.Item.gameObject;
                Draw(commandBuffer, objectToHighlight, data.outlineStencilMaterial);
                Draw(commandBuffer, objectToHighlight, data.candidateOutlineMaterial);
            }
        }

        static void Draw(CommandBuffer commandBuffer, GameObject gameObject, Material material)
        {
            foreach (var renderer in gameObject.GetComponentsInChildren<MeshRenderer>())
            {
                var meshFilter = renderer.GetComponent<MeshFilter>();
                if (meshFilter == null)
                {
                    continue;
                }
                var sharedMesh = meshFilter.sharedMesh;
                if (sharedMesh == null)
                {
                    continue;
                }
                DrawRenderer(commandBuffer, renderer, material, sharedMesh.subMeshCount);
            }

            foreach (var renderer in gameObject.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                var sharedMesh = renderer.sharedMesh;
                if (sharedMesh == null)
                {
                    continue;
                }
                DrawRenderer(commandBuffer, renderer, material, sharedMesh.subMeshCount);
            }
        }

        static void DrawRenderer(CommandBuffer commandBuffer, Renderer renderer, Material material, int submeshCount)
        {
            for (var index = 0; index < submeshCount; ++index)
            {
                commandBuffer.DrawRenderer(renderer, material, index);
            }
        }
    }
}
#endif
