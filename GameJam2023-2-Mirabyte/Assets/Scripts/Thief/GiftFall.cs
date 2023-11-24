using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftFall : MonoBehaviour
{
    Vector2 target;
    string color;
    public Sprite blueSprite;
    public Sprite pinkSprite;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public Animator animator;

    public void StartFall(Vector2 target, string color)
    {
        animator.enabled = false;
        this.target = target;
        transform.position = new Vector2(target.x, target.y + 15);
        this.color = color;
        Debug.Log(color);
        if(color == "blue")
        {
            spriteRenderer.sprite = blueSprite;
        }
        else
        {
            spriteRenderer.sprite = pinkSprite;
        }
        spriteRenderer.size = new Vector2(0.6787114f, 0.5442973f);
        rb.WakeUp();
    }

    private void Update()
    {
        if(transform.position.y <= target.y && !IsAnimationPlaying())
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartAnimation();
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Exit") && !IsAnimationPlaying())
        {
            Destroy(gameObject);
        }
    }

    bool IsAnimationPlaying()
    {
        if(color == "blue")
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName("gift_fall_blue");
        }
        else
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName("gift_fall_pink");
        }
    }

    void StartAnimation()
    {
        animator.enabled = true;
        if(color == "blue")
        {
            animator.SetBool("IsBlue", true);
        }
        else
        {
            animator.SetBool("IsPink", true);
        }
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        animator.SetBool("explode", true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && !IsAnimationPlaying())
        {
            StartAnimation();
            if(collision.transform.gameObject.GetComponent<PlayerBehaviour>().GetState() != State.Stunned)
            {
                collision.transform.gameObject.GetComponent<PlayerBehaviour>().StartStun();
            }
        }
    }
}
