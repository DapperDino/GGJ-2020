using Cinemachine;
using DapperDino.GGJ2020.DataTypes;
using UnityEngine;

namespace DapperDino.GGJ2020.Movements
{
    public class FreeLookInputSystemBinding : MonoBehaviour
    {
        [SerializeField] private CinemachineFreeLook freeLook = null;

        public void SetLookDelta(SerializableVector2 value)
        {
            freeLook.m_XAxis.m_InputAxisValue = value.x;
            freeLook.m_YAxis.m_InputAxisValue = value.y;
        }
    }
}