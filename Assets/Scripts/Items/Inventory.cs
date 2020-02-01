using DapperDino.GGJ2020.Parts;
using System;
using System.Collections.Generic;

namespace DapperDino.GGJ2020.Items
{
    public class Inventory
    {
        public Inventory(int size)
        {
            Items = new Item[size];
        }

        public event Action<int, Item> OnInventorySlotUpdate = delegate { };
        public event Action<PartType, Item> OnEquipmentSlotUpdate = delegate { };

        public Item[] Items { get; }

        private readonly Dictionary<PartType, Item> equipment = new Dictionary<PartType, Item>();

        public bool AddItem(ItemTemplate itemTemplate) => AddItem(new Item(itemTemplate));

        public bool AddItem(Item item)
        {
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] != null) { continue; }

                Items[i] = item;

                OnInventorySlotUpdate(i, item);

                return true;
            }

            return false;
        }

        public Item GetItemByPartType(PartType partType)
        {
            if (equipment.TryGetValue(partType, out Item item))
            {
                return item;
            }

            return null;
        }

        public void Swap(int indexOne, int indexTwo)
        {
            Item itemOne = Items[indexOne];
            Item itemTwo = Items[indexTwo];

            if (itemTwo != null)
            {
                Item tempItem = itemOne;

                Items[indexOne] = itemTwo;
                Items[indexTwo] = tempItem;
            }
            else
            {
                Items[indexTwo] = itemOne;
                Items[indexOne] = null;
            }

            OnInventorySlotUpdate(indexOne, Items[indexOne]);
            OnInventorySlotUpdate(indexTwo, Items[indexTwo]);
        }

        public void Swap(int itemIndex, PartType partType)
        {
            Item inventoryItem = Items[itemIndex];

            if (partType != inventoryItem.PartType) { return; }

            if (equipment.TryGetValue(partType, out Item partItem))
            {
                Item tempItem = partItem;

                equipment[partType] = inventoryItem;
                Items[itemIndex] = tempItem;
            }
            else
            {
                equipment[partType] = inventoryItem;
                Items[itemIndex] = null;
            }

            OnInventorySlotUpdate(itemIndex, Items[itemIndex]);
            OnEquipmentSlotUpdate(partType, equipment[partType]);
        }

        public void Swap(PartType partType, int itemIndex)
        {
            Item partItem = equipment[partType];
            Item inventoryItem = Items[itemIndex];

            if (inventoryItem != null)
            {
                if (partItem.PartType != inventoryItem.PartType) { return; }

                Item tempItem = partItem;

                equipment[partType] = inventoryItem;
                Items[itemIndex] = tempItem;
            }
            else
            {
                Items[itemIndex] = partItem;
                equipment[partType] = null;
            }

            OnInventorySlotUpdate(itemIndex, Items[itemIndex]);
            OnEquipmentSlotUpdate(partType, equipment[partType]);
        }
    }
}
