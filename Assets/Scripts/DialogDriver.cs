using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogDriver : MonoBehaviour
{
    public Transform dialogPanel;
    public TextMeshProUGUI dialogText;
    public GameObject bee;
    public GameObject player;
    public GameObject[] dialogTriggers;
    public float dialogTimer = 3;

    private void Start()
    {
        Hide();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Hide();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.Equals(dialogTriggers[0]))
        {
            Show();
            string[] lines = new string[4] { 
                "Can you hear me now?", 
                "Oh this might be helpful!", 
                "*thud *", 
                "Hi! I’m B.E.E, thanks to me you can now see and hear! You seem somewhat capable. Follow me!"
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[1]))
        {
            Show();
            string[] lines = new string[3]
            {
                "What are you doing down there? Come on we have to go!",
                "......",
                "Are you struggling with the stairs? Here try these",
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[2]))
        {
            Show();
            string[] lines = new string[2]
            {
                "Stuck again!",
                "I'll do the heavy lifting again but this is the last time!"
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[3]))
        {
            Show();
            string[] lines = new string[3]
            {
                "Right just up here we go...",
                "YOU HAVE TO BE KIDDING ME!",
                "YOU KNOW WHAT! I'LL WAIT FOR YOU UP HERE. FIGURE IT OUT!"
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[4]))
        {
            Show();
            string[] lines = new string[1]
            {
                "Finally... Right then, come along."
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[5]))
        {
            Show();
            string[] lines = new string[3]
            {
                "This is getting ridiculous now,",
                "I really have picked the most inept robot.",
                "There must be something nearby that can help."
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[6]))
        {
            Show();
            string[] lines = new string[1]
            {
                "How convenient..."
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[7]))
        {
            Show();
            string[] lines = new string[1]
            {
                "Ooooo shiny"
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[8]))
        {
            Show();
            string[] lines = new string[1]
            {
                "I don't understand why this makes me go faster too but I'm loving it!"
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[9]))
        {
            Show();
            string[] lines = new string[1]
            {
                "RUN!!!"
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[10]))
        {
            Show();
            string[] lines = new string[1]
            {
                "Whew that was close. Come, we're almost there."
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[11]))
        {
            Show();
            string[] lines = new string[3]
            {
                "I really need help with a top secret quest and I <i> thought </i> you were the perfect bot to help me.",
                "Based on how hard it was to get here I have a feeling you'll need some extra parts.",
                "Explore this facility and see what you can find. I'll wait here."
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
    }

    void Show()
    {
        dialogPanel.gameObject.SetActive(true);
    }

    void Hide()
    {
        dialogPanel.gameObject.SetActive(false);
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(dialogTimer);
        Hide();
    }

    IEnumerator Multi(string[] lines)
    {
        foreach (string line in lines)
        {
            dialogText.text = line;
            yield return new WaitForSeconds(dialogTimer);
        }
        Hide();
    }

}
