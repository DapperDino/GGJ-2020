using DapperDino.GGJ2020.Parts;
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
        private static GameObject EnemyObject;

        private Vector3[] spawnpoints;
        internal int EnemiesSpawnCount { get; set; }
        int enemiesAlive = 0;

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

        private void Start()
        {
            var props = GetComponent<RoomProperties>();
            spawnpoints = props.SpawnPoints.Select(x => x.position).ToArray();
        }

        public void Activate()
        {
            if (IsCleared) return;
            if (EnemiesSpawnCount <= 0) return;
            if (EnemyObject == null) EnemyObject = Resources.Load<GameObject>("Enemy_resource");

            foreach(var door in Node.doors)
            {
                door.SetDoorsOpen(false);
            }

            enemiesAlive += EnemiesSpawnCount;
            for (int i = 0; i < EnemiesSpawnCount; i++)
            {
                var j = UnityEngine.Random.Range(0, spawnpoints.Length);
                Debug.Log($"Spawned enemy {i}");
                var obj = Instantiate(EnemyObject,
                    spawnpoints[j],
                    Quaternion.identity);

                var deathHandler = obj.GetComponent<CoreDeathHandler>();
                deathHandler.OnCoreDeath.AddListener(EnemyDied);
            }
            EnemiesSpawnCount = 0;
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

        //public void OnTriggerStay(Collider other)
        //{
        //    if (other.CompareTag("Player"))
        //    {
        //        if (enemiesAlive <= 0)
        //            Deactivate();
        //    }
        //}

        private void EnemyDied()
        {
            enemiesAlive--;
            IsCleared = (enemiesAlive <= 0);
        }
    }

}