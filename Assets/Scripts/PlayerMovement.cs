using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    
    public float movementSpeed;
    public Rigidbody2D rb;
    public float jumpForce = 10f;
    public TextMeshProUGUI deathText;
    public SpriteRenderer bodyRenderer;
    public Sprite[] bodySprites;
    public SpriteRenderer armsRenderer;
    public Sprite[] armSprites;
    public SpriteRenderer legsRenderer;
    public Sprite[] legSprites;

    private float mx;
    private float my;
    private Vector2 lastCheckpoint = new Vector2(-5,-5);

    public Inventory inventory;

    private bool isGrounded = false;
    private bool canMove = true;
    private bool hasLegs = false;
    private bool hasKnees = false;
    private bool hasArms = false;
    private bool canClimb = false;

    private void Start()
    {
        bodyRenderer.sprite = bodySprites[0];
        bodyRenderer.gameObject.AddComponent<BoxCollider2D>();
        deathText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (rb.velocity.x < 0)
        {
            FaceLeft();
        } else if (rb.velocity.x > 0)
        {
            FaceRight();
        }


        if (canMove)
        {
            mx = Input.GetAxis("Horizontal");

            if (Input.GetButtonDown("Jump") && isGrounded && hasKnees)
            {
                Jump();
            }
        }
        if (canClimb)
        {
            my = Input.GetAxis("Vertical");
        }
    }

    private void FixedUpdate()
    {
        if (canMove && canClimb) {
            Vector2 movement = new Vector2(mx * movementSpeed, my * movementSpeed);
            rb.velocity = movement;
        } else if (canMove)
        {
            Vector2 movement = new Vector2(mx * movementSpeed, rb.velocity.y);
            rb.velocity = movement;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        } else if (collision.gameObject.CompareTag("Spike"))
        {
            //canMove = false;
            //deathText.gameObject.SetActive(true);
            transform.position = new Vector2(lastCheckpoint.x, lastCheckpoint.y+5);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.CompareTag("Leg Pickup")){
            StartCoroutine(GiveLegs());
            go.SetActive(false);
            inventory.foundItem(1);
        } else if (go.CompareTag("Checkpoint"))
        {
            lastCheckpoint = go.transform.position;
        } else if (go.CompareTag("Ladder") && hasArms)
        {
            canClimb = true;
        } else if (go.CompareTag("Knee Pickup"))
        {
            inventory.foundItem(3);
            go.SetActive(false);
            hasKnees = true;
        } else if (go.CompareTag("Arm Pickup"))
        {
            inventory.foundItem(5);
            go.SetActive(false);
            hasArms = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            canClimb = false;
        }
    }

    void Jump()
    {
        Vector2 movement = new Vector2(rb.velocity.x, jumpForce);

        rb.velocity = movement;
    }

    IEnumerator GiveLegs()
    {
        yield return new WaitForSeconds(5);
        hasLegs = true;
        GameObject.FindGameObjectWithTag("Stair Block").SetActive(false);
        legsRenderer.sprite = legSprites[0];
        legsRenderer.gameObject.AddComponent<BoxCollider2D>();
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 5);
    }

    void FaceRight()
    {
        bodyRenderer.sprite = bodySprites[0];
        if (hasArms)
        {
            armsRenderer.sprite = armSprites[0];
            armsRenderer.gameObject.transform.localPosition = new Vector2(0.03f, - 0.01f);
        }
    }

    void FaceLeft()
    {
        bodyRenderer.sprite = bodySprites[1];
        if (hasArms)
        {
            armsRenderer.sprite = armSprites[1];
            armsRenderer.gameObject.transform.localPosition = new Vector2(-0.03f, - 0.01f);
        }
    }
}
