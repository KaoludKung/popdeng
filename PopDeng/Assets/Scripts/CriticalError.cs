using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalError : MonoBehaviour
{
    [SerializeField] private GameObject errorPopup;
    [SerializeField] private GameObject areaProtection;
    //total: 16 blocks
    [SerializeField] private GameObject[] progressBlock;
    [SerializeField] private int requirementEvent;
    [SerializeField] private AudioClip errorClip;

    private bool isSpawn = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnErrorPopup());
    }

    IEnumerator SpawnErrorPopup()
    {
        while (true)
        {
            // Check if errorPopup is not spawned and the required event is triggered
            if (!isSpawn && EventManager.Instance.IsEventTriggered(requirementEvent))
            {
                isSpawn = true;

                // Randomly choose a spawn delay between 90 and 210 seconds
                int time = Random.Range(90, 210);

                yield return new WaitForSeconds(time);
                errorPopup.SetActive(true);
                areaProtection.SetActive(true);
                SoundComputer.Instance.PlaySoundComputer(errorClip, 0.8f);
                StartCoroutine(TroubleShooting());
            }

            yield return new WaitForSeconds(2.5f); // Check every 2.5 seconds
        }
    }

    IEnumerator TroubleShooting()
    {
        //Let the progressBlock appear one by one
        for (int i = 0; i < progressBlock.Length; i++)
        {
            progressBlock[i].SetActive(true);
            yield return new WaitForSeconds(1.8f);
        }

        yield return new WaitForSeconds(1.5f);
        errorPopup.SetActive(false);
        areaProtection.SetActive(false);

        //Reset all progressBlock
        for (int i = 0; i < progressBlock.Length; i++)
        {
            progressBlock[i].SetActive(false);
        }

        isSpawn = false;
    }

    void OnDisable()
    {
        StopAllCoroutines();
        Debug.Log("CriticalError disabled and Coroutines stopped.");
    }
}
