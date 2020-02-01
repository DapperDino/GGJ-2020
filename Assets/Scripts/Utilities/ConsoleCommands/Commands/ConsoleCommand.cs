using UnityEngine;

namespace DapperDino.GGJ2020.Utilities.ConsoleCommands.Commands
{
    public abstract class ConsoleCommand : ScriptableObject
    {
        [SerializeField] private string commandWord = string.Empty;

        public bool Process(string commandInput, string[] args)
        {
            if (!commandInput.Equals(commandWord, System.StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return Process(args);
        }

        protected abstract bool Process(string[] args);

        protected GameObject GetPlayer() => GameObject.FindWithTag("Player");
    }
}
