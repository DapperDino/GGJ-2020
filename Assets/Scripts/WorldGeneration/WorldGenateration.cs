using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldGenateration : MonoBehaviour
{
    private List<Node> nodes = new List<Node>();
    private static Node startingRoom = new Node() { Name="StartingRoom", RoomFlags = RoomFlags.NorthDoor, X = 0, Y = 0 };

    [Range(1, 100)]
    public uint MaxRooms = 3;
    [Range(3, 100)]
    public int MaxChain = 5;
    private int randomChainLength = 4;
    private int chainLengthCounter = 0;
    private int currentChain = 0;
    private int totalChainLength = 0;
    public RoomFlags demo = RoomFlags.ContainsKey;

    private const uint RoomSize = 20;


    void Awake()
    {
        GenerateLayout();
        ApplyRooms();
    }

    private void GenerateLayout()
    {
        Add(startingRoom);
        var lastNode = nodes[0];

        for (int i = 0; i < MaxRooms; i++)
        {
            Node newNode = new Node();
            // Check if the lastNode
            if (lastNode == nodes[0])
            {
                newNode.X = lastNode.X;
                newNode.Y = lastNode.Y + 1;                
            }
            else
            {
                // Continueing forward until the max chain length is reached
                var continueForward = chainLengthCounter <= randomChainLength;
                if (continueForward)
                {
                    var direction = GetRandomDirection();

                    var IsValidSpace = false;
                    // Check the randomized directions
                    for(int j = 0; j < 4; j++)
                    {
                        newNode.X = lastNode.X;
                        newNode.Y = lastNode.Y;
                        switch (direction[j])
                        {
                            case 0: // North
                                newNode.Y += 1;
                                break;
                            case 1: // East
                                newNode.X += 1;
                                break;
                            case 2: // South
                                newNode.Y -= 1;
                                break;
                            case 3: // West
                                newNode.X -= 1;
                                break;
                        }
                        IsValidSpace = IsEmpty(newNode);
                        if (IsValidSpace)
                            break;
                    }
                    
                    if (!IsValidSpace)
                    {
                        // Return
                        Debug.Log("The space isn't valid!!");
                        continueForward = false;
                    }
                    chainLengthCounter++;
                    Debug.Log(chainLengthCounter);
                }
                // Proceed generating from a previous point
                if (!continueForward)
                {
                    if (chainLengthCounter > 1)
                        currentChain++;

                    // ADD key
                    lastNode.RoomFlags |= RoomFlags.ContainsKey;
                    lastNode.Name += "_ContainsKey";

                    var totalBackwardsSteps = UnityEngine.Random.Range(3, totalChainLength - 2);
                    totalChainLength = chainLengthCounter - totalBackwardsSteps;
                    chainLengthCounter = 0;
                    randomChainLength = UnityEngine.Random.Range(2, MaxChain);
                    Node node = lastNode;
                    for (int stepsbackward = 0; stepsbackward < totalBackwardsSteps; stepsbackward++)
                    {
                        try
                        {
                            node = node.Parent;
                            Debug.Log(node.Name);
                        }
                        catch(Exception e)
                        {
                            Debug.LogError(e.Message);
                            Debug.Log(totalBackwardsSteps);
                            Debug.Log(node.Name);
                            return;
                        }
                    }
                    Debug.Log("Proceeding to generate from a previous point.");

                    lastNode = node;
                    i--;
                    continue;
                }

            }
            newNode.Name = $"Node:{currentChain}_{chainLengthCounter}";
            //if ((newNode.RoomFlags & RoomFlags.ContainsKey) != 0)
            //{
            //    newNode.Name += "_ContainsKey";
            //}
            newNode.Parent = lastNode;
            Add(newNode);
            lastNode = newNode;
        }
    }
    private void ApplyRooms()
    {
        foreach(var node in nodes)
        {
            // Check for the kind of room that is required.
            // As in: where we need doors.
            var roomObject = Resources.Load<GameObject>("Rooms/DefaultRoom");
            node.GameObject = Instantiate(roomObject, new Vector3(node.X * RoomSize, 0, node.Y * RoomSize), roomObject.transform.rotation, this.transform);
            node.GameObject.name = node.Name;
            // TODO: remove this
            node.GameObject.transform.localScale = (new Vector3(20, 20, 1));
        }
    }

    bool IsEmpty(Node node) => !nodes.Any(obj => obj.X == node.X && obj.Y == node.Y);

    void Add(Node node)
    {
        var duplicateFound = !IsEmpty(node);
        if (duplicateFound)
            Debug.LogError("Duplicate found");//throw new System.Exception("Duplicate found");

        nodes.Add(node);
    }

    static int[] directions = { 0, 1, 2, 3 };
    static int[] GetRandomDirection()
    {
        return directions.OrderBy(x => UnityEngine.Random.Range(0, 15)).ToArray();
    }
}
