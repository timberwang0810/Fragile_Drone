using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool facingLeft;


    GameObject holding = null;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        facingLeft = false;
    }
    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        if (horizontalMove < 0) facingLeft = true;
        else if (horizontalMove > 0) facingLeft = false;

        if (holding != null)
        {
            if (Input.GetKeyUp("space"))
            {
                holding.GetComponent<Rigidbody2D>().gravityScale = 1;
                holding.GetComponent<Transform>().parent = null;
                holding = null;
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cargo")
        {
            if (Input.GetKeyDown("space") && holding == null)
            {
                //Debug.Log("space pressed");
                collision.transform.parent = transform;
                collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                Vector3 newPos = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);

                collision.transform.position = newPos;
                holding = collision.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cargo")
        {
            holding = null;
            collision.transform.parent = null;
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

}
