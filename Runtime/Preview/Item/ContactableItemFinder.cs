#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Constants;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Preview.Item
{
    public sealed class ContactableItemFinder : MonoBehaviour
    {
        [SerializeField] Transform center;

        const int InteractableItemLayerMask = LayerName.InteractableItemMask;

        readonly HashSet<IContactableItem> contactableItems = new HashSet<IContactableItem>();
        readonly Collider[] collidings = new Collider[1024];

        public IReadOnlyCollection<IContactableItem> ContactableItems => contactableItems;

        void Update()
        {
            contactableItems.Clear();

            var collidingsCount = Physics.OverlapSphereNonAlloc(center.position, Constants.Constants.ItemContactableRange, collidings,
                InteractableItemLayerMask);
            if (collidingsCount == 0)
            {
                return;
            }

            foreach (var colliding in collidings.Take(collidingsCount))
            {
                var item = colliding.gameObject.GetComponentInParent<IContactableItem>();
                if (item != null && item.IsContactable)
                {
                    contactableItems.Add(item);
                }
            }
        }
    }
}
#endif
