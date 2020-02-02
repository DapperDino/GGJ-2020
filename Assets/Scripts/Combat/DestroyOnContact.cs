using UnityEngine;

namespace DapperDino.GGJ2020.Combat
{
    public class DestroyOnContact : MonoBehaviour
    {
        [SerializeField] private Transform trail = null;

        private void Start() => Destroy(gameObject, 5f);

        private void OnTriggerEnter(Collider other)
        {
            if(trail != null)
            {
                trail.SetParent(null);
            }

            Destroy(gameObject);
        }
    }
}
