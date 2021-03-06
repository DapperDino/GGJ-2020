﻿using DapperDino.GGJ2020.Parts;
using UnityEngine;

namespace DapperDino.GGJ2020.Items
{
    public class Item
    {
        private readonly ItemTemplate template;

        public Item(ItemTemplate template)
        {
            this.template = template;
        }

        public int Id => template.Id;
        public string Name => template.name;
        public float Height => template.Height;
        public PartType PartType => template.PartType;
        public GameObject Prefab => template.Prefab;
        public Sprite Icon => template.Icon;
    }
}
