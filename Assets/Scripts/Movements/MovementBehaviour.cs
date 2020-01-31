﻿using UnityEngine;

namespace DapperDino.GGJ2020.Movements
{
    public class MovementBehaviour : MonoBehaviour
    {
        [SerializeField] private CharacterController controller = null;

        private Movement movement;
        public Movement Movement
        {
            get
            {
                if (movement != null) { return movement; }
                movement = new Movement(controller);
                return movement;
            }
        }

        private void Update() => Movement.Tick(Time.deltaTime);
    }
}
