using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is simply exists for exposing where doors should be.
/// </summary>
public class Room : MonoBehaviour
{
    private Node node;
    internal Node Node {
        get
        {
            return node;
        }
        set
        {
            node = value;
            Name = node.Name;
            RoomFlags = node.RoomFlags;
            TotalFlags = (int)RoomFlags;
        } 
    }
    [SerializeField] public string Name;
    [SerializeField] public RoomFlags RoomFlags;
    [SerializeField] public int TotalFlags;
}
