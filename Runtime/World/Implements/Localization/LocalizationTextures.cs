using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ClusterVR.CreatorKit.World.Implements.Localization
{
    [CreateAssetMenu(fileName = "LocalizationTextures", menuName = "ScriptableObjects/Localization/Textures - cluster", order = 2)]
    [Serializable]
    public sealed class LocalizationTextures : ScriptableObject
    {
        [Serializable]
        struct TextureWithLangCode
        {
            [SerializeField, LangCode] string langCode;
            [SerializeField] Texture2D texture;

            public string LangCode => langCode;
            public Texture2D Texture => texture;
        }

        [SerializeField] TextureWithLangCode[] settings;

        public Texture2D GetContent(string langCode)
        {
            if (settings.Length == 0)
            {
                return null;
            }
            var content = settings.FirstOrDefault(asset => asset.LangCode == langCode).Texture;
            return content ? content : settings.First().Texture;
        }
    }
}
