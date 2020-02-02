using DapperDino.GGJ2020.ScriptableEvents.Events;
using UnityEngine;

namespace DapperDino.GGJ2020.Combat
{
    public class DieFromFall : MonoBehaviour
    {
        [SerializeField] private float killHeight = -10f;
        [SerializeField] private VoidEvent onDeath = null;

        private void Update()
        {
            if (transform.position.y <= killHeight)
            {
                onDeath.Raise();
                Destroy(gameObject);
            }
        }
    }
}
