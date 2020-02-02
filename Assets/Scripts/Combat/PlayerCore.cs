using DapperDino.GGJ2020.ScriptableEvents.Events;
using UnityEngine;

namespace DapperDino.GGJ2020.Combat
{
    public class PlayerCore : MonoBehaviour, IDamageable
    {
        [SerializeField] private VoidEvent OnDeath = null;

        private int health = 100;

        public void DealDamage(int damage, Vector3 direction)
        {
            health = Mathf.Max(health - damage, 0);

            if(health != 0) { return; }

            OnDeath.Raise();
        }
    }
}
