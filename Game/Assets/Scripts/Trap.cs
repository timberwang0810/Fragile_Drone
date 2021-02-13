using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float damage;
    public float timeBetweenDamages;

    private bool justDamaged = false;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        GameManager.S.takeDamage();
    //    }
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !justDamaged)
        {
            GameManager.S.takeDamage();
            StartCoroutine(DamageDelay(timeBetweenDamages));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") justDamaged = false;
    }

    private IEnumerator DamageDelay(float seconds)
    {
        justDamaged = true;
        yield return new WaitForSeconds(seconds);
        justDamaged = false;
    }
}
