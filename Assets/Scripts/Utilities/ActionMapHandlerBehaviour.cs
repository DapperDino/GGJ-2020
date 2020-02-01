using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DapperDino.GGJ2020.Utilities
{
    public class ActionMapHandlerBehaviour : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputActionAsset = null;
        [SerializeField] private string actionMapName = "Action Map Name";

        private static readonly Dictionary<string, int> activeHandlers = new Dictionary<string, int>();

        private void OnEnable()
        {
            inputActionAsset.FindActionMap(actionMapName).Disable();

            if (!activeHandlers.TryGetValue(actionMapName, out int count))
            {
                activeHandlers.Add(actionMapName, 1);

                return;
            }

            activeHandlers[actionMapName] += 1;
        }

        private void OnDisable()
        {
            if(activeHandlers[actionMapName] == 1)
            {
                activeHandlers[actionMapName] = 0;

                inputActionAsset.FindActionMap(actionMapName).Enable();

                return;
            }

            activeHandlers[actionMapName] -= 1;
        }
    }
}
