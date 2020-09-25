using System;
using ClusterVR.CreatorKit.Item;
using UnityEngine;
using UnityEngine.UI;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [RequireComponent(typeof(Image)), DisallowMultipleComponent]
    public class SetFillAmountGimmick : MonoBehaviour, IGlobalGimmick
    {
        [SerializeField, HideInInspector] Image image;
        [SerializeField, LocalizableGlobalGimmickKey] GlobalGimmickKey globalGimmickKey;
        [SerializeField, ParameterTypeField(ParameterType.Integer, ParameterType.Float)] ParameterType parameterType = ParameterType.Float;
        [SerializeField, Tooltip("FillAmountが0となるときの値")] float minValue = 0f;
        [SerializeField, Tooltip("FillAmountが1となるときの値")] float maxValue = 1f;

        ItemId IGimmick.ItemId => globalGimmickKey.ItemId;
        GimmickTarget IGimmick.Target => globalGimmickKey.Key.Target;
        string IGimmick.Key => globalGimmickKey.Key.Key;
        ParameterType IGimmick.ParameterType => parameterType;

        public void Run(GimmickValue value, DateTime current)
        {
            if (image == null) image = GetComponent<Image>();
            var targetValue = parameterType == ParameterType.Integer ? value.IntegerValue : value.FloatValue;
            image.fillAmount = Mathf.InverseLerp(minValue, maxValue, targetValue);
        }

        void OnValidate()
        {
            if (image == null || image.gameObject != gameObject) image = GetComponent<Image>();
            image.type = Image.Type.Filled;
        }

        void Reset()
        {
            image = GetComponent<Image>();
        }
    }
}