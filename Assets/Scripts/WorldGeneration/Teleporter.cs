using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private static Teleporter StartingTeleporter { get;set; }
    private static Teleporter LastTeleporter { get; set; }

    private void Start()
    {
        if (StartingTeleporter == null)
            StartingTeleporter = this;
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!StartingTeleporter.gameObject.activeSelf)
            {
                StartingTeleporter.gameObject.SetActive(true);
                Debug.Log(StartingTeleporter.transform.parent.name);
            }

            Teleporter destination = (this == StartingTeleporter) ? LastTeleporter : StartingTeleporter;
            LastTeleporter = this;

            Debug.Log($"Telporting the player to {destination.transform.parent.name}");
            var controller = other.GetComponent<CharacterController>();
            controller.enabled = false;
            other.transform.position = destination.transform.position + destination.transform.forward * 3 + destination.transform.up * 3;
            controller.enabled = true;
        }
    }

    public void EnableTeleporter()
    {
        gameObject.SetActive(true);
    }
}
