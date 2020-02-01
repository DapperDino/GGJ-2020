using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node
{
    public string Name { get; set; } = "";

    // Null means it's the starting point.
    public Node Parent { get; set; }
    public RoomFlags RoomFlags { get; set; } = RoomFlags.None;
    public GameObject GameObject { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}
