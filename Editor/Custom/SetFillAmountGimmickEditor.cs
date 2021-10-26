using ClusterVR.CreatorKit.Gimmick.Implements;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(SetFillAmountGimmick)), CanEditMultipleObjects]
    public sealed class SetFillAmountGimmickEditor : VisualElementEditor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var container = base.CreateInspectorGUI();
            var image = ((Component) target).GetComponent<Image>();
            var helpBox = new IMGUIContainer(() =>
            {
                if (image.sprite == null)
                {
                    EditorGUILayout.HelpBox($"{nameof(Image)}のSource Imageを指定する必要があります。", MessageType.Warning);
                }
                else if (image.type != Image.Type.Filled)
                {
                    EditorGUILayout.HelpBox($"{nameof(Image)}のImage Typeを{nameof(Image.Type.Filled)}に指定する必要があります。",
                        MessageType.Warning);
                }
            });
            container.Add(helpBox);
            return container;
        }
    }
}
