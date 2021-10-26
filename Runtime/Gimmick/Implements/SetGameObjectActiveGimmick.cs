using System;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [DisallowMultipleComponent, LocalizableGlobalGimmick(LocalizableGlobalGimmickAttribute.Condition.InPlayerLocal)]
    public sealed class SetGameObjectActiveGimmick : MonoBehaviour, IGlobalGimmick
    {
        [SerializeField] GlobalGimmickKey globalGimmickKey;

        ItemId IGimmick.ItemId => globalGimmickKey.ItemId;
        GimmickTarget IGimmick.Target => globalGimmickKey.Key.Target;
        string IGimmick.Key => globalGimmickKey.Key.Key;
        ParameterType IGimmick.ParameterType => ParameterType.Bool;

        public void Run(GimmickValue value, DateTime _)
        {
            gameObject.SetActive(value.BoolValue);
        }
    }
}
