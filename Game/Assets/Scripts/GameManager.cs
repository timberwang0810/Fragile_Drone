using System.Collections;
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

    public GameObject playerObject;


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

    private void GameOver()
    {
        gameState = GameState.gameOver;
        // TODO: Restart Mechanics (UI button)
    }

    public void takeDamage()
    {
        if (currHP <= 0) return;
        currHP -= 1;
        Debug.Log(currHP);
        if (currHP == 0) OnPlayerDeath();
    }

    private void OnPlayerWon()
    {
        // TODO: Displays Game Won UI (with coroutine)
        GameOver();
    }

    private void OnPlayerLost()
    {
        // TODO: Displays Game Lost UI (with coroutine)
        GameOver();
    }
    private void OnPlayerDeath()
    {
        Debug.Log("hi im dead inside");
        // TODO: Add explosion / death effects
        playerObject.transform.DetachChildren();
        Destroy(playerObject);
        OnPlayerLost();
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
