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

    private float acceleration;
    private float distanceMoved = 0;
    public float lastDistanceMoved = 0;
    private Vector2 last;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        facingLeft = false;
        last = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.S.gameState == GameManager.GameState.playing)
        {
            float horizontalMove = Input.GetAxisRaw("Horizontal");
            if (horizontalMove < 0) facingLeft = true;
            else if (horizontalMove > 0) facingLeft = false;

            if (Input.GetKeyDown("space") && holding == null)
            {
                ShootClaw();
            }
            if (holding != null && (Input.GetKeyUp("space") || Vector2.Distance(transform.position, holding.transform.position) > droneReach))
            {
                holding.GetComponent<Rigidbody2D>().gravityScale = 1;
                holding.GetComponent<Rigidbody2D>().AddRelativeForce((facingLeft ? Vector2.left : Vector2.right)/ 30, ForceMode2D.Force);
                holding.GetComponent<Transform>().parent = null;
                holding = null;
            }
            else if (holding != null)
            {
                holding.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            UpdateAcceleration();
            Debug.Log(acceleration);
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
        }
    }

    private void UpdateAcceleration()
    {
        distanceMoved = Vector2.Distance(last, transform.position);
        distanceMoved *= Time.deltaTime;
        acceleration = distanceMoved - lastDistanceMoved;
        lastDistanceMoved = distanceMoved;
        last = transform.position;
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

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Cargo")
    //    {
    //        holding = null;
    //        collision.transform.parent = null;
    //        collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
    //    }
    //}

}
