using DapperDino.GGJ2020.Items;
using DapperDino.GGJ2020.Utilities.ConsoleCommands.Commands;
using UnityEngine;

namespace DapperDino.Hel.Utilities.ConsoleCommands.Commands
{
    [CreateAssetMenu(fileName = "New Test Command", menuName = "Utilities/Console Commands/Test Command")]
    public class TestCommand : ConsoleCommand
    {
        [SerializeField] private ItemDatabase itemDatabase = null;

        protected override bool Process(string[] args)
        {
            if (args.Length != 1) { return false; }

            if(!int.TryParse(args[0], out var count))
            {
                return false;
            }

            if (!GetPlayer().TryGetComponent<InventoryBehaviour>(out var inventoryBehaviour))
            {
                return false;
            }

            for (int i = 0; i < count; i++)
            {
                inventoryBehaviour.Inventory.AddItem(itemDatabase.GetItemById(i + 1));
            }

            return true;
        }
    }
}
