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
        [SerializeField] GlobalGimmickKey globalGimmickKey;
        [SerializeField] ParameterType parameterType;
        [SerializeField, Tooltip("Textに設定するフォーマット"), Multiline] string format = DefaultFormat;

        const string DefaultFormat = "{0}";
        ItemId IGlobalGimmick.ItemId => globalGimmickKey.ItemId;
        GimmickTarget IGimmick.Target => globalGimmickKey.Key.Target;
        string IGimmick.Key => globalGimmickKey.Key.Key;
        ParameterType IGimmick.ParameterType => parameterType;

        void Start()
        {
            if (text == null) text = GetComponent<Text>();
            if (string.IsNullOrWhiteSpace(format)) format = DefaultFormat;
        }

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
            text.text = string.Format(format, value);
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
