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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LoadingBay")
        {
            GameManager.S.scored();
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        float v = 0;
        
        if (transform.parent != null)
        {
            v = transform.parent.GetComponentInParent<PlayerMovement>().magnitude;
        }
        else {
            v = collision.relativeVelocity.magnitude;
        }
        if (v > 5)
        {
            takeDamage(1);
            SoundManager.S.BoxLandHard();
        }
        else
        {
            SoundManager.S.BoxLandSoft();
        }

        
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        if (health == 1)
        {
            animator.SetTrigger("damaged");
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
