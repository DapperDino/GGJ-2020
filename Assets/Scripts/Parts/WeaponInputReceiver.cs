using UnityEngine;
using UnityEngine.Events;

namespace DapperDino.GGJ2020.Parts
{
    public class WeaponInputReceiver : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnFire = new UnityEvent();

        public void Fire() => OnFire.Invoke();
    }
}
