using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public enum GameState { menu, getReady, playing, oops, gameOver };
    // Game State
    public GameState gameState;
    // Singleton Declaration
    public static GameManager S;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI statusText;

    public int getReadyTime;
    public int gameTime;
    public int startHP;
    private int currHP;
    public int score;

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
        score = 0;
        currHP = startHP;
        timeText.text = "Time: " + gameTime;
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
        SoundManager.S.playMusic();
        StartCoroutine(GameTimer());
    }

    private void GameOver()
    {
        gameState = GameState.gameOver;
        SoundManager.S.stopMusic();
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
        StopAllCoroutines();
        gameState = GameState.oops;
        // TODO: Displays Game Won UI (with coroutine)
        Debug.Log("WON!");
        GameOver();
    }

    private void OnPlayerLost()
    {
        StopAllCoroutines();
        gameState = GameState.oops;
        // TODO: Displays Game Lost UI (with coroutine)
        Debug.Log("LOST!");
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
        statusText.enabled = true;
        statusText.text = "Get Ready!";
        for (int i = 0; i < getReadyTime; i++)
        {
            statusText.text = (getReadyTime - i).ToString();
            yield return new WaitForSeconds(1);
        }

        statusText.text = "Go!";
        yield return new WaitForSeconds(1);
        statusText.enabled = false;
        Debug.Log("Go");
        StartRound();
    }

    private IEnumerator GameTimer()
    {
        for (int i = 0; i < gameTime; i++)
        {
            Debug.Log(i);
            yield return new WaitForSeconds(1);
            timeText.text = "Time: " + (gameTime - i);

        }
        if (gameState == GameState.playing)
        {
            OnPlayerLost();
        }
    }

    public void scored()
    {
        score += 1;
        scoreText.text = "Score: " + score;
    }
}
