using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject receiver;

    private bool teleportEnabled = false;

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && teleportEnabled) {
            if (receiver == null)
            {
                Debug.Log("Level Not Yet Available"); //MAKE ACTUAL PROMPT
            }
            else
            {
                GameObject.FindGameObjectWithTag("Player").transform.position = receiver.transform.position;
            }
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.CompareTag("Body") || go.CompareTag("Legs"))
        {
            teleportEnabled = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.CompareTag("Body") || go.CompareTag("Legs"))
        {
            teleportEnabled = false;
        }
    }
}


