using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Constants;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Preview.Item
{
    public sealed class InteractableItemFinder : MonoBehaviour
    {
        [SerializeField] Transform center;
        [SerializeField] float itemInteractableRange;

        const int InteractableItemLayerMask = LayerName.InteractableItemMask;

        readonly HashSet<IGrabbableItem> interactableItems = new HashSet<IGrabbableItem>();
        readonly Collider[] collidings = new Collider[1024];

        public IReadOnlyCollection<IGrabbableItem> InteractableItems => interactableItems;

        void Update()
        {
            interactableItems.Clear();

            var collidingsCount = Physics.OverlapSphereNonAlloc(center.position, itemInteractableRange, collidings, InteractableItemLayerMask);
            if (collidingsCount == 0) return;

            foreach (var colliding in collidings.Take(collidingsCount))
            {
                var item = colliding.gameObject.GetComponentInParent<IGrabbableItem>();
                if (item != null) interactableItems.Add(item);
            }
        }
    }
}
