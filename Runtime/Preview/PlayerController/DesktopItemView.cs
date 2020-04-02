using System;
using ClusterVR.CreatorKit.Item;
using UnityEngine;
using UnityEngine.UI;

namespace ClusterVR.CreatorKit.Preview.PlayerController
{
    // 掴んでいるアイテムに関連したUIです。
    public class DesktopItemView : MonoBehaviour
    {
        [SerializeField] Button releaseButton;
        [SerializeField] GameObject itemLabel;
        [SerializeField] Text itemLabelText;

        public event Action OnRelease;

        void Start()
        {
            releaseButton.onClick.AddListener(() => OnRelease?.Invoke());
        }

        public void SetGrabbingItem(IGrabbableItem item)
        {
            if (item == null)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
                var itemName = item.MovableItem.Item.ItemName;
                itemLabel.SetActive(!string.IsNullOrEmpty(itemName));
                itemLabelText.text = itemName;
            }
        }
    }
}
