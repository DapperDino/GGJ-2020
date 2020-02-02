using UnityEngine;

namespace DapperDino.GGJ2020.Combat
{
    public class DealDamageOnContact : MonoBehaviour
    {
        [SerializeField] private int damage = 20;
        [SerializeField] private Rigidbody rb = null;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<IDamageable>(out var damageable))
            {
                return;
            }

            damageable.DealDamage(damage, rb.velocity.normalized);
        }
    }
}

