using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager S;

    private AudioSource audio;

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
}
