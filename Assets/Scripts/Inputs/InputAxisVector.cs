using UnityEngine;
using UnityEngine.InputSystem;

namespace DapperDino.GGJ2020.Inputs
{
    [CreateAssetMenu(fileName = "New Input Axis Vector", menuName = "Inputs/Input Axis Vector")]
    public class InputAxisVector : ScriptableObject
    {
        [SerializeField] private InputActionReference inputAction = null;

        public Vector2 GetValue() => inputAction.action.ReadValue<Vector2>();
    }
}
