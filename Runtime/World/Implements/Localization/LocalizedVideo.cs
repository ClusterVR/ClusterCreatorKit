using ClusterVR.CreatorKit.World;
using UnityEngine;
using UnityEngine.Video;

namespace ClusterVR.CreatorKit.World.Implements.Localization
{
    [RequireComponent(typeof(VideoPlayer)), DisallowMultipleComponent]
    public sealed class LocalizedVideo : MonoBehaviour, ILocalizedAsset
    {
        [SerializeField] LocalizationTexts localizationTexts;
        [SerializeField] VideoPlayer target;

        void ILocalizedAsset.SetLangCode(string langCode)
        {
            if (target == null)
            {
                target = GetComponent<VideoPlayer>();
            }
            var url = localizationTexts.GetContent(langCode);
            if (url == null)
            {
                return;
            }
            target.url = url;
        }

        void Reset()
        {
            target = GetComponent<VideoPlayer>();
        }

        void OnValidate()
        {
            if (target == null || target.gameObject != gameObject)
            {
                target = GetComponent<VideoPlayer>();
            }
        }
    }
}
