using UnityEngine;

namespace DapperDino.GGJ2020.Items
{
    public class ItemPickupBehaviour : MonoBehaviour
    {
        public Item Item { get; private set; }

        public void SetItem(Item item) => Item = item;
    }
}
