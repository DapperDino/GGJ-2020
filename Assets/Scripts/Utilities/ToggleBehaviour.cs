using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DapperDino.GGJ2020.Utilities
{
    public class ToggleBehaviour : MonoBehaviour
    {
        [SerializeField] private UnityEvent ToggleOn = new UnityEvent();
        [SerializeField] private UnityEvent ToggleOff = new UnityEvent();

        private bool toggled;

        public void Toggle()
        {
            if (toggled)
            {
                ToggleOff.Invoke();              
            }
            else
            {
                ToggleOn.Invoke();
            }

            toggled = !toggled;
        }
    }
}
