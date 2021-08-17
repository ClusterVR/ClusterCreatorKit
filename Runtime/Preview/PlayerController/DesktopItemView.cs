#if UNITY_EDITOR
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Trigger;
using UnityEngine;
using UnityEngine.UI;

namespace ClusterVR.CreatorKit.Preview.PlayerController
{
    public sealed class DesktopItemView : MonoBehaviour
    {
        [SerializeField] Text itemLabelText;
        [SerializeField] GameObject useTooltip;

        public void SetGrabbingItem(IGrabbableItem item)
        {
            if (item == null)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
                var itemName = item.Item.ItemName;
                if (string.IsNullOrEmpty(itemName))
                {
                    itemName = "アイテム";
                }
                itemLabelText.text = itemName;
                useTooltip.SetActive(item.Item.gameObject.GetComponent<IUseItemTrigger>() != null);
            }
        }
    }
}
#endif
