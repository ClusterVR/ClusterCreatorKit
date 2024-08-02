using ClusterVR.CreatorKit.Operation.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(PlayerTriggerLottery), isFallback = true), CanEditMultipleObjects]
    public class PlayerTriggerLotteryEditor : TriggerLotteryEditor
    {
    }
}
