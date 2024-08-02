using ClusterVR.CreatorKit.Operation.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(ItemTriggerLottery), isFallback = true), CanEditMultipleObjects]
    public class ItemTriggerLotteryEditor : TriggerLotteryEditor
    {
    }
}
