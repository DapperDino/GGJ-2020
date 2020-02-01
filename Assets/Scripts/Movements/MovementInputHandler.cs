using DapperDino.GGJ2020.DataTypes;
using UnityEngine;

namespace DapperDino.GGJ2020.Movements
{
    public class MovementInputHandler : MonoBehaviour, IMovementProcessor
    {
        [SerializeField] private MovementInputBehaviour movementInputBehaviour = null;

        public void Process(MovementData movementData)
        {
            if (movementData.CameraTransform != null)
            {
                Vector3 right = movementData.CameraTransform.right;
                Vector3 forward = movementData.CameraTransform.forward;

                right.y = 0f;
                forward.y = 0f;

                movementData.Input = (forward * movementData.Input.y) + (right * movementData.Input.x);

                if (movementData.Input != Vector3.zero)
                {
                    movementData.CharacterTransform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementData.Input), 0.1f);
                }
            }

            movementInputBehaviour.MovementInput.Move(movementData.Input);
        }
    }
}
