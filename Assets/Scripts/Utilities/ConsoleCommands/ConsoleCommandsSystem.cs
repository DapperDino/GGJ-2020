using DapperDino.GGJ2020.Utilities.ConsoleCommands.Commands;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DapperDino.GGJ2020.Utilities.ConsoleCommands
{
    public class ConsoleCommandsSystem : MonoBehaviour
    {
        [SerializeField] private string commandPrefix = "/";
        [SerializeField] private ConsoleCommand[] commands = new ConsoleCommand[0];

        [Header("UI")]
        [SerializeField] private GameObject uiCanvas = null;
        [SerializeField] private TMP_InputField inputField = null;

        private float pausedTimeScale;

        public void Toggle()
        {
            if (uiCanvas.activeSelf)
            {
                Time.timeScale = pausedTimeScale;
                uiCanvas.SetActive(false);
            }
            else
            {
                pausedTimeScale = Time.timeScale;
                Time.timeScale = 0;
                uiCanvas.SetActive(true);
                inputField.ActivateInputField();
            }
        }

        public void ProcessCommand(string inputValue)
        {
            inputField.text = string.Empty;

            if (!inputValue.StartsWith(commandPrefix)) { return; }

            inputValue = inputValue.Remove(0, commandPrefix.Length);

            string[] inputSplit = inputValue.Split(' ');

            string commandInput = inputSplit[0];
            string[] args = inputSplit.Skip(1).ToArray();

            CheckForCommands(commandInput, args);
        }

        private void CheckForCommands(string commandInput, string[] args)
        {
            foreach (var command in commands)
            {
                if (command.Process(commandInput, args))
                {
                    return;
                }
            }
        }
    }
}
