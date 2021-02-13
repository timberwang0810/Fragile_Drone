using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChargingEnemy : Enemy
{

    public float searchRadius;
    public float speed;
    private bool player_locked;
    private bool patrolling;
    private bool facingLeft;
    private Vector2 playerLocation;

    public LayerMask rayCastLayers;
    public Transform patrolPostLocation;
    protected override void OnEnemyDeath()
    {
        Debug.Log("Ahhh im dead");
        // TODO: Add enemy explosion effects
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
            // TODO: Add cool teleportation effect
        }
        else
        {
            patrolling = true;
            if (facingLeft)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            else
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
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
            transform.position = Vector2.MoveTowards(transform.position, hit.point, Time.deltaTime * speed);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, playerLocation, Time.deltaTime * speed);
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
    }

    private void Update()
    {
        Patrol();
        OnPlayerSighting();
        Charge();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (patrolling && collision.gameObject.layer == 8)
        {
            facingLeft = !facingLeft;
        }
    }
}
