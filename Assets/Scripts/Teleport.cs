using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject receiver;

    private bool teleportEnabled = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)){
            teleportEnabled = true;
        } else
        {
            teleportEnabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if((go.CompareTag("Body") || go.CompareTag("Legs")) && teleportEnabled)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = receiver.transform.position;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if ((go.CompareTag("Body") || go.CompareTag("Legs")) && teleportEnabled)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = receiver.transform.position;
        }
    }
}


