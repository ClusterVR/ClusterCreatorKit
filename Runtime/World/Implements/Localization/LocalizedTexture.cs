using ClusterVR.CreatorKit.World;
using UnityEngine;

namespace ClusterVR.CreatorKit.World.Implements.Localization
{
    [RequireComponent(typeof(Renderer)), DisallowMultipleComponent]
    public sealed class LocalizedTexture : MonoBehaviour, ILocalizedAsset
    {
        [SerializeField] LocalizationTextures localizationTextures;
        [SerializeField] string targetMaterialPropertyName = "_MainTex";
        [SerializeField] Renderer target;

        void ILocalizedAsset.SetLangCode(string langCode)
        {
            if (target == null)
            {
                target = GetComponent<Renderer>();
            }
            var texture = localizationTextures.GetContent(langCode);
            if (texture == null)
            {
                return;
            }
            var materialPropertyBlock = new MaterialPropertyBlock();
            materialPropertyBlock.SetTexture(targetMaterialPropertyName, texture);
            target.SetPropertyBlock(materialPropertyBlock);
        }

        void Reset()
        {
            target = GetComponent<Renderer>();
        }

        void OnValidate()
        {
            if (target == null || target.gameObject != gameObject)
            {
                target = GetComponent<Renderer>();
            }
        }
    }
}
