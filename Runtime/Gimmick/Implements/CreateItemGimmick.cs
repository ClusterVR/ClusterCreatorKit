﻿using System;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [RequireComponent(typeof(Item.Implements.Item))]
    public sealed class CreateItemGimmick : MonoBehaviour, IItemGimmick, ICreateItemGimmick
    {
        [SerializeField, HideInInspector] Item.Implements.Item item;
        [SerializeField, ItemGimmickKey] GimmickKey key = new GimmickKey(Target.Item);
        [SerializeField] Item.Implements.Item itemTemplate;
        [SerializeField, HideInInspector] ItemTemplateId itemTemplateId;
        [SerializeField] Transform spawnPoint;

        IItem IItemGimmick.Item => item != null ? item : item = GetComponent<Item.Implements.Item>();
        Target IGimmick.Target => key.Target;
        string IGimmick.Key => key.Key;
        ParameterType IGimmick.ParameterType => ParameterType.Signal;
        public Transform SpawnPoint => spawnPoint;

        public IItem ItemTemplate => itemTemplate;
        public ItemTemplateId ItemTemplateId
        {
            get => itemTemplateId;
#if UNITY_EDITOR
            set => itemTemplateId = value;
#endif
        }


        public event CreateItemEventHandler OnCreateItem;

        DateTime lastTriggeredAt;

        void Start()
        {
            if (item == null) item = GetComponent<Item.Implements.Item>();
        }

        public void Run(GimmickValue value, DateTime current)
        {
            if (value.TimeStamp <= lastTriggeredAt) return;
            lastTriggeredAt = value.TimeStamp;
            if ((current - value.TimeStamp).TotalSeconds > Constants.Gimmick.TriggerExpireSeconds) return;

            var useSpawnPoint = spawnPoint != null ? spawnPoint : transform;
            var args = new CreateItemEventArgs
            {
                SenderId = item.Id,
                TemplateId = itemTemplateId,
                Position = useSpawnPoint.position,
                Rotation = useSpawnPoint.rotation
            };
            OnCreateItem?.Invoke(args);
        }

        void Reset()
        {
            item = GetComponent<Item.Implements.Item>();
        }

        void OnValidate()
        {
            if (item == null || item.gameObject != gameObject) item = GetComponent<Item.Implements.Item>();
            if (spawnPoint == null) spawnPoint = transform;
        }
    }
}