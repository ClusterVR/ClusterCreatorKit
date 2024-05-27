using System;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Translation;
using UnityEngine;
using UnityEngine.UI;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [RequireComponent(typeof(Image)), DisallowMultipleComponent,
        LocalizableGlobalGimmick(LocalizableGlobalGimmickAttribute.Condition.InPlayerLocal)]
    public sealed class SetFillAmountGimmick : MonoBehaviour, IGlobalGimmick
    {
        [SerializeField, HideInInspector] Image image;
        [SerializeField] GlobalGimmickKey globalGimmickKey;

        [SerializeField, ParameterTypeField(ParameterType.Integer, ParameterType.Float)]
        ParameterType parameterType = ParameterType.Float;

        [SerializeField, Tooltip(TranslationTable.cck_fillamount_zero_value)] float minValue = 0f;
        [SerializeField, Tooltip(TranslationTable.cck_fillamount_one_value)] float maxValue = 1f;

        ItemId IGimmick.ItemId => globalGimmickKey.ItemId;
        GimmickTarget IGimmick.Target => globalGimmickKey.Key.Target;
        string IGimmick.Key => globalGimmickKey.Key.Key;
        ParameterType IGimmick.ParameterType => parameterType;

        public void Run(GimmickValue value, DateTime current)
        {
            if (image == null)
            {
                image = GetComponent<Image>();
            }
            var targetValue = parameterType == ParameterType.Integer ? value.IntegerValue : value.FloatValue;
            image.fillAmount = Mathf.InverseLerp(minValue, maxValue, targetValue);
        }

        void OnValidate()
        {
            if (image == null || image.gameObject != gameObject)
            {
                image = GetComponent<Image>();
            }
            image.type = Image.Type.Filled;
        }

        void Reset()
        {
            image = GetComponent<Image>();
        }
    }
}
