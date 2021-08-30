﻿using System;
using System.Collections.Generic;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Item;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ClusterVR.CreatorKit.Editor.Preview.Item
{
    public sealed class ItemCreator
    {
        readonly Dictionary<ItemTemplateId, IItem> itemTemplates = new Dictionary<ItemTemplateId, IItem>();
        public event Action<IItem> OnCreate;
        public event Action<IItem> OnCreateCompleted;

        public ItemCreator(IEnumerable<ICreateItemGimmick> createItemGimmicks)
        {
            foreach (var createItemGimmick in createItemGimmicks)
            {
                AddItemTemplate(createItemGimmick);
            }
        }

        void AddItemTemplate(ICreateItemGimmick gimmick)
        {
            if (!gimmick.IsValid())
            {
                return;
            }
            var templateId = gimmick.ItemTemplateId;
            if (itemTemplates.ContainsKey(templateId))
            {
                return;
            }
            var template = gimmick.ItemTemplate;
            itemTemplates.Add(templateId, template);
            foreach (var descendantGimmick in template.gameObject.GetComponents<ICreateItemGimmick>())
            {
                AddItemTemplate(descendantGimmick);
            }
        }

        public void Create(ItemTemplateId templateId, Vector3 position, Quaternion rotation)
        {
            if (!itemTemplates.TryGetValue(templateId, out var itemTemplate))
            {
                return;
            }
            if (itemTemplate.gameObject == null)
            {
                return;
            }
            var createdGameObject = Object.Instantiate(itemTemplate.gameObject, position, rotation);
            var createdItem = createdGameObject.GetComponent<IItem>();
            createdItem.Id = ItemId.Create(); // todo: 重複チェック
            OnCreate?.Invoke(createdItem);
            OnCreateCompleted?.Invoke(createdItem);
        }
    }
}
