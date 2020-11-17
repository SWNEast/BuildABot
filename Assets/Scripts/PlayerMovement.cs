using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    
    public float movementSpeed;
    public Rigidbody2D rb;
    public float jumpForce = 10f;
    public TextMeshProUGUI tipText;
    public SpriteRenderer bodyRenderer;
    public Sprite[] bodySprites;
    public SpriteRenderer armsRenderer;
    public Sprite[] armSprites;
    public SpriteRenderer legsRenderer;
    public Sprite[] legSprites;
    public Inventory inventory;
    private float mx;
    private float my;
    private Vector2 lastCheckpoint = new Vector2(-5,-5);


    private bool isGrounded = false;
    private bool canMove = true;
    private bool legsEquipped = false;
    private bool kneesEquipped = false;
    private bool armsEquipped = false;
    private bool canClimb = false;
    private bool magnetsEquipped = false;
    private bool canMagnet = false;
    

    private void Start()
    {
        bodyRenderer.sprite = bodySprites[0];
        bodyRenderer.gameObject.AddComponent<BoxCollider2D>();
        tipText.gameObject.SetActive(false);
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

        if (isGrounded)
        {
            movementSpeed = 7;
        } else
        {
            movementSpeed = 5;
        }

        
        if (canMove)
        {
            mx = Input.GetAxis("Horizontal");
            if (canClimb)
            {
                my = Input.GetAxis("Vertical");
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                if (isGrounded && kneesEquipped)
                {
                    Jump();
                }
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                if (magnetsEquipped)
                {
                    Magnet();
                }
            }
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
        if (go.CompareTag("Leg Pickup"))
        {
            legsRenderer.sprite = legSprites[0];
            legsRenderer.gameObject.AddComponent<BoxCollider2D>();
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 2);
            GameObject.FindGameObjectWithTag("Stair Block").SetActive(false);
            inventory.foundItem(1);
            go.SetActive(false);
            legsEquipped = true;
        } else if (go.CompareTag("Checkpoint"))
        {
            lastCheckpoint = go.transform.position;
        } else if (go.CompareTag("Ladder") && armsEquipped)
        {
            canClimb = true;
        } else if (go.CompareTag("Knee Pickup"))
        {
            inventory.foundItem(3);
            go.SetActive(false);
            kneesEquipped = true;
        } else if (go.CompareTag("Arm Pickup"))
        {
            inventory.foundItem(5);
            go.SetActive(false);
            armsEquipped = true;
        } else if (go.CompareTag("Magnet Pickup"))
        {
            go.SetActive(false);
            magnetsEquipped = true;
        } else if (go.CompareTag("Magnet"))
        {
            canMagnet = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.CompareTag("Ladder"))
        {
            canClimb = false;
        } else if (go.CompareTag("Magnet"))
        {
            canMagnet = false;
            rb.gravityScale = 1.0f;
            tipText.gameObject.SetActive(false);
        }
        
    }

    void Jump()
    {
        Vector2 movement = new Vector2(rb.velocity.x, jumpForce);

        rb.velocity = movement;
    }

    void Magnet() //TODO Fix magnet activation when on floor.
    {
        if (canMagnet)
        {
            if (rb.gravityScale.Equals(0.0f)) //Magnet on
            {
                rb.gravityScale = 1.0f;
                tipText.gameObject.SetActive(false);
            }
            else
            {
                rb.gravityScale = 0.0f;
                rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                tipText.gameObject.SetActive(true);
            }
        }
    }

    void FaceRight()
    {
        bodyRenderer.sprite = bodySprites[0];
        if (armsEquipped)
        {
            armsRenderer.sprite = armSprites[0];
            armsRenderer.gameObject.transform.localPosition = new Vector2(0.03f, - 0.01f);
        }
    }

    void FaceLeft()
    {
        bodyRenderer.sprite = bodySprites[1];
        if (armsEquipped)
        {
            armsRenderer.sprite = armSprites[1];
            armsRenderer.gameObject.transform.localPosition = new Vector2(-0.03f, - 0.01f);
        }
    }

    void NewItemAnimation()
    {
        //TODO
    }
}
