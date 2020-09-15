using System;
using ClusterVR.CreatorKit.Item;
using UnityEngine;
using UnityEngine.UI;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [DisallowMultipleComponent, RequireComponent(typeof(Text))]
    public class SetTextGimmick : MonoBehaviour, IGlobalGimmick
    {
        [SerializeField, HideInInspector] Text  text;
        [SerializeField, LocalizableGlobalGimmickKey] GlobalGimmickKey globalGimmickKey;
        [SerializeField] ParameterType parameterType;
        [SerializeField, Tooltip("Textに設定するフォーマット"), Multiline] string format = DefaultFormat;

        const string DefaultFormat = "{0}";
        ItemId IGimmick.ItemId => globalGimmickKey.ItemId;
        GimmickTarget IGimmick.Target => globalGimmickKey.Key.Target;
        string IGimmick.Key => globalGimmickKey.Key.Key;
        ParameterType IGimmick.ParameterType => parameterType;

        public void Run(GimmickValue value, DateTime current)
        {
            switch (parameterType)
            {
                case ParameterType.Signal:
                    SetText(value.TimeStamp.ToLocalTime());
                    return;
                case ParameterType.Bool:
                    SetText(value.BoolValue);
                    return;
                case ParameterType.Float:
                    SetText(value.FloatValue);
                    return;
                case ParameterType.Integer:
                    SetText(value.IntegerValue);
                    return;
            }
        }

        void SetText<T>(T value)
        {
            if (text == null) text = GetComponent<Text>();
            if (string.IsNullOrWhiteSpace(format)) format = DefaultFormat;

            try
            {
                text.text = string.Format(format, value);
            }
            catch (FormatException e)
            {
#if UNITY_EDITOR
                Debug.LogException(e, this);
#endif
            }
        }

        void OnValidate()
        {
            if (text == null || text.gameObject != gameObject) text = GetComponent<Text>();
        }

        void Reset()
        {
            text = GetComponent<Text>();
        }
    }
}
