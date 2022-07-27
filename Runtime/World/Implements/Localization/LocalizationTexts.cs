using System;
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.World;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace ClusterVR.CreatorKit.World.Implements.Localization
{
    [CreateAssetMenu(fileName = "LocalizationTexts", menuName = "ScriptableObjects/Localization/Texts - cluster", order = 1)]
    [Serializable]
    public sealed class LocalizationTexts : ScriptableObject
    {
        [Serializable]
        struct TextWithLangCode
        {
            [SerializeField, LangCode] string langCode;
            [SerializeField] string text;

            public string LangCode => langCode;
            public string Text => text;
        }

        [SerializeField] TextWithLangCode[] settings;

        public string GetContent(string langCode)
        {
            if (settings.Length == 0)
            {
                return null;
            }
            return settings.FirstOrDefault(asset => asset.LangCode == langCode).Text ?? settings.First().Text;
        }
    }
}
