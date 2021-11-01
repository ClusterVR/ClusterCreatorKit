using System.Collections.Generic;
using ClusterVR.CreatorKit.Editor.Preview.World;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Preview.Common;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Preview.Item
{
    public sealed class RidableItemManager
    {
        const float LengthOfGetOffTimeSec = 0.5f;

        readonly PlayerPresenter playerPresenter;
        public IRidableItem RidingItem { get; private set; }
        float getOffLongPressDurationSec;

        public RidableItemManager(ItemCreator itemCreator, ItemDestroyer itemDestroyer,
            PlayerPresenter playerPresenter, IEnumerable<IRidableItem> ridableItems)
        {
            this.playerPresenter = playerPresenter;
            itemCreator.OnCreate += OnCreate;
            itemDestroyer.OnDestroy += OnDestroy;
            TickGenerator.Instance.OnTick += Tick;
            Register(ridableItems);
        }

        void Register(IEnumerable<IRidableItem> ridableItems)
        {
            foreach (var ridableItem in ridableItems)
            {
                Register(ridableItem);
            }
        }

        void Register(IRidableItem ridableItem)
        {
            ridableItem.OnInvoked += () => OnInvoked(ridableItem);
            var getOffItemGimmicks = ridableItem.Item.gameObject.GetComponents<IGetOffItemGimmick>();
            foreach (var getOffItemGimmick in getOffItemGimmicks)
            {
                getOffItemGimmick.OnGetOff += GetOffIfRiding;
            }
        }

        void OnInvoked(IRidableItem ridableItem)
        {
            if (ridableItem == RidingItem)
            {
                return;
            }

            if (ridableItem.Seat == null)
            {
                return;
            }

            GetOff();
            GetOn(ridableItem);
        }

        void GetOffIfRiding(IRidableItem ridableItem)
        {
            if (RidingItem == ridableItem)
            {
                GetOff();
            }
        }

        void GetOff()
        {
            if (RidingItem == null)
            {
                return;
            }
            var item = RidingItem;
            RidingItem = null;
            getOffLongPressDurationSec = 0f;
            playerPresenter.SetRidingItem(null);
            TryExecuteExitWarp(item);
            item.GetOff();
        }

        bool TryExecuteExitWarp(IRidableItem ridableItem)
        {
            var exitTransform = ridableItem.ExitTransform;
            if (exitTransform == null) return false;
            playerPresenter.WarpTo(exitTransform.position);
            playerPresenter.RotateTo(exitTransform.rotation);
            return true;
        }

        void GetOn(IRidableItem ridableItem)
        {
            RidingItem = ridableItem;
            playerPresenter.SetRidingItem(ridableItem);
            ridableItem.GetOn();
        }

        void OnCreate(IItem item)
        {
            var ridableItem = item.gameObject.GetComponent<IRidableItem>();
            if (ridableItem != null)
            {
                Register(ridableItem);
            }
        }

        void OnDestroy(IItem item)
        {
            if (RidingItem != null && RidingItem.Item == item)
            {
                RidingItem = null;
                playerPresenter.SetRidingItem(null);
            }
        }

        void Tick()
        {
            if (RidingItem == null)
            {
                getOffLongPressDurationSec = 0f;
                return;
            }

            if (RidingItem != null && Input.GetKey(KeyCode.X))
            {
                getOffLongPressDurationSec += Time.deltaTime;
            }
            else
            {
                getOffLongPressDurationSec = 0f;
            }

            if (getOffLongPressDurationSec > LengthOfGetOffTimeSec ||
                RidingItem.Seat == null ||
                !RidingItem.Item.gameObject.activeInHierarchy)
            {
                GetOff();
            }
        }
    }
}
