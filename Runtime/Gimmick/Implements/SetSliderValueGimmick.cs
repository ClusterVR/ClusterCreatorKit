using System;
using ClusterVR.CreatorKit.Item;
using UnityEngine;
using UnityEngine.UI;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [RequireComponent(typeof(Slider)), DisallowMultipleComponent]
    public class SetSliderValueGimmick : MonoBehaviour, IGlobalGimmick
    {
        [SerializeField, HideInInspector] Slider slider;
        [SerializeField, LocalizableGlobalGimmickKey] GlobalGimmickKey globalGimmickKey;
        [SerializeField, ParameterTypeField(ParameterType.Integer, ParameterType.Float)] ParameterType parameterType = ParameterType.Float;

        ItemId IGimmick.ItemId => globalGimmickKey.ItemId;
        GimmickTarget IGimmick.Target => globalGimmickKey.Key.Target;
        string IGimmick.Key => globalGimmickKey.Key.Key;
        ParameterType IGimmick.ParameterType => parameterType;

        public void Run(GimmickValue value, DateTime current)
        {
            if (slider == null) slider = GetComponent<Slider>();
            slider.value = parameterType == ParameterType.Integer ? value.IntegerValue : value.FloatValue;
        }

        void OnValidate()
        {
            if (slider == null || slider.gameObject != gameObject) slider = GetComponent<Slider>();
        }

        void Reset()
        {
            slider = GetComponent<Slider>();
        }
    }
}