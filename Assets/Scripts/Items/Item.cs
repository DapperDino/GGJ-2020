using DapperDino.GGJ2020.Parts;
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

        public string Name => template.name;
        public PartType PartType => template.PartType;
        public Sprite Icon => template.Icon;
    }
}
