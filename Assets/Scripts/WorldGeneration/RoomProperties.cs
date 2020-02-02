using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DapperDino.GGJ2020.World
{
    public class RoomProperties : MonoBehaviour
    {
        // Read for the possible door locations
        // Only 0-3 are read.
        public RoomFlags RoomFlags;

        [SerializeField] internal Transform[] SpawnPoints; 
    }
}