using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorStop : MonoBehaviour
{
    private Rigidbody2D rb;
    /*void OnTriggerStay(Collider other)
    {
        if (other.Transform.Tag == "Ground")
        {
            IsGrounded = true;
            Debug.Log("Grounded");
        }
        else
        {
            IsGrounded = false;
            Debug.Log("Not Grounded!");
        }
    }*/
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
        }
    }

}
