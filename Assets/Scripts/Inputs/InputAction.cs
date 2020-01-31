using UnityEngine;
using UnityEngine.InputSystem;

namespace DapperDino.GGJ2020.Inputs
{
    [CreateAssetMenu(fileName = "New Input Action", menuName = "Inputs/Input Action")]
    public class InputAction : ScriptableObject
    {
        [SerializeField] private InputActionReference inputAction = null;

        public bool Value => inputAction.action.triggered;
    }
}
