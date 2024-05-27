using System;
using ClusterVR.CreatorKit.Editor.Builder;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Enquete
{
    public static class EnqueteService
    {
        public const int SuggestIntervalOnOpen = 60 * 60 * 24 * 30; // 30 days
        public const int SuggestIntervalOnCancel = 60 * 60 * 24 * 7; // 7 days
        public const string EnqueteUrl = "https://forms.gle/6TYFtREQBcEUqqUb6";

        public static bool ShouldShowEnqueteRequest()
        {
            return EditorPrefsUtils.NextEnqueteAskTime <= GetCurrentTimeStamp();
        }

        public static void ShowEnqueteDialog()
        {
            var ok = EditorUtility.DisplayDialog(
                TranslationTable.cck_survey_request,
                TranslationTable.cck_quality_improvement_survey_invitation,
                TranslationTable.cck_answer,
                TranslationTable.cck_later);

            if (ok)
            {
                OpenEnqueteLink();
            }
            else
            {
                CancelEnquete();
            }
        }

        public static void OpenEnqueteLink()
        {
            EditorPrefsUtils.NextEnqueteAskTime = GetCurrentTimeStamp() + SuggestIntervalOnOpen;
            Application.OpenURL(EnqueteUrl);
        }
        public static void CancelEnquete()
        {
            EditorPrefsUtils.NextEnqueteAskTime = GetCurrentTimeStamp() + SuggestIntervalOnCancel;
        }

        static int GetCurrentTimeStamp()
        {
            return (int) DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }
}
