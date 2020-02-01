using UnityEngine;

namespace DapperDino.GGJ2020.Items
{
    public class EquipmentDragHandler : ItemDragHandler
    {
        [SerializeField] private EquipmentSlotBehaviour equipmentSlotBehaviour = null;

        public EquipmentSlotBehaviour EquipmentSlotBehaviour => equipmentSlotBehaviour;
    }
}
