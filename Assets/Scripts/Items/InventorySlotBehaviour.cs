using UnityEngine.EventSystems;

namespace DapperDino.GGJ2020.Items
{
    public class InventorySlotBehaviour : SlotBehaviour, IDropHandler
    {
        public int SlotIndex { get; private set; }

        private void Start()
        {
            SlotIndex = transform.GetSiblingIndex();
        }

        private void OnEnable()
        {
            UpdateSlot(inventoryBehaviour.Inventory.Items[SlotIndex]);

            inventoryBehaviour.Inventory.OnInventorySlotUpdate += UpdateSlot;
        }

        private void OnDisable()
        {
            inventoryBehaviour.Inventory.OnInventorySlotUpdate -= UpdateSlot;
        }

        private void UpdateSlot(int slotIndex, Item item)
        {
            if (SlotIndex != slotIndex) { return; }

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
                inventoryBehaviour.Inventory.Swap(inventoryDragHandler.InventorySlotBehaviour.SlotIndex, SlotIndex);

                return;
            }

            if (dragHandler is EquipmentDragHandler equipmentDragHandler)
            {
                inventoryBehaviour.Inventory.Swap(equipmentDragHandler.EquipmentSlotBehaviour.PartType, SlotIndex);

                return;
            }
        }
    }
}
