using UnityEngine;
using UnityEngine.SceneManagement;

namespace DapperDino.GGJ2020.Utilities.ConsoleCommands.Commands
{
    [CreateAssetMenu(fileName = "New Scene Command", menuName = "Utilities/Console Commands/Scene Command")]
    public class SceneCommand : ConsoleCommand
    {
        protected override bool Process(string[] args)
        {
            if (args.Length != 1) { return false; }

            if (!int.TryParse(args[0], out var index))
            {
                return false;
            }

            SceneManager.LoadScene(index);

            Time.timeScale = 1f;

            return true;
        }
    }
}
