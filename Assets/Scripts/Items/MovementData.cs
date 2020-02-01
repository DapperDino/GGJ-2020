using System;
using UnityEngine;
using UnityEngine.Events;

namespace DapperDino.GGJ2020.Movements
{
    [Serializable]
    public struct MovementData
    {
        public Vector3 Input { get; set; }
        public Transform CharacterTransform { get; set; }
        public Transform CameraTransform { get; set; }
    }

    [Serializable]
    public class UnityMovemementDataEvent : UnityEvent<MovementData> { }
}
