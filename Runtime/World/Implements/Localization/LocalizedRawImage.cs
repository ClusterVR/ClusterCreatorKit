using ClusterVR.CreatorKit.World;
using UnityEngine;
using UnityEngine.UI;

namespace ClusterVR.CreatorKit.World.Implements.Localization
{
    [RequireComponent(typeof(RawImage)), DisallowMultipleComponent]
    public sealed class LocalizedRawImage : MonoBehaviour, ILocalizedAsset
    {
        [SerializeField] LocalizationTextures localizationTextures;
        [SerializeField] RawImage target;

        void ILocalizedAsset.SetLangCode(string langCode)
        {
            if (target == null)
            {
                target = GetComponent<RawImage>();
            }
            var texture = localizationTextures.GetContent(langCode);
            if (texture == null)
            {
                return;
            }
            target.texture = texture;
        }

        void Reset()
        {
            target = GetComponent<RawImage>();
        }

        void OnValidate()
        {
            if (target == null || target.gameObject != gameObject)
            {
                target = GetComponent<RawImage>();
            }
        }
    }
}
