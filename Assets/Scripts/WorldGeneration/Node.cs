using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node
{
    public string Name { get; set; } = "";

    public List<Node> neighbors = new List<Node>();
    public List<DoorBehaviour> doors = new List<DoorBehaviour>();
    // Null means it's the starting point.
    public Node Parent { get; set; }
    public RoomFlags RoomFlags { get; set; }
    public GameObject GameObject { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}
