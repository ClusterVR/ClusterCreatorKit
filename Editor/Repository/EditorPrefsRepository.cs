using ClusterVR.CreatorKit.Editor.Infrastructure;
using ClusterVR.CreatorKit.Editor.Utils;

namespace ClusterVR.CreatorKit.Editor.Repository
{
    public sealed class EditorPrefsRepository
    {
        public static readonly EditorPrefsRepository Instance = new();

        ReactiveEditorPrefs ReactiveEditorPrefs => ReactiveEditorPrefs.Instance;

        public IReadOnlyReactive<bool> HasAlreadyShownAboutWindow => ReactiveEditorPrefs.HasAlreadyShownAboutWindow;
        public IReadOnlyReactive<string> TmpUserId => ReactiveEditorPrefs.TmpUserId;
        public IReadOnlyReactive<int> NextEnqueteAskTime => ReactiveEditorPrefs.NextEnqueteAskTime;
        public IReadOnlyReactive<string> LanguageSettings => ReactiveEditorPrefs.LanguageSettings;

        EditorPrefsRepository()
        {
        }

        public void SetHasAlreadyShownAboutWindow(bool value)
        {
            ReactiveEditorPrefs.SetHasAlreadyShownAboutWindow(value);
        }

        public void SetTmpUserId(string value)
        {
            ReactiveEditorPrefs.SetTmpUserId(value);
        }

        public void SetNextEnqueteAskTime(int value)
        {
            ReactiveEditorPrefs.SetNextEnqueteAskTime(value);
        }

        public void SetLanguageSetting(string value)
        {
            ReactiveEditorPrefs.SetLanguageSetting(value);
        }
    }
}
