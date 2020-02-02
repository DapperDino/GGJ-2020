using DapperDino.GGJ2020.Parts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DapperDino.GGJ2020.Items
{
    public class EquipmentSlotBehaviour : SlotBehaviour
    {
        [SerializeField] private PartType partType = null;

        public PartType PartType => partType;

        public override Item Item => inventoryBehaviour.Inventory.GetItemByPartType(PartType);

        private void OnEnable()
        {
            UpdateSlot(PartType, inventoryBehaviour.Inventory.GetItemByPartType(PartType));

            inventoryBehaviour.Inventory.OnEquipmentSlotUpdate += UpdateSlot;
        }

        private void OnDisable()
        {
            inventoryBehaviour.Inventory.OnEquipmentSlotUpdate -= UpdateSlot;
        }

        private void UpdateSlot(PartType partType, Item item)
        {
            if (PartType != partType) { return; }

            UpdateSlot(item);
        }

        public override void OnDrop(PointerEventData eventData)
        {
            if (!eventData.pointerDrag.TryGetComponent<ItemDragHandler>(out var dragHandler))
            {
                return;
            }

            if (dragHandler is InventoryDragHandler inventoryDragHandler)
            {
                inventoryBehaviour.Inventory.Swap(inventoryDragHandler.InventorySlotBehaviour.SlotIndex, PartType);

                return;
            }

            if (dragHandler is EquipmentDragHandler)
            {
                return;
            }
        }
    }
}
