using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.World.Implements.MainScreenViews
{
    [RequireComponent(typeof(MeshRenderer))]
    public sealed class StandardMainScreenView : MonoBehaviour, IMainScreenView
    {
        MeshRenderer meshRenderer;
        MaterialPropertyBlock propertyBlock;
        [SerializeField] bool useCustomAspectRatio = false;
        [SerializeField] Vector2 screenAspectRatio = new Vector2(16, 9);

        public event Action OnDestroyed;

        public float AspectRatio
        {
            get
            {
                if (useCustomAspectRatio)
                {
                    return Mathf.Abs(screenAspectRatio.x / screenAspectRatio.y);
                }
                else
                {
                    var localScale = transform.localScale;
                    return Mathf.Abs(localScale.x / localScale.y);
                }
            }
            set
            {
                useCustomAspectRatio = true;
                screenAspectRatio = new Vector2(value, 1f);
            }
        }

        void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        void Start()
        {
            this.DisableRichText();
            this.DisableImageRayCastTarget();
        }

        public void UpdateContent(Texture texture, bool requiresYFlip)
        {
            if (propertyBlock == null) propertyBlock = new MaterialPropertyBlock();

            var textureSt = TextureSTCalculator.CalcOverlapTextureST(texture, AspectRatio, requiresYFlip);

            propertyBlock.SetTexture("_MainTex", texture);
            propertyBlock.SetVector("_MainTex_ST", textureSt);

            if (meshRenderer == null) return;

            meshRenderer.SetPropertyBlock(propertyBlock);
        }

        void OnDestroy()
        {
            OnDestroyed?.Invoke();
        }

        void OnEnable()
        {
            meshRenderer.SetPropertyBlock(propertyBlock);
        }
    }
}
