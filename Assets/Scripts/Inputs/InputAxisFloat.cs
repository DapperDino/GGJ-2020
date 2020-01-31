﻿using UnityEngine;
using UnityEngine.InputSystem;

namespace DapperDino.GGJ2020.Inputs
{
    [CreateAssetMenu(fileName = "New Input Axis Float", menuName = "Inputs/Input Axis Float")]
    public class InputAxisFloat : ScriptableObject
    {
        [SerializeField] private InputActionReference inputAction = null;

        public float GetValue() => inputAction.action.ReadValue<float>();
    }
}
