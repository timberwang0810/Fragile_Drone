    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update

    public void btn_StartTheGame()
    {
        StartCoroutine(StartTheGame());
    }

    public IEnumerator StartTheGame()
    {
        yield return new WaitForSeconds(.7f);
        SceneManager.LoadScene("SampleScene");

    }

    public void btn_Tutorial()
    {
        StartCoroutine(Tutorial());
    }
    public IEnumerator Tutorial()
    {
        yield return new WaitForSeconds(.7f);

        SceneManager.LoadScene("Tutorial");

    }

    public void btn_Credits()
    {
        StartCoroutine(Credits());
    }
    public IEnumerator Credits()
    {
        yield return new WaitForSeconds(.7f);

        SceneManager.LoadScene("Credits");

    }

    public void btn_QuitGame()
    {
        StartCoroutine(Quit());
    }
    public IEnumerator Quit()
    {
        yield return new WaitForSeconds(.7f);

        Application.Quit();

    }
    public void btn_Back()
    {
        StartCoroutine(Back());
    }
    public IEnumerator Back()
    {
        yield return new WaitForSeconds(.7f);

        SceneManager.LoadScene("Title Menu");

    }
}
