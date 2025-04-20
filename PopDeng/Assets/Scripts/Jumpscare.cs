using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpscare : MonoBehaviour
{
    [SerializeField] GameObject[] monster;
    [SerializeField] GameObject gameoverTimeline;
    [SerializeField] AudioSource jumpscareSource;
    [SerializeField] AudioClip jumpscareClips;

    private int jumpscareIndex;

    // Start is called before the first frame update
    void Start()
    {
        jumpscareIndex = PlayerPrefs.GetInt("Monster");
        StartCoroutine(ActiveJumpscare());
    }

    IEnumerator ActiveJumpscare()
    {
        jumpscareSource.clip = jumpscareClips;
        yield return null;

        monster[jumpscareIndex].SetActive(true);
        jumpscareSource.Play();

        yield return new WaitForSeconds(1.9f);
        gameoverTimeline.SetActive(true);

        yield return new WaitForSeconds(0.3f);
        monster[jumpscareIndex].SetActive(false);

        yield return new WaitForSeconds(10.0f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }
}
