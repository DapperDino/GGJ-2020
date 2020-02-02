using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DapperDino.GGJ2020.Parts
{
    public class ProjectileLauncher : MonoBehaviour
    {
        [SerializeField] private GameObject prefab = null;
        [SerializeField] private Transform launchPoint = null;
        [SerializeField] private Vector3 velocity = new Vector3();

        public void Launch()
        {
            GameObject projectile = Instantiate(prefab, launchPoint.position, Quaternion.LookRotation(launchPoint.forward));

            if (!projectile.TryGetComponent<Rigidbody>(out var rb))
            {
                return;
            }

            rb.velocity = launchPoint.TransformDirection(velocity);
        }
    }
}
