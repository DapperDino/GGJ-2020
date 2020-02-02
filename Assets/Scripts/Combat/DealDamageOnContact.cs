using UnityEngine;

namespace DapperDino.GGJ2020.Combat
{
    public class DealDamageOnContact : MonoBehaviour
    {
        [SerializeField] private int damage = 20;
        [SerializeField] private Rigidbody rb = null;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<Health>(out var health))
            {
                return;
            }

            health.DealDamage(damage, rb.velocity.normalized);
        }
    }
}

