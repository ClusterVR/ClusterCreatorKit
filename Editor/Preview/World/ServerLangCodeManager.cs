using System.Collections.Generic;
using ClusterVR.CreatorKit.World;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Preview.World
{
    public static class ServerLangCodeManager
    {
        const string ServerLangCodeKey = "ServerLangCode";

        public static void InitializeLocalizedAssets(IEnumerable<ILocalizedAsset> localizedAssets)
        {
            var serverLangCode = GetLangCode();
            foreach (var localizedAsset in localizedAssets)
            {
                localizedAsset.SetLangCode(serverLangCode);
            }
        }

        public static string GetLangCode()
        {
            return PlayerPrefs.GetString(ServerLangCodeKey, "ja");
        }

        public static void SetLangCode(string langCode)
        {
            PlayerPrefs.SetString(ServerLangCodeKey, langCode);
        }
    }
}
