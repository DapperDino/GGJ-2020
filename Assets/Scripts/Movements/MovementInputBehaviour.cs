using UnityEngine;

namespace DapperDino.GGJ2020.Movements
{
    public class MovementInputBehaviour : MonoBehaviour
    {
        [SerializeField] private MovementBehaviour movementBehaviour = null;
        [SerializeField] private float movementSpeed = 10f;

        private MovementInput movementInput;
        public MovementInput MovementInput
        {
            get
            {
                if (movementInput != null) { return movementInput; }
                return movementInput = new MovementInput(movementSpeed);
            }
        }

        private void OnEnable()
        {
            if (movementBehaviour == null) { return; }

            movementBehaviour.Movement.AddModifier(MovementInput);
        }

        private void OnDisable()
        {
            MovementInput.Move(Vector3.zero);

            if (movementBehaviour == null) { return; }

            movementBehaviour.Movement.RemoveModifier(MovementInput);
        }

        public void SetMovementBehaviour(MovementBehaviour movementBehaviour)
        {
            this.movementBehaviour = movementBehaviour;

            if (movementBehaviour == null) { return; }

            movementBehaviour.Movement.AddModifier(MovementInput);
        }
    }
}
