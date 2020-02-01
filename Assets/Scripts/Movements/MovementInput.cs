using UnityEngine;

namespace DapperDino.GGJ2020.Movements
{
    public class MovementInput : IMovementModifier
    {
        private readonly float movementSpeed;

        public MovementInput(float movementSpeed)
        {
            this.movementSpeed = movementSpeed;
        }

        public Vector3 Value { get; private set; }

        public void Move(Vector3 movement) => Value = movement * movementSpeed;
    }
}
