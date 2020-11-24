using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bart : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public Sprite[] normal;
    public Sprite[] angry;
    public float waitTime;
    public GameObject player;
    public float followOffsetX;
    public float followOffsetY;

    public float speed = 1;

    private int currentIndex;
    private Sprite currentSprite;
    private bool waiting = false;
    private bool isAngry = false;

    private bool following = false;
    private bool travelling = false;
    private Vector2 target;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = player.transform.position;
        currentIndex = 0;
        spriteRenderer.sprite = normal[0];
        currentSprite = normal[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (!waiting)
        {
            if (isAngry)
            {
                StartCoroutine(NextSprite(angry));
            } else
            {
                StartCoroutine(NextSprite(normal));
            }
        }

        float step = speed * Time.deltaTime;
        if (travelling)
        {
            following = false;
            transform.position = Vector2.MoveTowards(transform.position, target, step);
            if (target == new Vector2(transform.position.x, transform.position.y))
            {
                travelling = false;
            }
        }

        if (following)
        {
            target = new Vector2(player.transform.position.x + followOffsetX, player.transform.position.y + followOffsetY);
            transform.position = Vector2.MoveTowards(transform.position, target, step * 2);
            spriteRenderer.flipX = !player.GetComponent<PlayerMovement>().getFacingRight();
        }
        
    }

    IEnumerator NextSprite(Sprite[] sprites)
    {
        waiting = true;
        yield return new WaitForSeconds(waitTime);
        currentIndex += 1;
        if (currentIndex == sprites.Length)
        {
            currentIndex = 0;
        }
        currentSprite = sprites[currentIndex];
        spriteRenderer.sprite = currentSprite;
        waiting = false;
    }

    public Sprite GetSprite()
    {
        return currentSprite;
    }

    public void SetFollowing(bool following)
    {
        this.following = following;
    }


    public void SetTarget(Vector2 target)
    {
        this.target = target;
        travelling = true;
    }

}
