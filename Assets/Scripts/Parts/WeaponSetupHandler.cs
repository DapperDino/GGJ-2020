using UnityEngine;

namespace DapperDino.GGJ2020.Parts
{
    public class WeaponSetupHandler : MonoBehaviour
    {
        [SerializeField] private PartBehaviour partBehaviour = null;

        private WeaponInputReceiver weaponInputReceiver;

        public void Fire()
        {
            if (weaponInputReceiver == null) { return; }

            weaponInputReceiver.Fire();
        }

        public void SetUp()
        {
            if (!partBehaviour.PartInstance.TryGetComponent<WeaponInputReceiver>(out var weaponInputReceiver))
            {
                return;
            }

            this.weaponInputReceiver = weaponInputReceiver;
        }

        public void Clear()
        {
            weaponInputReceiver = null;
        }
    }
}
