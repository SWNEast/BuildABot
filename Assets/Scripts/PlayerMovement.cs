using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public Rigidbody2D rb;

    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;

    public TextMeshProUGUI legText;
    public TextMeshProUGUI foundText;

    public float jumpForce = 20f;
    public Transform feet;
    public LayerMask groundLayers;


    private BoxCollider2D boxCollider;
    private float mx;
    private bool IsGrounded = false;
    private bool canMove = true;

    private bool HasLegs = false;


    private void Start()
    {
        spriteRenderer.sprite = spriteArray[0];
        boxCollider = gameObject.AddComponent<BoxCollider2D>();
        foundText.gameObject.SetActive(false);
    }


    private void Update()
    {
        if (canMove)
        {
            mx = Input.GetAxis("Horizontal");

            if (Input.GetButtonDown("Jump") && CanJump())
            {
                Jump();
            }
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Vector2 movement = new Vector2(mx * movementSpeed, rb.velocity.y);

            rb.velocity = movement;
        }
    }

    void Jump()
    {
        Vector2 movement = new Vector2(rb.velocity.x, jumpForce);

        rb.velocity = movement;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsGrounded = false;
        }
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Legs"))
        {
            HasLegs = true;
            collision.gameObject.SetActive(false);
            StartCoroutine(ShowMessage("legs", 5));
            legText.text = "Has Legs";
            spriteRenderer.sprite = spriteArray[2];
            canMove = false;
            rb.velocity = new Vector2(0, 0);
            transform.position = new Vector2(rb.position.x, rb.position.y + 5);
            rb.isKinematic = true;
            yield return new WaitForSeconds(2);
            rb.isKinematic = false;
            canMove = true;
            
            spriteRenderer.sprite = spriteArray[1];
            ResetCollider();
        }
    }

    bool CanJump()
    {
        return IsGrounded && HasLegs;
    }

    IEnumerator ShowMessage(string message, float delay)
    {
        foundText.text = "You found some " + message + "!";
        foundText.gameObject.SetActive(true);
        yield return new WaitForSeconds(delay);
        foundText.gameObject.SetActive(false);
    }

    void ResetCollider()
    {
        boxCollider = null;
        boxCollider = gameObject.AddComponent<BoxCollider2D>();
    }
}
