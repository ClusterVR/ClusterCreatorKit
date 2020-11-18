using System;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [RequireComponent(typeof(AudioSource)), LocalizableGlobalGimmick(LocalizableGlobalGimmickAttribute.Condition.Always)]
    public class PlayAudioSourceGimmick : MonoBehaviour, IGlobalGimmick
    {
        static readonly ParameterType[] selectableTypes = { ParameterType.Signal, ParameterType.Bool };

        [SerializeField] AudioSource audioSource;
        [SerializeField] GlobalGimmickKey globalGimmickKey;
        [SerializeField, ParameterTypeField(ParameterType.Signal, ParameterType.Bool)]
        ParameterType parameterType = selectableTypes[0];

        ItemId IGimmick.ItemId => globalGimmickKey.ItemId;
        GimmickTarget IGimmick.Target => globalGimmickKey.Key.Target;
        string IGimmick.Key => globalGimmickKey.Key.Key;
        ParameterType IGimmick.ParameterType => parameterType;

        DateTime lastTriggeredAt;

        void Start()
        {
            if (audioSource == null) audioSource = GetComponentInChildren<AudioSource>();
        }

        public void Run(GimmickValue value, DateTime current)
        {
            if (audioSource == null) return;
            switch (parameterType)
            {
                case ParameterType.Signal:
                    if (value.TimeStamp <= lastTriggeredAt) return;
                    lastTriggeredAt = value.TimeStamp;
                    if ((current - value.TimeStamp).TotalSeconds > Constants.Gimmick.TriggerExpireSeconds) return;
                    audioSource.Play();
                    break;
                case ParameterType.Bool:
                    if (value.BoolValue)
                    {
                        audioSource.Play();
                    }
                    else
                    {
                        audioSource.Stop();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        void Reset()
        {
            audioSource = GetComponent<AudioSource>();
        }

        void OnValidate()
        {
            if (audioSource == null || audioSource.gameObject != gameObject) audioSource = GetComponent<AudioSource>();
            if (!selectableTypes.Contains(parameterType))
            {
                parameterType = selectableTypes[0];
            }

            var canvas = GetComponentsInParent<Canvas>(true).FirstOrDefault();
            if (canvas != null && canvas.rootCanvas.renderMode != RenderMode.WorldSpace)
            {
                audioSource.spatialize = false;
                audioSource.spatializePostEffects = false;
                audioSource.spatialBlend = 0.0f;
            }
        }
    }
}