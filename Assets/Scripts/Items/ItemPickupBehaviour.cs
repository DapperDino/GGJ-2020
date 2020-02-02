using UnityEngine;

namespace DapperDino.GGJ2020.Items
{
    public class ItemPickupBehaviour : MonoBehaviour
    {
        private Item item;

        public void SetItem(Item item) => this.item = item;
    }
}
