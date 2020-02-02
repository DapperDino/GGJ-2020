﻿using DapperDino.GGJ2020.Combat;
using DapperDino.GGJ2020.Items;
using UnityEngine;
using UnityEngine.Events;

namespace DapperDino.GGJ2020.Parts
{
    public class PartBehaviour : MonoBehaviour
    {
        [SerializeField] private PartType partType = null;
        [SerializeField] private Transform partAttatchTransform = null;
        [SerializeField] private InventoryBehaviour inventoryBehaviour = null;

        [SerializeField] private UnityEvent OnItemEquipped = new UnityEvent();
        [SerializeField] private UnityEvent OnItemUnequipped = new UnityEvent();

        public GameObject PartInstance { get; private set; }

        private void OnEnable()
        {
            inventoryBehaviour.Inventory.OnEquipmentSlotUpdate += HandleEquipmentUpdated;
        }

        private void OnDisable()
        {
            inventoryBehaviour.Inventory.OnEquipmentSlotUpdate -= HandleEquipmentUpdated;
        }

        private void HandleEquipmentUpdated(PartType partType, Item item)
        {
            if (this.partType != partType) { return; }

            if (item == null)
            {
                if (PartInstance != null)
                {
                    Destroy(PartInstance);
                }

                OnItemUnequipped.Invoke();

                return;
            }

            if (PartInstance != null)
            {
                item.PartBehaviour = null;

                Destroy(PartInstance);
            }

            PartInstance = Instantiate(item.Prefab, partAttatchTransform);
            item.PartBehaviour = this;

            if (PartInstance.TryGetComponent<Health>(out var health))
            {
                health.SetItem(item);
            }

            OnItemEquipped.Invoke();
        }
    }
}
