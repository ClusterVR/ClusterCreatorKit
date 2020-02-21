using UnityEngine;

namespace ClusterVR.CreatorKit.World.Implements.MainScreenViews
{
    [RequireComponent(typeof(MeshRenderer))]
    public class StandardMainScreenView : MonoBehaviour, IMainScreenView
    {
        MeshRenderer meshRenderer;
        MaterialPropertyBlock propertyBlock;

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

            var scaleY = requiresYFlip ? -Mathf.Abs(transform.localScale.y) : Mathf.Abs(transform.localScale.y);
            var scale = new Vector2(Mathf.Abs(transform.localScale.x), scaleY);
            var textureSt = TextureSTCalculator.CalcOverlapTextureST(texture, scale);

            propertyBlock.SetTexture("_MainTex", texture);
            propertyBlock.SetVector("_MainTex_ST", textureSt);

            meshRenderer.SetPropertyBlock(propertyBlock);
        }
    }
}