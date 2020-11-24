using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using TMPro;

public class DialogDriver : MonoBehaviour
{
    public Transform dialogPanel;
    public TextMeshProUGUI dialogText;
    public GameObject bart;
    public GameObject player;
    public Sprite question;
    public GameObject[] dialogTriggers;
    public GameObject[] pickups;
    public float dialogTimer = 3.0f;
    public TextMeshProUGUI skipText;
    public float flashSpeed;
    public GameObject speakerImage;
    public CanvasGroup blackScreen;

    private bool showing;
    private bool flashing;
    private string speaker;

    private void Start()
    {
        Hide();
        StartCoroutine(FlashText());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Hide();
        }

        if (!flashing && showing)
        {
            StartCoroutine(FlashText());
        }

        if (showing)
        {
            if (speaker == "bart")
            {
                speakerImage.GetComponent<Image>().sprite = bart.GetComponent<Bart>().GetSprite();
            } else if (speaker == "unknown")
            {
                speakerImage.GetComponent<Image>().sprite = question;
            } else if (speaker == "player")
            {
                speakerImage.GetComponent<Image>().sprite = GameObject.Find("Body").GetComponent<SpriteRenderer>().sprite;
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.Equals(dialogTriggers[0]))//ON SPAWN
        {
            speaker = "unknown";
            string[] lines = new string[4] { 
                "Can you hear me now?", 
                "Oh this might be helpful!", 
                "*thud*", 
                "Hi! I’m Bart, thanks to me you can now see and hear! You seem somewhat capable. Follow me!"
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[1]))//FIRST STAIRCASE
        {
            speaker = "bart";
            string[] lines = new string[3]
            {
                "What are you doing down there? Come on we have to go!",
                "......",
                "Are you struggling with the stairs? Here, these might help.",
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[2]))//FIRST GAP
        {
            speaker = "bart";
            string[] lines = new string[2]
            {
                "Stuck again!",
                "I'll do the heavy lifting again but this is the last time!"
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[3]))//FIRST LADDER
        {
            speaker = "bart";
            string[] lines = new string[3]
            {
                "Right just up here we go...",
                "YOU HAVE TO BE KIDDING ME!",
                "YOU KNOW WHAT! I'LL WAIT FOR YOU UP HERE. FIGURE IT OUT!"
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[4]))//AFTER CLIMBING FIRST LADDER
        {
            speaker = "bart";
            string[] lines = new string[1]
            {
                "Finally... Right then, come along."
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[5]))//AFTER DROPPING AT FIRST MAGNET
        {
            speaker = "bart";
            string[] lines = new string[3]
            {
                "This is getting ridiculous now.",
                "I really have picked the most inept robot.",
                "There must be something nearby that can help."
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[6]))//LEGACY. WAS AT MAGNET PICKUP. MOVED TO MAGNET PICKUP TRIGGER.
        {
            speaker = "bart";
            string[] lines = new string[1]
            {
                "How convenient..."
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[7]))//LEGACY. WAS AT WHEEL PICKUP. MOVED TO WHEEL PICKUP TRIGGER.
        {
            speaker = "bart";
            string[] lines = new string[1]
            {
                "Ooooo shiny"
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[8]))//AFTER 2ND JUMP
        {
            speaker = "bart";
            string[] lines = new string[1]
            {
                "Hey, wait up!"
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[9]))//ON BOULDER RELEASE
        {
            speaker = "bart";
            string[] lines = new string[1]
            {
                "RUN!!!"
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[10]))//AFTER ESCAPING BOULDER
        {
            speaker = "bart";
            string[] lines = new string[1]
            {
                "Whew that was close. Come, we're almost there."
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[11]))//UPON REACHING THE HUB
        {
            speaker = "bart";
            string[] lines = new string[3]
            {
                "I really need help with a top secret quest and I <i> thought </i> you were the perfect bot to help me.",
                "Based on how hard it was to get here I have a feeling you'll need some extra parts.",
                "Explore this facility and see what you can find. I'll wait here."
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[12]))//AT FIRST CHECKPOINT
        {
            speaker = "player";
            string[] lines = new string[1]
            {
                "A Checkpoint. These will come in handy if I die."
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[13]))//AT FIRST TP RECEIVER
        {
            speaker = "player";
            string[] lines = new string[1]
            {
                "Mmmm this looks like a Teleport Receiver. I wonder where the Sender is..."
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[14]))//AT FIRST TP SENDER
        {
            speaker = "player";
            string[] lines = new string[1]
            {
                "Ah, this should send be back to the Receiver if I activate it with [UP]."
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[15]))//AT HARD JUMP SECTION
        {
            speaker = "player";
            string[] lines = new string[1]
            {
                "This could be challenging..."
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(pickups[0]))//LEG PICKUP
        {
            speaker = "player";
            string[] lines = new string[1]
            {
                "With these legs attached I bet I could climb up those stairs"
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(pickups[1]))//SPRING PICKUP
        {
            speaker = "player";
            string[] lines = new string[1]
            {
                "If I upgrade my legs with these springs, using my inventory, I bet I could jump high and wide using [C]"
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(pickups[2]))//ARM PICKUP
        {
            speaker = "player";
            string[] lines = new string[1]
            {
                "These should help me get up that ladder using [UP]..."
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(pickups[3]))//MAGNET PICKUP
        {
            speaker = "player";
            string[] lines = new string[2]
            {
                "Magnets on my hands? Prehaps I could cling onto the ceiling by pressing [X]",
                "How convenient..."
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }
        else if (go.Equals(pickups[4]))//WHEEL PICKUP
        {
            speaker = "player";
            string[] lines = new string[2]
            {
                "I can move super fast with these wheels by holding [C]. Woah, is that a jump?",
                "Ooooo shiny"
            };
            StartCoroutine(Multi(lines));
            go.SetActive(false);
        }


    }

    void Show()
    {
        dialogPanel.gameObject.SetActive(true);
        showing = true;
    }

    void Hide()
    {
        dialogPanel.gameObject.SetActive(false);
        showing = false;
    }

    IEnumerator Multi(string[] lines)
    {
        foreach (string line in lines)
        {
            Show();
            CheckSpecialAction(line);
            dialogText.text = line;
            yield return new WaitForSeconds(dialogTimer);
            Hide();
        }
    }

    IEnumerator FlashText()
    {
        flashing = true;
        yield return new WaitForSeconds(flashSpeed);
        skipText.enabled = !skipText.enabled;
        flashing = false;
    }

    void CheckSpecialAction(string line)
    {
        if (line == "*thud*")
        {
            StartCoroutine(FadeIn());
        }
        else if (line == "Hi! I’m Bart, thanks to me you can now see and hear! You seem somewhat capable. Follow me!")
        {
            speaker = "bart";
            bart.GetComponent<Bart>().SetTarget(new Vector2(50, -2.5f));
            player.GetComponent<PlayerMovement>().setMovement(true);
        }
        else if (line == "What are you doing down there? Come on we have to go!")
        {
            bart.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (line == "With these legs attached I bet I could climb up those stairs")
        {
            bart.GetComponent<SpriteRenderer>().flipX = false;
            bart.GetComponent<Bart>().SetTarget(new Vector2(92.5f, -2.5f));
        }
        else if (line == "Are you struggling with the stairs? Here, these might help.")
        {
            StartCoroutine(ThrowItem(pickups[0], bart.transform.position, -1));
        }
        else if (line == "Stuck again!")
        {
            bart.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (line == "I'll do the heavy lifting again but this is the last time!")
        {
            StartCoroutine(ThrowItem(pickups[1], bart.transform.position, -1));
        }
        else if (line == "If I upgrade my legs with these springs, using my inventory, I bet I could jump high and wide using [C]")
        {
            bart.GetComponent<SpriteRenderer>().flipX = false;
            bart.GetComponent<Bart>().SetTarget(new Vector2(129.5f, -2.5f));
        }
        else if (line == "YOU KNOW WHAT! I'LL WAIT FOR YOU UP HERE. FIGURE IT OUT!")
        {
            bart.GetComponent<Bart>().SetTarget(new Vector2(129.5f, 21.5f));
        }
        else if (line == "Finally... Right then, come along.")
        {
            bart.GetComponent<Bart>().SetFollowing(true);
        }
        else if (line == "How convenient...")
        {
            speaker = "bart";
        }
        else if (line == "Ooooo shiny")
        {
            speaker = "bart";
        }
    }

    IEnumerator FadeIn()
    {
        float counter = 0;

        while (counter < dialogTimer)
        {
            counter += Time.deltaTime;

            blackScreen.alpha = Mathf.Lerp(1, 0, counter / dialogTimer);

            yield return null;
        }
    }

    IEnumerator ThrowItem(GameObject item, Vector2 from, float direction)
    {
        yield return new WaitForSeconds(2.5f);
        //pickups[0].transform.position = bart.transform.position;
        item.transform.position = from;
        //pickups[0].SetActive(true);
        item.SetActive(true);
        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();

        float throwSpeed = 5;
        rb.gravityScale = 1;
        rb.velocity = new Vector2(direction * throwSpeed, rb.velocity.y + throwSpeed);
    }
}
