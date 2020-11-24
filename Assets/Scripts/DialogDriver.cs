using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
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
    public CinemachineVirtualCamera playerCamera;
    private bool speaking;
    private bool flashing;
    private string speaker;
    private List<(string, string)> queue = new List<(string, string)>();
    private bool isColliding;

    private void Start()
    {
        Hide();
        StartCoroutine(FlashText());
        
    }

    private void Update()
    {
        isColliding = false;

        if (Input.GetKeyDown(KeyCode.V))
        {
            speaking = false;
        }

        if (!flashing && speaking)
        {
            StartCoroutine(FlashText());
        }

        if (speaking)
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

            Show();
        } else
        {
            Hide();
            if (queue.Count != 0)
            {
                StartCoroutine(Speak(queue[0].Item1, queue[0].Item2));
                queue.RemoveAt(0);
            }
            
        }

        


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isColliding)
        {
            return;
        }
        GameObject go = collision.gameObject;
        isColliding = true;
        if (go.Equals(dialogTriggers[0]))//ON SPAWN
        {
            queue.Add(("Can you hear me now?", "unknown" ));
            queue.Add(("Oh this might be helpful!", "unknown"));
            queue.Add(("*thud*", "unknown"));
            queue.Add(("Hi! I’m Bart, thanks to me you can now see and hear! You seem somewhat capable. Follow me!", "bart"));

            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[1]))//FIRST STAIRCASE
        {

            queue.Add(("What are you doing down there? Come on we have to go!", "bart"));
            queue.Add(("......", "bart"));
            queue.Add(("Are you struggling with the stairs? Here, these might help.", "bart"));
            
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[2]))//FIRST GAP
        {
            go.SetActive(false);
            Debug.Log("Added to queue");
            queue.Add(("Stuck again!", "bart"));
            queue.Add(("I'll do the heavy lifting again but this is the last time!", "bart"));
            
            
        }
        else if (go.Equals(dialogTriggers[3]))//FIRST LADDER
        {
            queue.Add(("Right, just up here we go", "bart"));
            queue.Add(("YOU HAVE TO BE KIDDING ME", "bart"));
            queue.Add(("YOU KNOW WHAT! I'LL WAIT FOR YOU UP HERE. FIGURE IT OUT!", "bart"));
            
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[4]))//AFTER CLIMBING FIRST LADDER
        {
            queue.Add(("Finally... Right then, come along.", "bart"));
            
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[5]))//AFTER DROPPING AT FIRST MAGNET
        {
            queue.Add(("This is getting ridiculous now.", "bart"));
            queue.Add(("I really have picked the most inept robot.", "bart"));
            queue.Add(("There must be something nearby that can help.", "bart"));
            
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[6]))//LEGACY. WAS AT MAGNET PICKUP. MOVED TO MAGNET PICKUP TRIGGER.
        {
            queue.Add(("How convenient...", "bart"));
            
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[7]))//LEGACY. WAS AT WHEEL PICKUP. MOVED TO WHEEL PICKUP TRIGGER.
        {
            queue.Add(("Ooooo shiny", "bart"));
            
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[8]))//AFTER 2ND JUMP
        {
            queue.Add(("Hey, wait up", "bart"));
            
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[9]))//ON BOULDER RELEASE
        {
            queue.Add(("RUN!!!", "bart"));
            
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[10]))//AFTER ESCAPING BOULDER
        {
            queue.Add(("Whew that was close. Come, we're almost there.", "bart"));
            
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[11]))//UPON REACHING THE HUB
        {
            queue.Add(("I really need help with a top secret quest and I <i> thought </i> you were the perfect bot to help me.", "bart"));
            queue.Add(("Based on how hard it was to get here I have a feeling you'll need some extra parts.", "bart"));
            queue.Add(("Explore this facility and see what you can find. I'll wait here.", "bart"));
            queue.Add(("Store any parts you don't need here. Don't forget you can only carry 3 unequipped parts.", "bart"));
            
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[12]))//AT FIRST CHECKPOINT
        {
            queue.Add(("A Checkpoint. These will come in handy if I die.", "player"));
            
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[13]))//AT FIRST TP RECEIVER
        {
            queue.Add(("Mmmm this looks like a Teleport Receiver. I wonder where the Sender is...", "player"));
            
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[14]))//AT FIRST TP SENDER
        {
            queue.Add(("Ah, this should send be back to the Receiver if I activate it with [UP].", "player"));
            go.SetActive(false);
        }
        else if (go.Equals(dialogTriggers[15]))//AT HARD JUMP SECTION
        {
            queue.Add(("This could be challenging...", "player"));
            go.SetActive(false);
        }
        else if (go.Equals(pickups[0]))//LEG PICKUP
        {
            queue.Add(("With these legs attached I bet I could climb up those stairs", "player"));
            go.SetActive(false);
        }
        else if (go.Equals(pickups[1]))//SPRING PICKUP
        {
            queue.Add(("If I upgrade my legs with these springs, using my inventory, I bet I could jump high and wide using [C]", "player"));
            go.SetActive(false);
        }
        else if (go.Equals(pickups[2]))//ARM PICKUP
        {
            queue.Add(("These should help me get up that ladder using [UP]...", "player"));
            go.SetActive(false);
        }
        else if (go.Equals(pickups[3]))//MAGNET PICKUP
        {
            queue.Add(("Magnets on my hands? Prehaps I could cling onto the ceiling by pressing [X]", "player"));
            queue.Add(("How convenient...", "bart"));
            
            go.SetActive(false);
        }
        else if (go.Equals(pickups[4]))//WHEEL PICKUP
        {
            queue.Add(("I can move super fast with these wheels by holding [C]. Woah, is that a jump?", "player"));
            queue.Add(("Ooooo shiny", "bart"));
            
            go.SetActive(false);
        }

    }

    void Show()
    {
        dialogPanel.gameObject.SetActive(true);
        //showing = true;
    }

    void Hide()
    {
        dialogPanel.gameObject.SetActive(false);
        //showing = false;
    }

    IEnumerator Speak(string line, string speaker)
    {
        speaking = true;
        this.speaker = speaker;
        CheckSpecialAction(line);
        dialogText.text = line;
        yield return new WaitForSeconds(dialogTimer);
        speaking = false;
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
            StartCoroutine(ThrowItem(pickups[0], bart.transform.position, -1, 5));
        }
        else if (line == "Stuck again!")
        {
            bart.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (line == "I'll do the heavy lifting again but this is the last time!")
        {
            Debug.Log("Throw Springs");
            StartCoroutine(ThrowItem(pickups[1], bart.transform.position, -1, 7));
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
        else if (line == "Explore this facility and see what you can find. I'll wait here.")
        {
            StartCoroutine(PanCamera(12, 80, 3, 1));
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

    IEnumerator ThrowItem(GameObject item, Vector2 from, float direction, float throwSpeed)
    {
        yield return new WaitForSeconds(2.5f);
        //pickups[0].transform.position = bart.transform.position;
        item.transform.position = from;
        //pickups[0].SetActive(true);
        item.SetActive(true);
        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();

        rb.gravityScale = 1;
        rb.velocity = new Vector2(direction * throwSpeed, rb.velocity.y + throwSpeed);
    }

    IEnumerator PanCamera(float speed, float offsetX, float holdTime, int direction)
    {
        player.GetComponent<PlayerMovement>().setMovement(false);
        bool panning = true;
        while (panning)
        {
            //camera.GetComponent<CinemachineVirtualCamera>().body

            playerCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset.x += direction;
            if (playerCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset == new Vector3(offsetX, 0, 0))
            {
                panning = false;
            }
            yield return new WaitForSeconds(speed / offsetX);
        }
        yield return new WaitForSeconds(holdTime);
        playerCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = Vector3.zero;
        player.GetComponent<PlayerMovement>().setMovement(true);
    }
}
