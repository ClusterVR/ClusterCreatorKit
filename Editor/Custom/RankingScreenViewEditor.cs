using ClusterVR.CreatorKit.World.Implements.RankingScreenViews;
using UnityEditor;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(RankingScreenView))]
    public sealed class RankingScreenViewEditor : VisualElementEditor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var container = base.CreateInspectorGUI();
            var rankingScreenView = (RankingScreenView) target;
            var warningBox = new IMGUIContainer(() =>
            {
                if (rankingScreenView.HasInvalidCell())
                {
                    EditorGUILayout.HelpBox($"{nameof(RankingScreenCell)}が設定されていないBoardCellsが存在します", MessageType.Warning);
                }
            });
            container.Insert(0, warningBox);
            return container;
        }
    }
}
