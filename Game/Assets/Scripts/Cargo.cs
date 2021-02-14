using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo : MonoBehaviour
{

    public int health;

    private Rigidbody2D rb;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("trugger" + collision.gameObject.tag);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LoadingBay")
        {
            Debug.Log("scored");
            Destroy(this.gameObject);
            GameManager.S.scored();
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        float v = 0;
        
        if (transform.parent != null)
        {
            v = transform.parent.GetComponentInParent<PlayerMovement>().magnitude;
            //hit = 0.5f * rb.mass * Mathf.Pow(transform.parent.GetComponentInParent<Rigidbody2D>().velocity.magnitude, 2);
        }
        else {
            v = collision.relativeVelocity.magnitude;
            //Debug.Log("no parent " + v2);
        }
        //Debug.Log(hit);
        if (v > 5)
        {
            health -= 1;
            if (health == 1)
            {
                animator.SetTrigger("damaged");
            }
            SoundManager.S.BoxLandHard();
        }
        else
        {
            SoundManager.S.BoxLandSoft();
        }

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ConveyerBelt" && transform.parent == null)
        {
            Belt cb = collision.gameObject.GetComponent<Belt>();
            Vector3 newPos = new Vector3(transform.position.x + (cb.direction * Time.deltaTime * cb.speed), transform.position.y, transform.position.z);
            transform.position = newPos;
        }
    }

}
