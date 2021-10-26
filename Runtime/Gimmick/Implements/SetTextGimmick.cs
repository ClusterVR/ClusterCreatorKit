using System;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using UnityEngine;
using UnityEngine.UI;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [DisallowMultipleComponent, RequireComponent(typeof(Text)),
        LocalizableGlobalGimmick(LocalizableGlobalGimmickAttribute.Condition.InPlayerLocal)]
    public sealed class SetTextGimmick : MonoBehaviour, IGlobalGimmick
    {
        static readonly ParameterType[] SelectableTypes =
            { ParameterType.Signal, ParameterType.Bool, ParameterType.Float, ParameterType.Integer, ParameterType.Vector2, ParameterType.Vector3 };

        [SerializeField, HideInInspector] Text text;
        [SerializeField] GlobalGimmickKey globalGimmickKey;
        [SerializeField, ParameterTypeField(ParameterType.Signal, ParameterType.Bool, ParameterType.Float, ParameterType.Integer, ParameterType.Vector2, ParameterType.Vector3)]
        ParameterType parameterType = SelectableTypes[0];
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
                case ParameterType.Vector2:
                    SetText(value.Vector2Value);
                    return;
                case ParameterType.Vector3:
                    SetText(value.Vector3Value);
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        void SetText<T>(T value)
        {
            if (text == null)
            {
                text = GetComponent<Text>();
            }
            if (string.IsNullOrWhiteSpace(format))
            {
                format = DefaultFormat;
            }

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
            if (text == null || text.gameObject != gameObject)
            {
                text = GetComponent<Text>();
            }

            if (!SelectableTypes.Contains(parameterType))
            {
                parameterType = SelectableTypes[0];
            }
        }

        void Reset()
        {
            text = GetComponent<Text>();
        }
    }
}
