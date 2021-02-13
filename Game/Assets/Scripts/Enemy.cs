using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    public float HP = 5;
    protected void TakeDamage(float damage)
    {
        if (HP <= 0) return;
        HP -= damage;
        if (HP <= 0) OnEnemyDeath();
    }

    protected abstract void OnPlayerSighting();
    protected abstract void OnEnemyDeath();
}
