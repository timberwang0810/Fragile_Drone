using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emission : MonoBehaviour
{
    // Emitted Substance (e.g. fire, steam)
    public GameObject substance;
    public AudioSource audio;

    // Time of emission breaks
    public float timeBetweenEmission;

    // Time of emission
    public float timeOfEmission;

    // Start delay -- gives all emissions different start times
    public float timeStartDelay;

    private Animator emissionAnimator;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        emissionAnimator = substance.GetComponent<Animator>();
        StartEmission();
    }

    private void StartEmission()
    {
        StartCoroutine(StartDelay());
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(timeStartDelay);
        StartCoroutine(StartEmitLoop());
    }

    private IEnumerator StartEmitLoop()
    {
        while (true)
        {
            audio.Play();
            emissionAnimator.SetBool("Emitting", true);
            yield return new WaitForSeconds(timeOfEmission);
            emissionAnimator.SetBool("Emitting", false);
            audio.Stop();
            yield return new WaitForSeconds(timeBetweenEmission);
            
        }
    }

    /**
     * Alternate Version below
     **/

    //private void StartEmitLoop()
    //{
    //    StartCoroutine(Emit());
    //}

    //private IEnumerator Emit()
    //{ 
    //    substance.SetActive(true);
    //    yield return new WaitForSeconds(timeOfEmission);
    //    substance.SetActive(false);
    //    yield return new WaitForSeconds(timeBetweenEmission);
    //    StartEmitLoop();
    //}
}
