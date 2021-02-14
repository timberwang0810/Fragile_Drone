using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool facingLeft;

    public float droneReach;
    public LayerMask pickupLayers;

    private GameObject holding = null;

    private Animator animator;

    private SpriteRenderer mySpriteRenderer;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        facingLeft = false;
        animator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();

    }
    
    // Update is called once per frame
    void Update()
    {
        if (GameManager.S.gameState == GameManager.GameState.playing)
        {
            float horizontalMove = Input.GetAxisRaw("Horizontal");
            if (horizontalMove < 0)
            {
                facingLeft = true;
                mySpriteRenderer.flipX = true;
            }
            else if (horizontalMove > 0)
            {
                facingLeft = false;
                mySpriteRenderer.flipX = false;
            }

            if (Input.GetKeyDown("space") && holding == null)
            {
                ShootClaw();
            }
            if (holding != null && (Input.GetKeyUp("space") || Vector2.Distance(transform.position, holding.transform.position) > droneReach || Vector2.Angle(holding.transform.position - transform.position, Vector2.down) > 35.0f))
            {
                holding.GetComponent<Rigidbody2D>().gravityScale = 1;

                Vector2 v = GetComponent<PlayerMovement>().velocity;
                holding.GetComponent<Rigidbody2D>().AddRelativeForce(v / 300, ForceMode2D.Force);

            
                holding.GetComponent<Transform>().parent = null;
                holding = null;
                animator.SetBool("carrying", false);
            }
            else if (holding != null)
            {
                holding.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    private void ShootClaw()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, droneReach, pickupLayers);
        Collider2D collision = hit.collider;
        if (collision == null) return;
        if (collision.gameObject.CompareTag("Cargo") || collision.gameObject.CompareTag("Barrel"))
        {
            collision.transform.parent = transform;
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            holding = collision.gameObject;

            animator.SetBool("carrying", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Cargo" || collision.gameObject.tag == "Enemy")
        {
            
        } else if (collision.gameObject.tag == "Wood")
        {
            SoundManager.S.WoodHit();
        }
        else
        {
            SoundManager.S.DronePipe();
        }
    }

}
