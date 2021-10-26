using System;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [RequireComponent(typeof(CharacterItem)), DisallowMultipleComponent]
    public sealed class SetVelocityCharacterItemGimmick : MonoBehaviour, IItemGimmick
    {
        [SerializeField, HideInInspector] CharacterItem characterItem;
        [SerializeField, ItemGimmickKey] GimmickKey key = new GimmickKey(GimmickTarget.Item);

        [SerializeField] Transform space;
        [SerializeField] float scaleFactor = 1f;

        ItemId IGimmick.ItemId =>
            (characterItem != null ? characterItem.Item : (characterItem = GetComponent<CharacterItem>()).Item).Id;

        GimmickTarget IGimmick.Target => key.Target;
        string IGimmick.Key => key.Key;
        ParameterType IGimmick.ParameterType => ParameterType.Vector2;

        Vector2 currentValue;

        void Start()
        {
            if (characterItem == null)
            {
                characterItem = GetComponent<CharacterItem>();
            }

            if (space == null)
            {
                space = transform;
            }
        }

        public void Run(GimmickValue value, DateTime _)
        {
            currentValue = value.Vector2Value;
            UpdateVelocity();
        }

        void Update()
        {
            UpdateVelocity();
        }

        void UpdateVelocity()
        {
            if (space == null)
            {
                return;
            }

            var direction = space.TransformDirection(new Vector3(currentValue.x, 0, currentValue.y));
            characterItem.SetVelocityXZ(new Vector2(direction.x, direction.z) * scaleFactor);
        }

        void OnValidate()
        {
            if (characterItem == null || characterItem.gameObject != gameObject)
            {
                characterItem = GetComponent<CharacterItem>();
            }
        }

        void Reset()
        {
            characterItem = GetComponent<CharacterItem>();

            if (space == null)
            {
                space = transform;
            }
        }
    }
}
