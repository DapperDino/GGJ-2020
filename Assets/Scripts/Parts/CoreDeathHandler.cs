using UnityEngine;

namespace DapperDino.GGJ2020.Parts
{
    public class CoreDeathHandler : MonoBehaviour
    {
        [SerializeField] private GameObject character = null;
        [SerializeField] private Transform coreTransform = null;
        [SerializeField] private GameObject explosionEffect = null;
        [SerializeField] private float radius = 5f;
        [SerializeField] private float force = 5f;

        public void Handle()
        {
            Instantiate(explosionEffect, coreTransform.position, Quaternion.identity);
            Destroy(gameObject);

            Collider[] colliders = Physics.OverlapSphere(coreTransform.position, radius);
            foreach (var hit in colliders)
            {
                var rb = hit.GetComponent<Rigidbody>();

                if (rb == null) { continue; }

                rb.AddExplosionForce(force, coreTransform.position, radius, 0f, ForceMode.Impulse);
            }
        }
    }
}
