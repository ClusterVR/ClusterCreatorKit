using ClusterVR.CreatorKit.World;
using UnityEngine;
using UnityEngine.UI;

namespace ClusterVR.CreatorKit.World.Implements.Localization
{
    [RequireComponent(typeof(Text)), DisallowMultipleComponent]
    public sealed class LocalizedText : MonoBehaviour, ILocalizedAsset
    {
        [SerializeField] LocalizationTexts localizationTexts;
        [SerializeField] Text target;

        void ILocalizedAsset.SetLangCode(string langCode)
        {
            if (target == null)
            {
                target = GetComponent<Text>();
            }
            var text = localizationTexts.GetContent(langCode);
            if (text == null)
            {
                return;
            }
            target.text = text;
        }

        void Reset()
        {
            target = GetComponent<Text>();
        }

        void OnValidate()
        {
            if (target == null || target.gameObject != gameObject)
            {
                target = GetComponent<Text>();
            }
        }
    }
}


