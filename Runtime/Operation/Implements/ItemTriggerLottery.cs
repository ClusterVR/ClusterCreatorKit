using System;
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Constants;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Gimmick.Implements;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Trigger;
using ClusterVR.CreatorKit.Trigger.Implements;
using UnityEngine;

namespace ClusterVR.CreatorKit.Operation.Implements
{
    [RequireComponent(typeof(Item.Implements.Item))]
    public sealed class ItemTriggerLottery : MonoBehaviour, IItemTrigger, IItemGimmick
    {
        [SerializeField, HideInInspector] Item.Implements.Item item;
        [SerializeField, ItemGimmickKey] GimmickKey key = new GimmickKey(GimmickTarget.Item);
        [SerializeField, LotteryChoice] Choice[] choices;

        [Serializable]
        sealed class Choice
        {
            [SerializeField] float weight;
            [SerializeField, ItemTriggerLotteryTriggerParam] ConstantTriggerParam[] triggers;

            TriggerParam[] triggersCache;

            internal float Weight => weight;
            internal IEnumerable<TriggerParam> Triggers => triggers.Select(t => t.Convert());
            internal TriggerParam[] CachedTriggers => triggersCache ?? (triggersCache = Triggers.ToArray());

            public void Correct()
            {
                if (weight < 0f)
                {
                    weight = 0f;
                }
                triggers = triggers?.Select(trigger =>
                        trigger.Target != TriggerTarget.Item && trigger.Target != TriggerTarget.Player
                            ? new ConstantTriggerParam(TriggerTarget.Item, null, trigger.Key, trigger.Type,
                                trigger.RawValue)
                            : trigger)
                    .ToArray();
            }
        }

        IItem Item => item != null ? item : item = GetComponent<Item.Implements.Item>();
        IItem IItemTrigger.Item => Item;
        ItemId IGimmick.ItemId => Item.Id;

        GimmickTarget IGimmick.Target => key.Target;
        string IGimmick.Key => key.Key;
        ParameterType IGimmick.ParameterType => ParameterType.Signal;

        public event TriggerEventHandler TriggerEvent;
        IEnumerable<TriggerParam> ITrigger.TriggerParams => choices.SelectMany(c => c.Triggers);

        DateTime lastTriggeredAt;

        public void Run(GimmickValue value, DateTime current)
        {
            if (choices.Length == 0)
            {
                return;
            }
            if (value.TimeStamp <= lastTriggeredAt)
            {
                return;
            }
            lastTriggeredAt = value.TimeStamp;
            if ((current - value.TimeStamp).TotalSeconds > TriggerGimmick.TriggerExpireSeconds)
            {
                return;
            }
            lastTriggeredAt = value.TimeStamp;

            Invoke();
        }

        void Invoke()
        {
            if (Lottery.TryGetWeightRandom(choices, c => c.Weight, out var result))
            {
                TriggerEvent?.Invoke(this, new TriggerEventArgs(result.CachedTriggers));
            }
        }

        void Reset()
        {
            item = GetComponent<Item.Implements.Item>();
        }

        void OnValidate()
        {
            if (item == null || item.gameObject != gameObject)
            {
                item = GetComponent<Item.Implements.Item>();
            }
            if (choices != null)
            {
                foreach (var choice in choices)
                {
                    choice.Correct();
                }
            }
        }
    }
}
