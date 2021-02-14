using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager S;


    public AudioClip droneHit1;
    public AudioClip droneHit2;
    public AudioClip droneHit3;
    public AudioClip boxHitSoft1;
    public AudioClip boxHitSoft2;
    public AudioClip boxHitSoft3;
    public AudioClip boxHitHard1;
    public AudioClip boxHitHard2;
    public AudioClip boxHitHard3;
    public AudioClip pipes1;
    public AudioClip pipes2;
    public AudioClip pipes3;
    public AudioClip enemyAlert;
    public AudioClip gameOver;
    public AudioClip win;
    public AudioClip bombExplode;
    public AudioClip woodHit1;
    public AudioClip woodHit2;
    public AudioClip woodBreak;


    private AudioSource audio;
    public AudioSource bgm;

    public Button MuteButton;
    public Button UnmuteButton;

    //public AudioClip bgm;

    private void Awake()
    {
        S = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void playMusic()
    {
        bgm.Play();
        MuteButton.gameObject.SetActive(true);
        UnmuteButton.gameObject.SetActive(false);
    }

    public void stopMusic()
    {
        bgm.Stop();
        MuteButton.gameObject.SetActive(false);
        UnmuteButton.gameObject.SetActive(true);
    }

    public void explodeBomb()
    {
        audio.PlayOneShot(bombExplode);
    }

    public void breakWood()
    {
        audio.PlayOneShot(woodBreak);
    }

    public void DroneHit()
    {
        int r = Random.Range(1, 3);
        if (r == 1)
        {

            audio.PlayOneShot(droneHit1);
            audio.PlayOneShot(pipes1);
        }
        else if (r == 2)
        {
            audio.PlayOneShot(droneHit2);
            audio.PlayOneShot(pipes2);

        }
        else
        {
            audio.PlayOneShot(droneHit3);
            audio.PlayOneShot(pipes3);

        }
    }

    public void DronePipe()
    {
        int r = Random.Range(1, 3);
        if (r == 1)
        {
            audio.PlayOneShot(pipes1, 0.7F);
        }
        else if (r == 2)
        {
            audio.PlayOneShot(pipes2, 0.7F);

        }
        else
        {
            audio.PlayOneShot(pipes3, 0.7F);
        }
    }

    public void WoodHit()
    {
        int r = Random.Range(1, 2);
        if (r == 1)
        {
            audio.PlayOneShot(woodHit1, 2f);
        }
        else
        {
            audio.PlayOneShot(woodHit2, 2f);
        }
    }

    public void BoxLandHard()
    {
        int r = Random.Range(1, 3);
        if (r == 1)
        {

            audio.PlayOneShot(boxHitHard1);
        }
        else if (r == 2)
        {
            audio.PlayOneShot(boxHitHard2);
        }
        else
        {
            audio.PlayOneShot(boxHitHard3);
        }
    }

    public void BoxLandSoft()
    {
        int r = Random.Range(1, 2);
        if (r == 1)
        {

            audio.PlayOneShot(boxHitSoft1);
        }
        else if (r == 2)
        {
            audio.PlayOneShot(boxHitSoft2);
        }
        else
        {
            audio.PlayOneShot(boxHitSoft3);
        }
    }

    public void EnemyAlertSound()
    {
        audio.PlayOneShot(enemyAlert);
    }

    public void GameOverSound()
    {
        audio.PlayOneShot(gameOver, 1.3f);
    }

    public void WinSound()
    {
        audio.PlayOneShot(win);
    }

}
