using System;
using System.Linq;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Gimmick.Implements;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Trigger;
using ClusterVR.CreatorKit.Trigger.Implements;
using UnityEngine;

namespace ClusterVR.CreatorKit.Operation.Implements
{
    public sealed class GlobalTriggerLottery : MonoBehaviour, IGlobalTrigger, IGlobalGimmick
    {
        [SerializeField, ConsistentlySyncGlobalGimmickKey] GlobalGimmickKey globalGimmickKey;
        [SerializeField] Choice[] choices;

        [Serializable]
        sealed class Choice
        {
            [SerializeField] float weight;
            [SerializeField, GlobalOperationTriggerParam] Trigger.Implements.TriggerParam[] triggers;

            Trigger.TriggerParam[] triggersCache;

            internal float Weight => weight;
            internal Trigger.TriggerParam[] Triggers => triggersCache ?? (triggersCache = triggers.Select(t => t.Convert()).ToArray());

            public void Correct()
            {
                if (weight < 0f) weight = 0f;
                triggers = triggers?.Select(trigger =>
                    trigger.Target != TriggerTarget.Global ?
                        new Trigger.Implements.TriggerParam(TriggerTarget.Global, null, trigger.Key, trigger.Type, trigger.RawValue) :
                        trigger)
                    .ToArray();
            }
        }

        GimmickTarget IGimmick.Target => globalGimmickKey.Key.Target;
        ItemId IGimmick.ItemId => globalGimmickKey.ItemId;

        string IGimmick.Key => globalGimmickKey.Key.Key;
        ParameterType IGimmick.ParameterType => ParameterType.Signal;

        public event GlobalTriggerEventHandler TriggerEvent;

        DateTime lastTriggeredAt;

        public void Run(GimmickValue value, DateTime current)
        {
            if (choices.Length == 0) return;
            if (value.TimeStamp <= lastTriggeredAt) return;
            lastTriggeredAt = value.TimeStamp;
            if ((current - value.TimeStamp).TotalSeconds > Constants.Gimmick.TriggerExpireSeconds) return;
            lastTriggeredAt = value.TimeStamp;

            Invoke();
        }

        void Invoke()
        {
            if (Lottery.TryGetWeightRandom(choices, c => c.Weight, out var result))
            {
                TriggerEvent?.Invoke(new TriggerEventArgs(result.Triggers));
            }
        }

        void OnValidate()
        {
            if (choices != null) foreach (var choice in choices) choice.Correct();
        }
    }
}