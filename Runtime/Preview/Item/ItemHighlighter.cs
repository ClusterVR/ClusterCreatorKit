using ClusterVR.CreatorKit.Item;
using UnityEngine;
using UnityEngine.Rendering;

namespace ClusterVR.CreatorKit.Preview.Item
{
    public sealed class ItemHighlighter : MonoBehaviour
    {
        [SerializeField] Camera targetCamera;
        [SerializeField] InteractableItemFinder interactableItemFinder;
        [SerializeField] InteractableItemRaycaster interactableItemRaycaster;
        [SerializeField] Material outlineStencilMaterial;
        [SerializeField] Material candidateOutlineMaterial;
        [SerializeField] Material grabbableOutlineMaterial;
        [SerializeField] Material interactableOutlineMaterial;

        CommandBuffer commandBuffer;

        void Start()
        {
            commandBuffer = new CommandBuffer();
            targetCamera.AddCommandBuffer(CameraEvent.BeforeImageEffects, commandBuffer);
        }

        void Update()
        {
            commandBuffer.Clear();
            // アウトライン部分もステンシルに書き込んでいるので、interactableの方を先に書いて優先する
            if (Cursor.lockState != CursorLockMode.Locked &&
                interactableItemRaycaster.RaycastItem(Input.mousePosition, out var interactableItem, out _))
            {
                var objectToHighlight = interactableItem.Item.gameObject;
                Draw(objectToHighlight, outlineStencilMaterial);
                Draw(objectToHighlight, interactableItem is IGrabbableItem ? grabbableOutlineMaterial : interactableOutlineMaterial);
            }

            foreach (var candidateItem in interactableItemFinder.InteractableItems)
            {
                var objectToHighlight = candidateItem.Item.gameObject;
                Draw(objectToHighlight, outlineStencilMaterial);
                Draw(objectToHighlight, candidateOutlineMaterial);
            }
        }

        void Draw(GameObject gameObject, Material material)
        {
            foreach (var renderer in gameObject.GetComponentsInChildren<MeshRenderer>())
            {
                var meshFilter = renderer.GetComponent<MeshFilter>();
                if (meshFilter == null) continue;
                var sharedMesh = meshFilter.sharedMesh;
                if (sharedMesh == null) continue;
                DrawRenderer(renderer, material, sharedMesh.subMeshCount);
            }
            foreach (var renderer in gameObject.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                var sharedMesh = renderer.sharedMesh;
                if (sharedMesh == null) continue;
                DrawRenderer(renderer, material, sharedMesh.subMeshCount);
            }
        }

        void DrawRenderer(Renderer renderer, Material material, int submeshCount)
        {
            for (var index = 0; index < submeshCount; ++index) commandBuffer.DrawRenderer(renderer, material, index);
        }
    }
}
