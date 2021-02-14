using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator animator;
    private Rigidbody2D rb;

    bool explosion = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float v = 0;

        if (transform.parent != null)
        {
            v = transform.parent.GetComponentInParent<PlayerMovement>().magnitude;
            //hit = 0.5f * rb.mass * Mathf.Pow(transform.parent.GetComponentInParent<Rigidbody2D>().velocity.magnitude, 2);
        }
        else
        {
            v = collision.relativeVelocity.magnitude;
            //Debug.Log("no parent " + v2);
        }
        //Debug.Log(hit);
        if (v > 5)
        {
            //explode
            animator.SetTrigger("explode");
            explosion = true;
            rb.bodyType = RigidbodyType2D.Static;
            SoundManager.S.explodeBomb();
            Destroy(this.gameObject, 0.5f);
        }
        else
        {
            SoundManager.S.DronePipe();
        }

        if (collision.gameObject.tag == "Barrel" && collision.gameObject.GetComponent<Barrel>().explosion && !explosion)
        {
            animator.SetTrigger("explode");
            explosion = true;
            rb.bodyType = RigidbodyType2D.Static;
            SoundManager.S.explodeBomb();
            Destroy(this.gameObject, 0.5f);
        }

        if (explosion)
        {
            if (collision.gameObject.tag == "Player")
            {
                GameManager.S.takeDamage();
                Vector2 dir = collision.gameObject.transform.position - transform.position;
                dir.Normalize();
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * 15, ForceMode2D.Impulse);
            }
            if (collision.gameObject.tag == "Enemy")
            {
                Destroy(collision.gameObject);
                Vector2 dir = collision.gameObject.transform.position - transform.position;
                dir.Normalize();
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * 15, ForceMode2D.Impulse);
            }
            if (collision.gameObject.tag == "Cargo")
            {
                Vector2 dir = collision.gameObject.transform.position - transform.position;
                dir.Normalize();
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(dir / 300, ForceMode2D.Impulse);
            }
            if (collision.gameObject.tag == "Wood")
            {
                if (collision.gameObject.GetComponent<FloorBreak>() != null)
                {
                    collision.gameObject.GetComponent<FloorBreak>().explode();
                }
                else if (collision.gameObject.GetComponent<WallBreak>() != null)
                {
                    collision.gameObject.GetComponent<WallBreak>().explode();
                }
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
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
