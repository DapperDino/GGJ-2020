using System;
using UnityEngine;
using UnityEngine.Events;

namespace DapperDino.GGJ2020.Inputs
{
    [Serializable]
    public class InputActionResponse
    {
        [SerializeField] private InputAction action = null;
        [SerializeField] private UnityEvent onAction = null;

        public InputAction Action => action;
        public UnityEvent OnAction => onAction;
    }
}
