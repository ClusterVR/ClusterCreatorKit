using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger
{
    public interface ISteerItemTrigger : IItemTrigger
    {
        SteerSpace FirstPersonMoveSpace { get; }
        SteerSpace ThirdPersonMoveSpace { get; }
        bool HasMoveInputTriggers();
        bool HasAdditionalAxisInputTriggers();
        void OnMoveInputValueChanged(Vector2 input);
        void OnAdditionalAxisInputValueChanged(float input);
    }

    public enum SteerSpace
    {
        SeatLocal,
        SeatToCamera
    }

    public static class ISteerItemTriggerExtensions
    {
        public static ISteerItemTrigger GetSteerItemTrigger(this IRidableItem ridableItem)
        {
            return ridableItem.Item.GetComponent<ISteerItemTrigger>();
        }
    }
}
