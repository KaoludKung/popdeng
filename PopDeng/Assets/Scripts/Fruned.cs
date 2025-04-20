using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruned : MonoBehaviour
{
    [SerializeField] private GameObject Window;
    [SerializeField] private Sprite[] windowSprite;
    [SerializeField] private GameObject frontCamera;
    
    private bool isTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FreundSpawn());
    }

    
    IEnumerator FreundSpawn()
    {
        while (true)
        {
            if (frontCamera != null && !frontCamera.activeSelf && !isTrigger)
            {
                int times = Random.Range(5, 10);
                yield return new WaitForSeconds(times);

                int successRate = Random.Range(1, 10);

                //20% to spawn fruend
                if (successRate < 3 && !frontCamera.activeSelf)
                {
                    isTrigger = true;
                    Window.GetComponent<SpriteRenderer>().sprite = windowSprite[1];

                    //Players have a chance to see it for 5 seconds.
                    yield return new WaitForSeconds(5.0f);
                    StartCoroutine(FrunedDisappear());
                }
                else
                {
                    Debug.Log("Spwan failed");
                }

            }

            yield return new WaitForSeconds(2.5f);
        }

    }

    IEnumerator FrunedDisappear()
    {
        while (true)
        {
            if (Window.GetComponent<SpriteRenderer>().sprite == windowSprite[1] && !frontCamera.activeSelf)
            {
                yield return new WaitForSeconds(1.5f);
                Window.GetComponent<SpriteRenderer>().sprite = windowSprite[0];
                StopAllCoroutines();
            }

            yield return new WaitForSeconds(2.5f);
        }
    }
}
