using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.World.Implements.MainScreenViews
{
    [RequireComponent(typeof(MeshRenderer))]
    public class StandardMainScreenView : MonoBehaviour, IMainScreenView
    {
        MeshRenderer meshRenderer;
        MaterialPropertyBlock propertyBlock;
        [SerializeField] bool useCustomAspectRatio = false;
        [SerializeField] Vector2 screenAspectRatio = new Vector2(16,9);

        public event Action OnDestroyed;

        void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            propertyBlock = new MaterialPropertyBlock();
        }

        void Start()
        {
            this.DisableRichText();
            this.DisableImageRayCastTarget();
        }

        public void UpdateContent(Texture texture, bool requiresYFlip)
        {
            if (meshRenderer == null || propertyBlock == null)
            {
                return;
            }

            var localScale = transform.localScale;
            var screenScale = useCustomAspectRatio ? screenAspectRatio : new Vector2(localScale.x, localScale.y);

            var scaleY = requiresYFlip ? -Mathf.Abs(screenScale.y) : Mathf.Abs(screenScale.y);
            var scale = new Vector2(Mathf.Abs(screenScale.x), scaleY);
            var textureSt = TextureSTCalculator.CalcOverlapTextureST(texture, scale);

            propertyBlock.SetTexture("_MainTex", texture);
            propertyBlock.SetVector("_MainTex_ST", textureSt);

            meshRenderer.SetPropertyBlock(propertyBlock);
        }

        void OnDestroy()
        {
            OnDestroyed?.Invoke();
        }
    }
}