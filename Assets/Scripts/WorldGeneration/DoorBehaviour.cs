using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DapperDino.GGJ2020.World
{
    public class DoorBehaviour : MonoBehaviour
    {
        [SerializeField] private bool RequiresKey = false;
        public bool Open { get; private set; } = false;

        private Vector3 closedPosition;
        [SerializeField] private Vector3 openPosition = new Vector3(0, -1, 0);
        private Collider Collider { get; set; }
        public Room Room { get; set; }


        private void Awake()
        {
            Collider = transform.GetComponentInParent<Collider>();
            closedPosition = transform.localPosition;
            Room = transform.GetComponentInParent<Room>();
            Room.Node.doors.Add(this);

            if (!RequiresKey)
            {
                Open = true;
                transform.localPosition = openPosition;
                Collider.enabled = false;
            }
        }

        public void Interact()
        {
            //Open = true;
            SetDoorsOpen(!Open, true);
        }

        /// <summary>
        /// Forces the door open, even when locked.
        /// </summary>
        public void ForceOpen()
        {
            RequiresKey = false;
            Open = true;
        }


        public void SetDoorsOpen(bool open, bool attemptToUseKey = false)
        {
            if (Open == open)
                return;

            if (open)
            {
                if (RequiresKey && attemptToUseKey)
                {
                    if (false)// hasKey
                    {
                        Debug.Log("TODO: remove key from inventory");
                        RequiresKey = false;
                    }
                    else
                    {
                        Debug.Log("Player misses a key in their inventory");
                        return;
                    }
                }
                transform.DOLocalMove(openPosition, 1f).OnComplete(() => 
                {
                    Collider.enabled = false; 
                });
            }
            else
            {
                Debug.Log("Closing door!!");
                transform.DOLocalMove(closedPosition, 1f).OnStart(() =>
                {
                    Collider.enabled = true;
                });
            }

            Open = open;
        }

        /*internal DoorBehaviour LinkedDoor { get; set; }
        public void Open()
        {
            if (LinkedDoor == null)
            {
                // Worst case scenario, we check all the neightbor rooms (4) and all their rooms (4) aka. 16 iterations.
                foreach (var neighbor in Room.Node.neighbors)
                {
                    foreach (var door in neighbor.doors)
                    {
                        // There is probably a better way to do this.. honestly
                        if (Vector3.Distance(door.transform.position, transform.position) < 3f)
                        {
                            //Debug.Log("Bingo.");
                            LinkedDoor = door;
                        }
                    }
                }
                // Provide a reference to ourselves in the neighboring door
                LinkedDoor.LinkedDoor = this;
            }
            LinkedDoor.IsOpen = !IsOpen;
            IsOpen = !IsOpen;
        }*/
    }
}