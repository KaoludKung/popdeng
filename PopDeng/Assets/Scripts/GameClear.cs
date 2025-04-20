using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToDisable;
    [SerializeField] private MonoBehaviour[] scriptsToDisable;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        StartCoroutine(ClearGame());
    }

    public IEnumerator ClearGame()
    {
        yield return new WaitForSecondsRealtime(0.25f);

        foreach (var script in scriptsToDisable)
        {
            script.enabled = false;
        }

        yield return new WaitForSecondsRealtime(0.5f);

        foreach (var obj in objectsToDisable)
        {
            obj.SetActive(false);
        }

        Debug.Log("Game Cleared, objects disabled!");
    }
}
