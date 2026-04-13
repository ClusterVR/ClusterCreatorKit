#if UNITY_EDITOR
using ClusterVR.CreatorKit.Common;
using ClusterVR.CreatorKit.Item;
using UnityEngine;
using UnityEngine.Rendering;
#if CLUSTER_USE_URP
using System.Collections.Generic;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;
#endif

namespace ClusterVR.CreatorKit.Preview.Item
{
    [AddComponentMenu("")]
    public sealed class ItemHighlighter : MonoBehaviour
    {
        [SerializeField] Camera targetCamera;
        [SerializeField] ContactableItemFinder contactableItemFinder;
        [SerializeField] ContactableItemRaycaster contactableItemRaycaster;
        [SerializeField] Material outlineStencilMaterial;
        [SerializeField] Material candidateOutlineMaterial;
        [SerializeField] Material grabbableOutlineMaterial;
        [SerializeField] Material interactableOutlineMaterial;

        InputSystem_Actions.UIActions uiActions;

        CommandBuffer commandBuffer;

        void Start()
        {
            uiActions = new InputSystem_Actions().UI;
            uiActions.Enable();

            if (RenderPipelineUtils.IsUrp())
            {
                RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
            }
            else
            {
                commandBuffer = new CommandBuffer();
                targetCamera.AddCommandBuffer(CameraEvent.BeforeImageEffects, commandBuffer);
            }
        }

        void OnBeginCameraRendering(ScriptableRenderContext _, Camera camera)
        {
#if CLUSTER_USE_URP
            if (camera == targetCamera)
            {
                targetCamera.GetUniversalAdditionalCameraData()
                    .scriptableRenderer
                    .EnqueuePass(new ItemHighlightPass(
                        GetTargetItem(),
                        contactableItemFinder.ContactableItems,
                        outlineStencilMaterial, candidateOutlineMaterial,
                        grabbableOutlineMaterial, interactableOutlineMaterial));
            }
#endif
        }

        void Update()
        {
            if (!RenderPipelineUtils.IsUrp())
            {
                ConfigureCommandBuffer();
            }
        }

        void ConfigureCommandBuffer()
        {
            commandBuffer.Clear();
            ItemHighlightCommandBufferConfigurer.AddCommands(commandBuffer, new ItemHighlightCommandBufferConfigurer.Data()
            {
                targetItem = GetTargetItem(),
                contactableItems = contactableItemFinder.ContactableItems,
                grabbableOutlineMaterial = grabbableOutlineMaterial,
                candidateOutlineMaterial = candidateOutlineMaterial,
                interactableOutlineMaterial = interactableOutlineMaterial,
                outlineStencilMaterial = outlineStencilMaterial,
            });
        }

        IContactableItem GetTargetItem()
        {
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                contactableItemRaycaster.RaycastItem(uiActions.Point.ReadValue<Vector2>(), out var targetItem, out _);
                return targetItem;
            }
            else
            {
                return null;
            }
        }

#if CLUSTER_USE_URP
        sealed class ItemHighlightPass : ScriptableRenderPass
        {
            readonly IContactableItem targetItem;
            readonly IReadOnlyCollection<IContactableItem> contactableItems;
            readonly Material outlineStencilMaterial;
            readonly Material candidateOutlineMaterial;
            readonly Material grabbableOutlineMaterial;
            readonly Material interactableOutlineMaterial;

            public ItemHighlightPass(IContactableItem targetItem, IReadOnlyCollection<IContactableItem> contactableItems,
                Material outlineStencilMaterial, Material candidateOutlineMaterial,
                Material grabbableOutlineMaterial, Material interactableOutlineMaterial)
            {
                this.targetItem = targetItem;
                this.contactableItems = contactableItems;
                this.outlineStencilMaterial = outlineStencilMaterial;
                this.candidateOutlineMaterial = candidateOutlineMaterial;
                this.grabbableOutlineMaterial = grabbableOutlineMaterial;
                this.interactableOutlineMaterial = interactableOutlineMaterial;
            }

            public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
            {
                var resourceData = frameData.Get<UniversalResourceData>();
                var textureHandle = resourceData.activeColorTexture;

                using (var builder = renderGraph.AddUnsafePass<ItemHighlightCommandBufferConfigurer.Data>("ItemHighlight", out var data))
                {
                    data.targetItem = targetItem;
                    data.contactableItems = contactableItems;
                    data.grabbableOutlineMaterial = grabbableOutlineMaterial;
                    data.candidateOutlineMaterial = candidateOutlineMaterial;
                    data.interactableOutlineMaterial = interactableOutlineMaterial;
                    data.outlineStencilMaterial = outlineStencilMaterial;

                    builder.UseTexture(textureHandle, AccessFlags.Write);
                    builder.SetRenderFunc<ItemHighlightCommandBufferConfigurer.Data>(static (data, context) =>
                    {
                        ItemHighlightCommandBufferConfigurer.AddCommands(
                            CommandBufferHelpers.GetNativeCommandBuffer(context.cmd), data);
                    });
                }
            }
        }
#endif
    }
}
#endif
