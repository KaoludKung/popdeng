using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSpawn : MonoBehaviour
{
    [SerializeField] int minTime;
    [SerializeField] int maxTime;
    [SerializeField] private int requirementEvent;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRandomPopup());
    }

    IEnumerator SpawnRandomPopup()
    {
        while (true)
        {
            // Check if randomPopup is not spawned and the required event is triggered
            if (EventManager.Instance.IsEventTriggered(requirementEvent))
            {
                // Randomly choose a spawn delay between minTime and maxTime seconds
                int time = Random.Range(minTime, maxTime);

                yield return new WaitForSeconds(time);
                PopupManager.Instance.CreateRandomPopup();
            }

            yield return new WaitForSeconds(2.5f); // Check every 2.5 seconds
        }
    }

    void OnDisable()
    {
        StopAllCoroutines();
        Debug.Log("PopupSapwan disabled and Coroutines stopped.");
    }
}
