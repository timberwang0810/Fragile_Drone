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
            if (holding != null && (Input.GetKeyUp("space") || Vector2.Distance(transform.position, holding.transform.position) > droneReach))
            {
                holding.GetComponent<Rigidbody2D>().gravityScale = 1;

                Vector2 v = GetComponent<PlayerMovement>().velocity;
                Debug.Log("drop" + v);
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
        Debug.Log("space pressed");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, droneReach, pickupLayers);
        //Debug.DrawLine(transform.position, transform.position + Vector3.down * droneReach, Color.blue, 10f);
        Collider2D collision = hit.collider;
        if (collision == null) return;
        if (collision.gameObject.CompareTag("Cargo"))
        {
            Debug.Log("Hit!!!");
            collision.transform.parent = transform;
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            //Vector3 newPos = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);

            //collision.transform.position = newPos;
            holding = collision.gameObject;

            animator.SetBool("carrying", true);
        }
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Cargo")
    //    {
    //        if (Input.GetKeyDown("space") && holding == null)
    //        {
    //            //Debug.Log("space pressed");
    //            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, droneReach);
    //            if (hit.collider.gameObject.CompareTag("Cargo"))
    //            {
    //                collision.transform.parent = transform;
    //                collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
    //                Vector3 newPos = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);

    //                collision.transform.position = newPos;
    //                holding = collision.gameObject;
    //            }
                
    //        }
    //    }
    //}

//     private void OnTriggerExit2D(Collider2D collision)
//     {
//         if (collision.gameObject.tag == "Cargo")
//         {
//             holding = null;
//             collision.transform.parent = null;
//             collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
//             animator.SetBool("carrying", false);
//         }
//     }

}
