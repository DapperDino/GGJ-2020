using DapperDino.GGJ2020.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DapperDino.GGJ2020.Items
{
    public class Inventory
    {
        private readonly PartType torsoType;

        public Inventory(PartType torsoType, int size)
        {
            this.torsoType = torsoType;
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

                item.Inventory = this;
                Items[i] = item;

                OnInventorySlotUpdate(i, item);

                return true;
            }

            return false;
        }

        public bool AddEquipment(ItemTemplate itemTemplate) => AddEquipment(new Item(itemTemplate));

        public bool AddEquipment(Item newItem)
        {
            Item item = GetItemByPartType(newItem.PartType);

            if (item != null) { return false; }

            if (newItem.PartType != torsoType)
            {
                if (GetItemByPartType(torsoType) == null)
                {
                    return false;
                }
            }

            newItem.Inventory = this;
            equipment[newItem.PartType] = newItem;

            OnEquipmentSlotUpdate(newItem.PartType, newItem);

            return true;
        }

        public GameObject Unequip(Item itemToUnequip)
        {
            var item = GetItemByPartType(itemToUnequip.PartType);

            if (item.Id != itemToUnequip.Id) { return null; }

            equipment[item.PartType].Inventory = null;
            equipment[item.PartType] = null;

            OnEquipmentSlotUpdate(item.PartType, equipment[item.PartType]);

            var pickupInstance = Object.Instantiate(item.PickupPrefab, item.PartBehaviour.transform.position, item.PartBehaviour.transform.rotation);

            if (!pickupInstance.TryGetComponent<ItemPickupBehaviour>(out var itemPickupBehaviour))
            {
                return pickupInstance;
            }

            itemPickupBehaviour.SetItem(item);

            if (item.PartType != torsoType) { return pickupInstance; }

            for (int i = 0; i < equipment.Count; i++)
            {
                var piece = equipment.Values.ElementAt(i);

                if (piece == null) { continue; }

                Unequip(piece);
            }

            return pickupInstance;
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

            if (inventoryItem.CurrentHealth == 0) { return; }

            if (partType != inventoryItem.PartType) { return; }

            if (inventoryItem.PartType != torsoType)
            {
                if (GetItemByPartType(torsoType) == null)
                {
                    return;
                }
            }

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

                if (inventoryItem.CurrentHealth == 0) { return; }

                Item tempItem = partItem;

                equipment[partType] = inventoryItem;
                Items[itemIndex] = tempItem;
            }
            else
            {
                Items[itemIndex] = partItem;
                equipment[partType] = null;

                if (partType == torsoType)
                {
                    for (int i = 0; i < equipment.Count; i++)
                    {
                        var piece = equipment.Values.ElementAt(i);

                        if (piece == null) { continue; }

                        Unequip(piece);
                    }
                }
            }

            OnInventorySlotUpdate(itemIndex, Items[itemIndex]);
            OnEquipmentSlotUpdate(partType, equipment[partType]);
        }
    }
}
