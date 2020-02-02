using UnityEngine;

namespace DapperDino.GGJ2020.Movements
{
    public class RotateToVelocity : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb = null;

        void Update() => transform.rotation = Quaternion.LookRotation(rb.velocity);
    }
}
