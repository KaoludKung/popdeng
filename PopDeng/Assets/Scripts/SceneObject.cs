using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObject : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private float times;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSecondsRealtime(times);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
