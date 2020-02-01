using UnityEngine;

namespace DapperDino.GGJ2020.Items
{
    public class InventoryDragHandler : ItemDragHandler
    {
        [SerializeField] private InventorySlotBehaviour inventorySlotBehaviour = null;

        public InventorySlotBehaviour InventorySlotBehaviour => inventorySlotBehaviour;
    }
}
