using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DapperDino.GGJ2020.Items
{
    [CreateAssetMenu(fileName = "New Item Database", menuName = "Items/Item Database")]
    public class ItemDatabase : ScriptableObject
    {
        [SerializeField] private List<ItemTemplate> items = new List<ItemTemplate>();

        [ContextMenu("CheckItems")]
        public void CheckItems()
        {
            var usedIds = new List<int>();

            foreach(var item in items)
            {
                if (usedIds.Contains(item.Id))
                {
                    Debug.LogWarning($"Duplicate Ids for items '{item.Name}' and '{items.First(x => x.Id == item.Id).Name}'");
                    return;
                }

                usedIds.Add(item.Id);
            }
        }

        public ItemTemplate GetItemByName(string name)
        {
            return items.FirstOrDefault(x => x.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
        }
    }
}
