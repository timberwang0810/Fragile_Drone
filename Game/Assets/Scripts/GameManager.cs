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

    public GameObject middlePanel;
    public TextMeshProUGUI gameStateText;
    private bool paused;

    public int getReadyTime;
    public int gameTime;
    public int startHP;
    private int currHP;
    public int score;

    public GameObject playerObject;
    private Animator playerAnimator;

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
        paused = false;
        middlePanel.SetActive(false);
        Time.timeScale = 1;
        StartNewGame();
    }

    private void Update()
    {
        if (gameState != GameState.playing ) return;
        if (Input.GetKeyDown(KeyCode.Escape) && !paused) OnGamePaused();
        else if (Input.GetKeyDown(KeyCode.Escape) && paused) OnGameUnpaused();
    }

    private void StartNewGame()
    {
        playerAnimator = playerObject.GetComponent<Animator>();
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
        gameStateText.text = "Game Over\nScore: " + score;
        middlePanel.SetActive(true);
    }

    public void takeDamage()
    {
        playerAnimator.SetTrigger("hurt");
        SoundManager.S.DroneHit();
        if (currHP <= 0) return;
        currHP -= 1;
        if (currHP == 0) OnPlayerDeath();
    }

    private void OnPlayerWon()
    {
        StopAllCoroutines();
        gameState = GameState.oops;
        StartCoroutine(DisplayStatusTextAtEnd("You win!"));
    }

    private void OnPlayerLost()
    {
        StopAllCoroutines();
        gameState = GameState.oops;
        StartCoroutine(DisplayStatusTextAtEnd("You lost~~"));
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
            //Debug.Log(i);
            yield return new WaitForSeconds(1);
            timeText.text = "Time: " + (gameTime - i);

        }
        if (gameState == GameState.playing)
        {
            OnPlayerLost();
        }
    }

    private IEnumerator DisplayStatusTextAtEnd(string text)
    {
        statusText.text = text;
        statusText.enabled = true;
        yield return new WaitForSeconds(5);
        statusText.enabled = false;
        GameOver();
    }

    public void scored()
    {
        score += 1;
        scoreText.text = "Score: " + score;
    }

    public void OnGamePaused()
    {
        gameStateText.text = "Game Paused\nScore: " + score;
        middlePanel.SetActive(true);
        Time.timeScale = 0;
        paused = true;
    }

    public void OnGameUnpaused()
    {
        middlePanel.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }

}
