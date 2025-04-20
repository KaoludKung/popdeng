using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chimpy : MonoBehaviour
{
    [SerializeField] GameObject[] spawnPoint; // Array of spawn points for Chimpy
    [SerializeField] int requirementEvent; // Event ID required to trigger Chimpy's spawn
    [SerializeField] float timeLimit = 20f; // Time limit for players to resolve an active spawn point

    private bool isSpawn = false; // Flag to check if Chimpy is already spawned
    private Coroutine timeOutCoroutine; // Reference to the timeout coroutine

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnChimpy());
    }

    // Update is called once per frame
    void Update()
    {
        // Check if any spawn point is active
        foreach (GameObject point in spawnPoint)
        {
            if (point.activeSelf && timeOutCoroutine == null)
            {
                // If a spawn point is active and no timeout coroutine is running, start the timeout
                timeOutCoroutine = StartCoroutine(TimeOut());
            }
        }
    }

    IEnumerator SpawnChimpy()
    {
        while (true)
        {
            // Check if Chimpy is not spawned and the required event is triggered
            if (!isSpawn && EventManager.Instance.IsEventTriggered(requirementEvent))
            {
                isSpawn = true;

                // Randomly choose a spawn delay between 35 and 60 seconds
                int time = Random.Range(35, 60);

                yield return new WaitForSeconds(time);

                // Randomly select a spawn point and activate it
                int index = Random.Range(0, spawnPoint.Length);
                spawnPoint[index].SetActive(true);
            }

            yield return new WaitForSeconds(2.5f); // Check every 2.5 seconds
        }
    }

    IEnumerator TimeOut()
    {
        // Start the timer
        float timer = 0f;

        while (timer < timeLimit)
        {
            // If all spawn points become inactive, stop the timeout
            if (AllSpawnPointsInactive())
            {
                timeOutCoroutine = null;
                isSpawn = false;
                yield break;
            }

            timer += Time.deltaTime; // Increment the timer
            yield return null;
        }

        // If the timer expires and any spawn point is still active, trigger the scene change
        PlayerPrefs.SetInt("Monster", 1); 
        UnityEngine.SceneManagement.SceneManager.LoadScene("Jumpscare"); 
    }

    private bool AllSpawnPointsInactive()
    {
        // Check if all spawn points are inactive
        foreach (GameObject point in spawnPoint)
        {
            if (point.activeSelf)
            {
                return false; // Found an active spawn point
            }
        }
        return true; // All spawn points are inactive
    }

    void OnDisable()
    {
        StopAllCoroutines();
        Debug.Log("Chimpy disabled and Coroutines stopped.");
    }
}
