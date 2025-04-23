using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Intro : MonoBehaviour
{
    [SerializeField] GameObject[] warningElements;
    private bool isSkip = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowWarningElement());
        StartCoroutine(GoToTitle());
    }

    private void Update()
    {
        if(!isSkip && (Input.GetMouseButtonDown(0)))
        {
            isSkip = true;
            StartCoroutine(SkipToTitle());    
        }
    }

    IEnumerator ShowWarningElement()
    {
        warningElements[0].SetActive(true);

        yield return new WaitForSeconds(0.15f);

        for(int i = 1; i < warningElements.Length; i++)
        {
            warningElements[i].SetActive(true);
        }

        yield return new WaitForSeconds(0.15f);
        isSkip = false;
    }

    IEnumerator GoToTitle()
    {
        yield return new WaitForSeconds(7.5f);

        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Title");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    IEnumerator SkipToTitle()
    {
        yield return new WaitForSeconds(1.0f);

        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Title");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
