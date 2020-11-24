using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    
    public float baseSpeed;
    public float maxY = 5;
    public float maxSpeed;
    public float rayOffset = 0.5f;
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
    public GameObject initialSpawn;
    private Vector2 lastCheckpoint;
    public float offsetY = 0.316f;
    private Vector3 lastPosition = new Vector3(-12f, -7f,0);
    private bool facingRight = true;

    private float movementSpeed;
    private bool isGrounded = false;
    private bool canMove = false;
    private bool legsEquipped = false;
    private bool springsEquipped = false;
    private bool jumping = false;
    private bool armsEquipped = false;
    private bool canClimb = false;
    private bool magnetsEquipped = false;
    private bool canMagnet = false;
    private bool wheelsEquipped = false;
    private float speedBoost = 0;
    private bool onRamp = false;

    private GameObject boulder;
    private GameObject boulderBlock;
    private GameObject boulderTrigger;
    private Vector2 boulderSpawn;

    public Transform legsTut;
    public Transform springsTut;
    public Transform armsTut;





    private void Start()
    {
        transform.position = initialSpawn.transform.position;
        lastCheckpoint = initialSpawn.transform.position;
        
        bodyRenderer.sprite = bodySprites[0];
        bodyRenderer.gameObject.AddComponent<BoxCollider2D>();
        tipText.gameObject.SetActive(false);

        boulder = GameObject.FindGameObjectWithTag("Boulder");
        boulderBlock = GameObject.FindGameObjectWithTag("Boulder Block");
        boulderTrigger = GameObject.FindGameObjectWithTag("Boulder Trigger");
        boulderSpawn = boulder.transform.position;

        legsTut.gameObject.SetActive(false);
        springsTut.gameObject.SetActive(false);
        armsTut.gameObject.SetActive(false);
    }

    private void Update()
    {
        Vector3 direction = transform.position - lastPosition;
        Vector3 localDirection = transform.InverseTransformDirection(direction);
        lastPosition = transform.position;
        if (localDirection.x < 0)
        {
            FaceLeft();
        } else if (localDirection.x > 0)
        {
            FaceRight();
        }

        if (PlayerIsOnGround())
        {
            movementSpeed = baseSpeed + speedBoost;
        } else
        {
            movementSpeed = baseSpeed - 2 + speedBoost;
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
                if (springsEquipped && PlayerIsOnGround())
                {
                    jumping = true;
                } 
            } else if (Input.GetKey(KeyCode.C))
            {
                if (wheelsEquipped)
                {
                    Sprint(true);
                }
            } else if (Input.GetKeyUp(KeyCode.C))
            {
                if (springsEquipped && rb.velocity.y > 0)
                {
                    rb.velocity = new Vector2(mx * movementSpeed, rb.velocity.y * 0.5f);
                } else if (wheelsEquipped)
                {
                    Sprint(false);
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
            /*if (legsEquipped && PlayerIsOnGround())
            {
                Vector2 startPosition = (Vector2)legsRenderer.transform.position - new Vector2(0f, 0.5f);
                int layerMask = LayerMask.GetMask("Ground");
                float laserLength = 5f;
                RaycastHit2D groundCheck = Physics2D.Raycast(startPosition + new Vector2(0.6f, 0f), Vector2.down, laserLength, layerMask, 0);
                Debug.Log("Estimated position" + (new Vector2(transform.position.x, transform.position.y - groundCheck.distance)));
                transform.position = new Vector2(transform.position.x, transform.position.y - groundCheck.distance);
            }*/
            //Vector2 movement;
            //if (rb.velocity.y > 1 && !jumping)
            //{
            //    movement = new Vector2(mx * movementSpeed, 1);

            //} else
            //{
            //    movement = new Vector2(mx * movementSpeed, rb.velocity.y);
            //}
            Vector2 movement;
            if (PlayerIsOnSlope())
            {
                if (rb.velocity.y > 1) //Moving up check
                {
                    movement = new Vector2(mx * movementSpeed, 1);
                } else
                {
                    movement = new Vector2(mx * movementSpeed, rb.velocity.y);
                }
                if (Input.GetAxis("Horizontal") == 0) //Not moving check
                {
                    rb.gravityScale = 0f;
                    movement = new Vector2(mx * movementSpeed, 0);
                } //else if (rb.velocity.y <= 0 && DistanceToGround() != Vector2.zero) //Moving down check
                //{
                //    transform.position = new Vector2(transform.position.x, DistanceToGround().y + offsetY);
                //}
                else
                {
                    rb.gravityScale = 1f;
                }
                
                
            } else
            {
                if (rb.velocity.y > maxY && !jumping)
                {
                    movement = new Vector2(mx * movementSpeed, rb.velocity.y);
                } else
                {
                    movement = new Vector2(mx * movementSpeed, rb.velocity.y);
                }
                
            }
            rb.velocity = movement;
        }
        if (jumping)
        {
            rb.gravityScale = 1.0f;
            Jump();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.CompareTag("Ground"))
        {
            isGrounded = true;
            onRamp = false;
        } else if (go.CompareTag("Spike"))
        {
            //canMove = false;
            //deathText.gameObject.SetActive(true);
            Die();
            
        } else if (go.CompareTag("Boulder"))
        {
            Die();
        }
        if (go.CompareTag("Ramp")) 
        {
            onRamp = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        if (go.CompareTag("Ramp"))
        {
            onRamp = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.CompareTag("Leg Pickup"))
        {
            legsRenderer.sprite = legSprites[0];
            legsRenderer.gameObject.AddComponent<BoxCollider2D>();
            //legsTut.gameObject.SetActive(true);
            //rb.velocity = Vector3.zero;
            //canMove = false;
            GameObject.FindGameObjectWithTag("Stair Block").SetActive(false);
            inventory.foundItem(2);
            go.SetActive(false);
            legsEquipped = true;
            NewItemAnimation();
        } else if (go.CompareTag("Checkpoint"))
        {
            lastCheckpoint = go.transform.position;
        } else if (go.CompareTag("Ladder") && armsEquipped)
        {
            canClimb = true;
        } else if (go.CompareTag("Spring Pickup"))
        {
            //springsTut.gameObject.SetActive(true);
            //rb.velocity = Vector3.zero;
            //canMove = false;
            inventory.foundItem(14);
            go.SetActive(false);
            springsEquipped = true;
            NewItemAnimation();
        } else if (go.CompareTag("Arm Pickup"))
        {
            //armsTut.gameObject.SetActive(true);
            //rb.velocity = Vector3.zero;
            //canMove = false;
            inventory.foundItem(4);
            go.SetActive(false);
            armsEquipped = true;
            NewItemAnimation();
        } else if (go.CompareTag("Magnet Pickup"))
        {
            inventory.foundItem(22);
            go.SetActive(false);
            magnetsEquipped = true;
            NewItemAnimation();
        } else if (go.CompareTag("Magnet"))
        {
            canMagnet = true;
        }
        else if (go.CompareTag("Wheels Pickup"))
        {
            inventory.foundItem(16);
            go.SetActive(false);
            wheelsEquipped = true;
            springsEquipped = false; //Delete when merged
            NewItemAnimation();
        } else if (go.CompareTag("Boulder Trigger"))
        {
            boulderBlock.SetActive(false);
            go.SetActive(false);
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
        jumping = false;
    }
    

    bool PlayerIsOnGround()
    {
        Vector2 startPosition = (Vector2)legsRenderer.transform.position - new Vector2(0f, 0.5f);
        int layerMask = LayerMask.GetMask("Ground");
        float laserLength = 0.5f;
        RaycastHit2D groundCheck1 = Physics2D.Raycast(startPosition + new Vector2(rayOffset, 0f), Vector2.down, laserLength, layerMask, 0);
        RaycastHit2D groundCheck2 = Physics2D.Raycast(startPosition, Vector2.down, laserLength, layerMask, 0);
        RaycastHit2D groundCheck3 = Physics2D.Raycast(startPosition - new Vector2(rayOffset, 0f), Vector2.down, laserLength, layerMask, 0);
        Debug.DrawRay(startPosition + new Vector2(rayOffset, 0f), Vector2.down * laserLength, Color.red);
        Debug.DrawRay(startPosition, Vector2.down * laserLength, Color.red);
        Debug.DrawRay(startPosition - new Vector2(rayOffset, 0f), Vector2.down * laserLength, Color.red);
        if ((groundCheck1.collider != null && groundCheck1.collider.gameObject.CompareTag("Ground")) ||
            (groundCheck2.collider != null && groundCheck2.collider.gameObject.CompareTag("Ground")) ||
            (groundCheck3.collider != null && groundCheck3.collider.gameObject.CompareTag("Ground")))
        {
            return true;
        } else return false;
        
    }

    bool PlayerIsOnSlope()
    {
        Vector2 startPosition = (Vector2)legsRenderer.transform.position - new Vector2(0f, 0.5f);
        int layerMask = LayerMask.GetMask("Ground");
        float laserLength = 1f;

        RaycastHit2D groundCheck = Physics2D.Raycast(startPosition, Vector2.down, laserLength, layerMask, 0);
        Debug.DrawRay(startPosition, Vector2.down * laserLength, Color.blue);
        if (groundCheck.collider != null && groundCheck.normal.y < 0.9f)
        {
            //Debug.Log(groundCheck.normal);
            return true;
        } else
        {
            //Debug.Log(groundCheck.normal);
            return false;
        }
    }

    Vector2 DistanceToGround()
    {
        Vector2 startPosition = (Vector2)legsRenderer.transform.position - new Vector2(0f, 0.5f);
        int layerMask = LayerMask.GetMask("Ground");
        float laserLength = 0.5f;

        RaycastHit2D groundCheck = Physics2D.Raycast(startPosition, Vector2.down, laserLength, layerMask, 0);
        //Debug.DrawRay(startPosition, Vector2.down * laserLength, Color.blue);
        if (groundCheck.collider != null)
        {
            Debug.Log(groundCheck.distance);
            return groundCheck.point;
        }
        else return new Vector2(0,0);
    }

    void Magnet()
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

    void Sprint(bool isSprinting)
    {
        if (isSprinting)
        {
            speedBoost = baseSpeed;
            if (onRamp && PlayerIsOnSlope())
            {
                rb.gravityScale = 1.0f;
                Jump();
                Debug.Log("Jumped");
            }
        } else
        {
            speedBoost = 0;
        }
    }

    void FaceRight()
    {
        facingRight = true;
        bodyRenderer.sprite = bodySprites[0];
        if (armsEquipped)
        {
            armsRenderer.sprite = armSprites[0];
            armsRenderer.gameObject.transform.localPosition = new Vector2(0.03f, - 0.01f);
        }
    }

    void FaceLeft()
    {
        facingRight = false;
        bodyRenderer.sprite = bodySprites[1];
        if (armsEquipped)
        {
            armsRenderer.sprite = armSprites[1];
            armsRenderer.gameObject.transform.localPosition = new Vector2(-0.03f, - 0.01f);
        }
    }

    void NewItemAnimation()
    {
        //transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
    }

    void Die()
    {
        ResetBoulder();
        transform.position = new Vector2(lastCheckpoint.x, lastCheckpoint.y + 0.5f);
    }

    void ResetBoulder()
    {
        boulder.transform.position = boulderSpawn;
        boulderTrigger.SetActive(true);
        boulderBlock.SetActive(true);
    }

    public void setMovement(bool canMove) {
        this.canMove = canMove;
    }

    public bool getFacingRight()
    {
        return facingRight;
    }
}
