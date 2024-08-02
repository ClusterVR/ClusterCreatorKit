using ClusterVR.CreatorKit.Operation.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(GlobalTriggerLottery), isFallback = true), CanEditMultipleObjects]
    public class GlobalTriggerLotteryEditor : TriggerLotteryEditor
    {
    }
}
