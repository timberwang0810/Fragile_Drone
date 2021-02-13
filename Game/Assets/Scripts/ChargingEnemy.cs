using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChargingEnemy : Enemy
{

    public float searchRadius;
    public float speed;
    private bool player_locked;
    private Vector2 playerLocation;

    public LayerMask rayCastLayers;
    protected override void OnEnemyDeath()
    {
        Debug.Log("Ahhh im dead");
        // TODO: Add enemy explosion effects
        Destroy(this.gameObject);
    }

    protected override void OnPlayerSighting()
    {
        GameObject[] playerInRange = Physics2D.OverlapCircleAll(transform.position, searchRadius).Select<Collider2D, GameObject>(c => c.gameObject).Where<GameObject>(obj => obj.CompareTag("Player")).ToArray();
        //Debug.Log(playerInRange.Length);
        if (playerInRange.Length == 0)
        {
            player_locked = false;
            //Debug.Log("1 REACHED");
            return;
        }
        //if (player_locked && Vector2.Distance(transform.position, playerLocation) > 0)
        //{
        //    Debug.Log("2 REACHED");
        //    return;
        //}
        //Debug.Log("3 REACHED");
        GameObject player = playerInRange[0];
        playerLocation = player.transform.position;
        Debug.Log("New Position");
        player_locked = true;
    }

    private void Charge()
    {
        if (!player_locked) return;
        // Check if any obstacles between player and enemy
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerLocation - new Vector2(transform.position.x, transform.position.y), Mathf.Infinity, rayCastLayers);
        if (hit.collider != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, hit.point, Time.deltaTime * speed);
            if (Vector2.Distance(transform.position, hit.point) == 0)
            {
                player_locked = false;
                return;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, playerLocation, Time.deltaTime * speed);
            if (Vector2.Distance(transform.position, playerLocation) == 0)
            {
                player_locked = false;
                return;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // TODO: Patrol called here
        player_locked = false;

        // Set initial location far away
        playerLocation = new Vector2(float.MaxValue, float.MaxValue);
    }

    private void Update()
    {
        OnPlayerSighting();
        Charge();
    }
}
