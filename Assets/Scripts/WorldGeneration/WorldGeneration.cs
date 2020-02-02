using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DapperDino.GGJ2020.World
{
    public class WorldGeneration : MonoBehaviour
    {
        private List<Node> nodes = new List<Node>();
        private static readonly Node startingRoom = new Node() { Name = "StartingRoom", RoomFlags = RoomFlags.NorthDoor, X = 0, Y = 0 };

        [Range(1, 100)] public uint MaxRooms = 3;
        [Range(3, 100)] public int MaxChain = 5;
        private int randomChainLength = 4;
        private int chainLengthCounter = 0;
        private int currentChain = 0;
        private int totalChainLength = 0;

        private int RoomSizeMultiplier = 3; // Do not change! Navmesh baked prefabs won't scale properly!
        private const int RoomSize = 20;

        private RoomProperties[] Rooms;
        private static GameObject Wall;
        private static GameObject Door;
        private static GameObject LockedDoor;


        void Awake()
        {
            DOTween.Init();
            Wall = Resources.Load<GameObject>("Rooms/Wall/Wall");
            Door = Resources.Load<GameObject>("Rooms/Wall/Door");
            LockedDoor = Resources.Load<GameObject>("Rooms/Wall/LockedDoor");

            GenerateLayout();
            GenerateRooms();
        }

        private void GenerateLayout()
        {
            Add(startingRoom);
            var lastNode = nodes[0];
            bool hasKey = false;

            for (int i = 0; i < MaxRooms; i++)
            {

                Node newNode = new Node();

                // Check if the lastNode
                if (lastNode == nodes[0])
                {
                    newNode.X = lastNode.X;
                    newNode.Y = lastNode.Y + 1;
                    newNode.RoomFlags |= RoomFlags.SouthDoor;
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
                        for (int j = 0; j < 4; j++)
                        {
                            newNode.X = lastNode.X;
                            newNode.Y = lastNode.Y;
                            switch (direction[j])
                            {
                                case 0: // North
                                    newNode.Y += 1;
                                    IsValidSpace = IsEmpty(newNode);
                                    if (IsValidSpace)
                                    {
                                        newNode.RoomFlags |= RoomFlags.SouthDoor;
                                        lastNode.RoomFlags |= (hasKey) ? RoomFlags.LockedNorthDoor : RoomFlags.NorthDoor;

                                        hasKey = false;
                                    }
                                    break;
                                case 1: // East
                                    newNode.X += 1;
                                    IsValidSpace = IsEmpty(newNode);
                                    if (IsValidSpace)
                                    {
                                        newNode.RoomFlags |= RoomFlags.WestDoor;
                                        lastNode.RoomFlags |= (hasKey) ? RoomFlags.LockedEastDoor : RoomFlags.EastDoor;

                                        hasKey = false;
                                    }
                                    break;
                                case 2: // South
                                    newNode.Y -= 1;
                                    IsValidSpace = IsEmpty(newNode);
                                    if (IsValidSpace)
                                    {
                                        newNode.RoomFlags |= RoomFlags.NorthDoor;
                                        lastNode.RoomFlags |= (hasKey) ? RoomFlags.LockedSouthDoor : RoomFlags.SouthDoor;

                                        hasKey = false;
                                    }
                                    break;
                                case 3: // West
                                    newNode.X -= 1;
                                    IsValidSpace = IsEmpty(newNode);
                                    if (IsValidSpace)
                                    {
                                        newNode.RoomFlags |= RoomFlags.EastDoor;
                                        lastNode.RoomFlags |= (hasKey) ? RoomFlags.LockedWestDoor : RoomFlags.WestDoor;

                                        hasKey = false;
                                    }
                                    break;
                            }
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
                        {
                            currentChain++;
                            // TODO: make this random so multiple branching paths get generated with open doors.
                            // TODO: remove the current if statement?
                            //if ((lastNode.RoomFlags & RoomFlags.ContainsKey) == 0)
                            {
                                lastNode.RoomFlags |= RoomFlags.ContainsKey;
                                lastNode.Name += "_ContainsKey";
                                hasKey = true;
                            }
                        }
                        else
                        {
                            Debug.Log("Current chain length was zero, finding now point to start");
                        }

                        var totalBackwardsSteps = UnityEngine.Random.Range(1, totalChainLength - 2);
                        // Calculate the length of the current 
                        totalChainLength += chainLengthCounter - totalBackwardsSteps - 1;
                        // Reset the current chain counter
                        chainLengthCounter = 0;
                        // Defines how long the next chain will be
                        randomChainLength = UnityEngine.Random.Range(2, MaxChain);
                        Node node = lastNode;
                        for (int stepsbackward = 0; stepsbackward < totalBackwardsSteps; stepsbackward++)
                        {
                            try
                            {
                                node = node.Parent;
                                Debug.Log(node.Name);
                            }
                            catch (Exception e)
                            {
                                Debug.LogError(e.Message);
                                Debug.Log("Chain length = " + totalChainLength);
                                Debug.Log("Steps backwards" + totalBackwardsSteps);
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
                newNode.Parent = lastNode;
                lastNode.neighbors.Add(newNode);
                Add(newNode);
                lastNode = newNode;
            }
            Debug.Log("<color=green>Finished generating the layout!</color>");
        }

        private void GenerateRooms()
        {
            Rooms = Resources.LoadAll<RoomProperties>("Rooms");
            Debug.Log($"Rooms found in the resources directory: {Rooms.Length}.");

            var roomId = 2;
            int difficulty = 1;
            foreach (var _node in nodes)
            {
                // Check for the kind of room that is required.
                // As in: where we need doors.
                GameObject roomObject = Rooms[roomId].gameObject;
                roomId = UnityEngine.Random.Range(0, Rooms.Length);
                _node.GameObject = Instantiate(roomObject, 
                    new Vector3(_node.X * RoomSize * RoomSizeMultiplier, 0, _node.Y * RoomSize * RoomSizeMultiplier), 
                    roomObject.transform.rotation, this.transform);
                _node.GameObject.name = _node.Name;
                //_node.GameObject.transform.localScale *= RoomSizeMultiplier;
                Room room = _node.GameObject.AddComponent<Room>();
                room.Node = _node;
                // Spawn difficulty /2 enemies.
                room.EnemiesSpawnCount = difficulty / 2;
                difficulty++;


                //Place the walls
                Quaternion wallRotation = Quaternion.identity;
                for (int i = 0; i < 4; i++)
                {
                    var wall = GetWall(_node.RoomFlags, i);
                    Instantiate(wall, _node.GameObject.transform.position + wall.transform.position, wallRotation, _node.GameObject.transform);
                    // Rotate the next wall by 90 degrees
                    wallRotation *= Quaternion.Euler(0, 90, 0);
                }
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

        /// <summary>
        /// Returns the correct wall.
        /// </summary>
        /// <param name="flags">The flags</param>
        /// <param name="direction">0 north, 1 east, 2 south, 3 west</param>
        /// <returns></returns>
        static GameObject GetWall(RoomFlags flags, int direction)
        {
            GameObject wall = Wall;
            switch (direction)
            {
                case 0:
                    if ((flags & RoomFlags.NorthDoor) != 0)
                        wall = Door;
                    if ((flags & RoomFlags.LockedNorthDoor) != 0)
                        wall = LockedDoor;
                    break;
                case 1:
                    if ((flags & RoomFlags.EastDoor) != 0)
                        wall = Door;
                    if ((flags & RoomFlags.LockedEastDoor) != 0)
                        wall = LockedDoor;
                    break;
                case 2:
                    if ((flags & RoomFlags.SouthDoor) != 0)
                        wall = Door;
                    if ((flags & RoomFlags.LockedSouthDoor) != 0)
                        wall = LockedDoor;
                    break;
                case 3:
                    if ((flags & RoomFlags.WestDoor) != 0)
                        wall = Door;
                    if ((flags & RoomFlags.LockedWestDoor) != 0)
                        wall = LockedDoor;
                    break;

            }
            return wall;
        }
        static readonly int[] directions = { 0, 1, 2, 3 };
        static int[] GetRandomDirection()
        {
            return directions.OrderBy(x => UnityEngine.Random.Range(0, 15)).ToArray();
        }
    }

}