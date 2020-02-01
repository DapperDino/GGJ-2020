using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DapperDino.GGJ2020.World
{
    /// <summary>
    /// This class is simply exists for exposing where doors should be.
    /// </summary>
    public class Room : MonoBehaviour
    {
        private Node node;
        internal Node Node
        {
            get
            {
                return node;
            }
            set
            {
                node = value;
                // For debugability sake.
                Name = node.Name;
                RoomFlags = node.RoomFlags;
                TotalFlags = (int)RoomFlags;
                Neighbors = node.neighbors.Select(x => x.Name).ToList();
            }
        }
        // Here for readability
        [SerializeField] internal string Name;
        [SerializeField] internal RoomFlags RoomFlags;
        [SerializeField] internal int TotalFlags;
        [SerializeField] public List<string> Neighbors;

        internal int EnemyCount { get; set; }

        private bool isCleared;

        public bool IsCleared
        {
            get { return isCleared; }
            set 
            { 
                isCleared = value;
                if (isCleared)
                    Deactivate();
            }
        }


        public void Activate()
        {
            if (IsCleared)
                return;

            foreach(var door in Node.doors)
            {
                door.SetDoorsOpen(false);
            }
        }

        public void Deactivate()
        {
            foreach(var door in Node.doors)
            {
                door.SetDoorsOpen(true);
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                Activate();
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
                Deactivate();
        }
    }

}