using UnityEngine;

namespace DapperDino.GGJ2020.Combat
{
    public interface IDamageable
    {
        void DealDamage(int damage, Vector3 direction);
    }
}
