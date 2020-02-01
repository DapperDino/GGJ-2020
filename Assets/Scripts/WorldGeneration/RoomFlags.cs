using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DapperDino.GGJ2020.World
{
    [Flags]
    public enum RoomFlags
    {
        NorthDoor = 1 << 0,
        EastDoor = 1 << 1,
        SouthDoor = 1 << 2,
        WestDoor = 1 << 3,
        LockedNorthDoor = 1 << 4,
        LockedEastDoor = 1 << 5,
        LockedSouthDoor = 1 << 6,
        LockedWestDoor = 1 << 7,
        ContainsKey = 1 << 8
    }
}