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

    private AudioSource audio;

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
        audio.Play();
        MuteButton.gameObject.SetActive(true);
        UnmuteButton.gameObject.SetActive(false);
    }

    public void stopMusic()
    {
        audio.Stop();
        MuteButton.gameObject.SetActive(false);
        UnmuteButton.gameObject.SetActive(true);
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



}
