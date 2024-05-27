using ClusterVR.CreatorKit.Operation;
using ClusterVR.CreatorKit.Translation;
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
                        EditorGUILayout.HelpBox(TranslationUtility.GetMessage(TranslationTable.cck_gimmick_execution_error, target.GetType().Name, nameof(ParameterType)),
                            MessageType.Error);
                    }
                });
                container.Insert(1, warningContainer);
            }
            return container;
        }
    }
}
