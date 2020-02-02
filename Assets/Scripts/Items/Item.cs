using DapperDino.GGJ2020.Parts;
using UnityEngine;

namespace DapperDino.GGJ2020.Items
{
    public class Item
    {
        private readonly ItemTemplate template;

        public Item(ItemTemplate template)
        {
            CurrentHealth = template.MaxHealth;

            this.template = template;
        }

        public int Id => template.Id;
        public string Name => template.Name;
        public float Height => template.Height;
        public int CurrentHealth { get; set; }
        public int MaxHealth => template.MaxHealth;
        public PartType PartType => template.PartType;
        public GameObject Prefab => template.Prefab;
        public GameObject PickupPrefab => template.PickupPrefab;
        public Sprite Icon => template.Icon;
        public PartBehaviour PartBehaviour { get; set; }
        public Inventory Inventory { get; set; }
        public int RepairCost => MaxHealth - CurrentHealth;
    }
}
