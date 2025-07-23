using ClusterVR.CreatorKit.Editor.Utils;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Infrastructure
{
    public sealed class ReactiveEditorPrefs
    {
        const string AlreadyShownAboutWindowKey = "ClusterAlreadyShownAboutWindow";
        const string AccessTokenSaveKey = "cluster_sdk_access_token";
        const string TmpUserIdKey = "ClusterTmpUserId";
        const string SavedUserIdKey = "ClusterSavedUserId";
        const string NextEnqueteAskTimeKey = "NextEnqueteAskTime";
        const string SavedLanguageSettingKey = "ClusterCreatorKitSavedLanguageSetting";

        public static readonly ReactiveEditorPrefs Instance = new();

        readonly Reactive<bool> hasAlreadyShownAboutWindow = new();
        readonly Reactive<string> savedAccessToken = new();
        readonly Reactive<string> tmpUserId = new();
        readonly Reactive<string> savedUserId = new();
        readonly Reactive<int> nextEnqueteAskTime = new();
        readonly Reactive<string> languageSettings = new();

        public IReadOnlyReactive<bool> HasAlreadyShownAboutWindow => hasAlreadyShownAboutWindow;
        public IReadOnlyReactive<string> SavedAccessToken => savedAccessToken;
        public IReadOnlyReactive<string> TmpUserId => tmpUserId;
        public IReadOnlyReactive<string> SavedUserId => savedUserId;
        public IReadOnlyReactive<int> NextEnqueteAskTime => nextEnqueteAskTime;
        public IReadOnlyReactive<string> LanguageSettings => languageSettings;

        ReactiveEditorPrefs()
        {
            hasAlreadyShownAboutWindow.Val = EditorPrefs.GetBool(AlreadyShownAboutWindowKey, false);
            savedAccessToken.Val = EditorPrefs.GetString(AccessTokenSaveKey, "");
            tmpUserId.Val = EditorPrefs.GetString(TmpUserIdKey, "");
            savedUserId.Val = EditorPrefs.GetString(SavedUserIdKey, "");
            nextEnqueteAskTime.Val = EditorPrefs.GetInt(NextEnqueteAskTimeKey, 0);
            languageSettings.Val = EditorPrefs.GetString(SavedLanguageSettingKey, "");
        }

        public void SetHasAlreadyShownAboutWindow(bool value)
        {
            hasAlreadyShownAboutWindow.Val = value;
            EditorPrefs.SetBool(AlreadyShownAboutWindowKey, value);
        }

        public void SetSavedAccessToken(string value)
        {
            savedAccessToken.Val = value ?? "";
            EditorPrefs.SetString(AccessTokenSaveKey, value);
        }

        public void SetTmpUserId(string value)
        {
            tmpUserId.Val = value ?? "";
            EditorPrefs.SetString(TmpUserIdKey, value);
        }

        public void SetSavedUserId(string value)
        {
            savedUserId.Val = value ?? "";
            EditorPrefs.SetString(SavedUserIdKey, value);
        }

        public void SetNextEnqueteAskTime(int value)
        {
            nextEnqueteAskTime.Val = value;
            EditorPrefs.SetInt(NextEnqueteAskTimeKey, value);
        }

        public void SetLanguageSetting(string value)
        {
            languageSettings.Val = value ?? "";
            EditorPrefs.SetString(SavedLanguageSettingKey, value);
        }
    }
}
