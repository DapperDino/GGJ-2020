﻿using UnityEngine;

namespace DapperDino.GGJ2020.Movements
{
    public class GravityBehaviour : MonoBehaviour
    {
        [SerializeField] private MovementBehaviour movementBehaviour = null;
        [SerializeField] private CharacterController controller = null;

        private Gravity gravity;
        private Gravity Gravity
        {
            get
            {
                if (gravity != null) { return gravity; }
                return gravity = new Gravity(controller, Physics.gravity.y);
            }
        }

        private void OnEnable() => movementBehaviour.Movement.AddModifier(Gravity);
        private void FixedUpdate() => Gravity.Tick(Time.fixedDeltaTime);
        private void OnDisable() => movementBehaviour.Movement.RemoveModifier(Gravity);
    }
}
