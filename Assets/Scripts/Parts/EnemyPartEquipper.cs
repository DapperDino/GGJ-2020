using DapperDino.GGJ2020.Items;
using UnityEngine;

namespace DapperDino.GGJ2020.Parts
{
    public class EnemyPartEquipper : MonoBehaviour
    {
        [SerializeField] private InventoryBehaviour inventoryBehaviour = null;
        [SerializeField] private ItemTemplate[] items = new ItemTemplate[0];

        private void Start()
        {
            foreach (var item in items)
            {
                inventoryBehaviour.Inventory.AddEquipment(item);
            }
        }
    }
}
