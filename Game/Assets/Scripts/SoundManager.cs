using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



    AudioSource audio;


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


    // Update is called once per frame
    void Update()
    {
        
    }

    public void playMusic()
    {
        audio.Play();
    }

    public void stopMusic()
    {
        audio.Stop();
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
