using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{

    public GameObject panel;
    public GameObject UISound;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HidePanel());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator HidePanel()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("Title Menu");

    }
}
