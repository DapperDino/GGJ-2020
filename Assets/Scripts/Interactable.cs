using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent onInteracted = new UnityEvent();

    private static Transform player;
    // TODO: expand upon this lol.
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;

            if (Vector3.Distance(player.position, transform.position) < 5f)
                onInteracted.Invoke();
        }
    }
}
