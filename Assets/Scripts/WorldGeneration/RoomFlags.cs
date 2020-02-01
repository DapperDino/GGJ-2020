using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum RoomFlags
{
    None = 1 << 0, // It should never be none
    NorthDoor = 1 << 1,
    EastDoor = 1 << 2,
    SouthDoor = 1 << 3,
    WestDoor = 1 << 4,
    LockedNorthDoor = 1 << 5,
    LockedEastDoor = 1 << 6,
    LockedSouthDoor = 1 << 7,
    LockedWestDoor = 1 << 8,
    ContainsKey = 1 << 9
}