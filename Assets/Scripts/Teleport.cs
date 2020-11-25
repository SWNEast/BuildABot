using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject receiver;

    private bool teleportEnabled = false;
    public AudioSource teleportSound;
    public bool active;
    public bool levelAvailable;

    public GameObject player;
    private DialogDriver dd;

    private void Start()
    {
        dd = player.GetComponent<DialogDriver>();
        if (!active)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.254902f, 0.254902f, 0.254902f, 1);
        }
    }
    private void Update()
    {

        /*if (!active)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(65, 65, 65, 255);
        }*/

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && teleportEnabled) {
            if (!active)
            {
                teleportEnabled = false;
                dd.Say("This teleporter is off. The switch will in located in another part of this zone.", "player");

                return;
            }
            else if (!levelAvailable)
            {
                dd.Say("This level is not yet available, please check back soon!", "unknown");
                return;
            }

            if (receiver == null)
            {
                Debug.Log("Level Not Yet Available"); //MAKE ACTUAL PROMPT
            }
            else
            {
                teleportSound.Play();
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

    public void SetActive(bool active)
    {
        this.active = active;
    }
}


