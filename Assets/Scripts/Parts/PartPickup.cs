using DapperDino.GGJ2020.Items;
using UnityEngine;

namespace DapperDino.GGJ2020.Parts
{
    public class PartPickup : MonoBehaviour
    {
        [SerializeField] private float radius = 3f;
        [SerializeField] private InventoryBehaviour inventoryBehaviour = null;

        public void Pickup()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            foreach (var collider in colliders)
            {
                if (!collider.TryGetComponent<ItemPickupBehaviour>(out var itemPickupBehaviour))
                {
                    continue;
                }

                if (!inventoryBehaviour.Inventory.AddItem(itemPickupBehaviour.Item))
                {
                    return;
                }

                Destroy(itemPickupBehaviour.gameObject);

                return;
            }
        }

        public void Equip()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            foreach (var collider in colliders)
            {
                if (!collider.TryGetComponent<ItemPickupBehaviour>(out var itemPickupBehaviour))
                {
                    continue;
                }

                if (!inventoryBehaviour.Inventory.AddEquipment(itemPickupBehaviour.Item))
                {
                    return;
                }

                Destroy(itemPickupBehaviour.gameObject);

                return;
            }
        }
    }
}
