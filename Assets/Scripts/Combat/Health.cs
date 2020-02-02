using DapperDino.GGJ2020.Items;
using UnityEngine;

namespace DapperDino.GGJ2020.Combat
{
    public class Health : MonoBehaviour
    {
        private Item item;

        public void SetItem(Item item) => this.item = item;

        public void DealDamage(int damage, Vector3 direction)
        {
            item.CurrentHealth = Mathf.Max(item.CurrentHealth - damage, 0);

            if (item.CurrentHealth == 0)
            {
                GameObject droppedItem = item.Inventory.Unequip(item);

                if (droppedItem.TryGetComponent<Rigidbody>(out var rb))
                {
                    rb.AddForce(direction * 10f, ForceMode.VelocityChange);
                }

                Destroy(gameObject);
            }
        }
    }
}
