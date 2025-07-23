using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Editor.Repository;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Window.Translation
{
    public static class TranslationSettings
    {
        public static readonly string[] EditorLanguages = { "en", "ja" };

        static EditorPrefsRepository EditorPrefsRepository => EditorPrefsRepository.Instance;

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
            var languageSettingKey = EditorPrefsRepository.LanguageSettings.Val;
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
            var languageSettingKey = EditorPrefsRepository.LanguageSettings.Val;
            return !string.IsNullOrEmpty(languageSettingKey);
        }

        public static void SetLanguageSettingBySystemLanguage()
        {
            var currentLanguage = Application.systemLanguage == SystemLanguage.Japanese
                ? "ja"
                : "en";
            var languageSettingKey = GetLanguageSettingKey(currentLanguage);
            EditorPrefsRepository.SetLanguageSetting(languageSettingKey);
        }

        static List<string> ConvertToScriptingDefineSymbols(string to, List<string> symbolsList)
        {
            var other = EditorLanguages.Where(l => GetLanguageSettingKey(l) != to)
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
