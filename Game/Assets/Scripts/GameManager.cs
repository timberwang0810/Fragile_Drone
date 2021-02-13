using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager S;

    public int startHP;
    private int currHP;

    private void Awake()
    {
        // Singleton Definition
        if (GameManager.S)
        {
            // singleton exists, delete this object
            Destroy(this.gameObject);
        }
        else
        {
            S = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currHP = startHP;
    }

    public void takeDamage()
    {
        if (currHP <= 0) return;
        currHP -= 1;
        Debug.Log(currHP);
        if (currHP == 0) OnPlayerDeath();
    }

    private void OnPlayerDeath()
    {
        Debug.Log("hi im dead inside");
    }
}
