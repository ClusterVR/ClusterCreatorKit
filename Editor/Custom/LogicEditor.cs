using ClusterVR.CreatorKit.Operation;
using UnityEditor;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    public abstract class LogicEditor : VisualElementEditor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var container = base.CreateInspectorGUI();
            if (target is ILogic targetlogic)
            {
                var warningContainer = new IMGUIContainer(() =>
                {
                    if (!targetlogic.Logic.IsValid())
                    {
                        EditorGUILayout.HelpBox($"エラーがあるためこの{target.GetType().Name}は実行されません。\nkeyが入力されており、{nameof(ParameterType)}の組み合わせが正しいかを確認してください。",
                            MessageType.Error);
                    }
                });
                container.Insert(1, warningContainer);
            }
            return container;
        }
    }
}
