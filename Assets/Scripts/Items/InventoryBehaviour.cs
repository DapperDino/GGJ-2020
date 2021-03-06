﻿using UnityEngine;

namespace DapperDino.GGJ2020.Items
{
    public class InventoryBehaviour : MonoBehaviour
    {
        [SerializeField] private int size = 10;

        private Inventory inventory;
        public Inventory Inventory
        {
            get
            {
                if(inventory != null) { return inventory; }
                return inventory = new Inventory(size);
            }
        }
    }
}

