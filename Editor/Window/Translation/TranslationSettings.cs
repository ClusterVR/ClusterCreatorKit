using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Editor.Builder;
using ClusterVR.CreatorKit.World;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Window.Translation
{
    public static class TranslationSettings
    {
        [InitializeOnLoadMethod]
        public static void UpdateLanguageSettings()
        {
            if (!IsLanguageSettingExists())
            {
                SetLanguageSettingBySystemLanguage();
            }
            ApplySymbolsForTarget();
        }
        public static void ApplySymbolsForTarget()
        {
            var languageSettingKey = EditorPrefsUtils.LanguageSetting;
            var target = NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup);

            PlayerSettings.GetScriptingDefineSymbols(target, out var defines);
            var symbolsList = defines.ToList();

            if (symbolsList.Contains(languageSettingKey))
            {
                return;
            }
            symbolsList = ConvertToScriptingDefineSymbols(languageSettingKey, symbolsList);
            PlayerSettings.SetScriptingDefineSymbols(target, symbolsList.ToArray());
        }

        static bool IsLanguageSettingExists()
        {
            var languageSettingKey = EditorPrefsUtils.LanguageSetting;
            return !string.IsNullOrEmpty(languageSettingKey);
        }

        public static void SetLanguageSettingBySystemLanguage()
        {
            var currentLanguage = Application.systemLanguage == SystemLanguage.Japanese
                ? "ja"
                : "en";
            var languageSettingKey = GetLanguageSettingKey(currentLanguage);
            EditorPrefsUtils.LanguageSetting = languageSettingKey;
        }

        static List<string> ConvertToScriptingDefineSymbols(string to, List<string> symbolsList)
        {
            var other = ServerLang.LangCodes.ToList().Where(l => GetLanguageSettingKey(l) != to)
                .Select(GetLanguageSettingKey).ToList();
            symbolsList.RemoveAll(other.Contains);

            if (!symbolsList.Contains(to))
            {
                symbolsList.Add(to);
            }

            return symbolsList;
        }
        public static string GetLanguageSettingKey(string code) { return "cck_" + code; }
    }
}
