using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChargingEnemy : Enemy
{

    public float searchRadius;
    public float patrolSpeed;
    public float chargeSpeed;
    public float repelStrength;
    private bool player_locked;
    private bool patrolling;
    private bool facingLeft;
    private Vector2 playerLocation;

    private SpriteRenderer mySpriteRenderer;
    private float xval;

    public LayerMask rayCastLayers;
    public Transform patrolPostLocation;

    protected override void OnEnemyDeath()
    {
        Destroy(this.gameObject);
    }

    protected override void OnPlayerSighting()
    {
        GameObject[] playerInRange = Physics2D.OverlapCircleAll(transform.position, searchRadius).Select<Collider2D, GameObject>(c => c.gameObject).Where<GameObject>(obj => obj.CompareTag("Player")).ToArray();
        if (playerInRange.Length == 0)
        {
            player_locked = false;
            return;
        }
        GameObject player = playerInRange[0];
        playerLocation = player.transform.position;
        patrolling = false;
        player_locked = true;
    }

    protected override void Patrol()
    {
        if (player_locked) return;
        if (!patrolling && transform.position != patrolPostLocation.position)
        {
            transform.position = patrolPostLocation.position;
        }
        else
        {
            patrolling = true;
            if (facingLeft)
            {
                transform.position += Vector3.left * patrolSpeed * Time.deltaTime;
   
            }
            else
            {
                transform.position += Vector3.right * patrolSpeed * Time.deltaTime;

            }
        }
    }

    private void Charge()
    {
        if (!player_locked) return;
        // Check if any obstacles between player and enemy
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerLocation - new Vector2(transform.position.x, transform.position.y), Mathf.Infinity, rayCastLayers);
        if (hit.collider != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, hit.point, Time.deltaTime * chargeSpeed);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, playerLocation, Time.deltaTime * chargeSpeed);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        patrolling = true;
        facingLeft = true;
        player_locked = false;

        // Set initial location far away
        playerLocation = new Vector2(float.MaxValue, float.MaxValue);

        mySpriteRenderer = GetComponent<SpriteRenderer>();
        xval = transform.position.x;
    }

    private void Update()
    {
        if (GameManager.S.gameState == GameManager.GameState.playing)
        {
            Patrol();
            OnPlayerSighting();
            Charge();

            if (transform.position.x - xval >= 0)
            {
                mySpriteRenderer.flipX = false;
            }
            else
            {
                mySpriteRenderer.flipX = true;
            }

            xval = transform.position.x;
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (patrolling && collision.gameObject.layer == 8)
        {
            facingLeft = !facingLeft;
            mySpriteRenderer.flipX = !mySpriteRenderer.flipX;
        }        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.S.takeDamage();
            Vector2 dir = collision.gameObject.transform.position - transform.position;
            dir.Normalize();
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * repelStrength, ForceMode2D.Impulse);
        }
    }
}
