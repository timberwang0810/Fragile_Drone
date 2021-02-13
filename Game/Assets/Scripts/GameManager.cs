﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { menu, getReady, playing, oops, gameOver };
    // Game State
    public GameState gameState;
    // Singleton Declaration
    public static GameManager S;

    public int getReadyTime;
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
        StartNewGame();
    }

    private void StartNewGame()
    {
        gameState = GameState.getReady;
        StartCoroutine(GetReady());
    }

    private void StartRound()
    {
        gameState = GameState.playing;
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

    private IEnumerator GetReady()
    {
        Debug.Log("GetReady!");
        // TODO: Show pop-up message telling player to get ready
        yield return new WaitForSeconds(getReadyTime);
        // TODO: Hide message
        Debug.Log("Go");
        StartRound();
    }
}
