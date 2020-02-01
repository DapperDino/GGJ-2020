using UnityEngine;

namespace DapperDino.GGJ2020.Movements
{
    public interface IMovementModifier
    {
        Vector3 Value { get; }
    }
}
