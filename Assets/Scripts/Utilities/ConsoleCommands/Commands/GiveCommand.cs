using DapperDino.GGJ2020.Items;
using DapperDino.GGJ2020.Utilities.ConsoleCommands.Commands;
using UnityEngine;

namespace DapperDino.Hel.Utilities.ConsoleCommands.Commands
{
    [CreateAssetMenu(fileName = "New Give Command", menuName = "Utilities/Console Commands/Give Command")]
    public class GiveCommand : ConsoleCommand
    {
        [SerializeField] private ItemDatabase itemDatabase = null;

        protected override bool Process(string[] args)
        {
            if (args.Length == 0) { return false; }

            string itemName = string.Join(" ", args);

            var item = itemDatabase.GetItemByName(itemName);

            if (item == null) { return false; }

            if (!GetPlayer().TryGetComponent<InventoryBehaviour>(out var inventoryBehaviour))
            {
                return false;
            }

            inventoryBehaviour.Inventory.AddItem(item);

            return true;
        }
    }
}
