using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DapperDino.GGJ2020.Parts
{
    public class WeaponSetupHandler : MonoBehaviour
    {
        [SerializeField] private PartBehaviour partBehaviour = null;

        private WeaponInputReceiver weaponInputReceiver;

        public void Fire() => weaponInputReceiver?.Fire();

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
