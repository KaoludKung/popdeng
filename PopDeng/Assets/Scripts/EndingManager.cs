using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class EndingManager : MonoBehaviour
{
    [SerializeField] GameObject[] objectsToDisable;
    [SerializeField] GameObject[] objectsToEnable;
    [SerializeField] GameObject continueArrow;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private AudioSource congratSoure;
    [SerializeField] private AudioClip clickClip;

    private int currentIndex = 0;

    private void Awake()
    {
        Time.timeScale = 1;
        videoPlayer.frame = 0;
        videoPlayer.playOnAwake = false;
        videoPlayer.isLooping = true;
    }

    void Start()
    {
        //Manage condition to unlock endless mode
        StartCoroutine(CheckEndlessMode());

        //nextArrow will appear in 6 seconds
        StartCoroutine(SetActiveArrow(true, 6f));
        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.Prepare();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && continueArrow.activeSelf)
        {
            if(currentIndex == 0)
            {
                //nextArrow will disappear when condition is right
                StartCoroutine(SetActiveArrow(false, 1.5f));
                SoundFXManager.Instance.PlaySoundFXClip(clickClip, transform, false, 0.8f);
                StartCoroutine(SetActiveObject());
            }else if(currentIndex == 1)
            {
                //nextArrow will disappear when condition is right
                StartCoroutine(SetActiveArrow(false, 1.5f));
                SoundFXManager.Instance.PlaySoundFXClip(clickClip, transform, false, 0.8f);
                StartCoroutine(GoToTitle());
            }
        }
    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        Debug.Log("Video is ready to play!");
        StartCoroutine(PlayVideoWithDelay(0.5f));
    }

    IEnumerator PlayVideoWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        videoPlayer.Play();
        congratSoure.Play();
    }

    IEnumerator SetActiveArrow(bool value, float times)
    {
        yield return new WaitForSecondsRealtime(times);
        continueArrow.SetActive(value);
    }

    IEnumerator CheckEndlessMode()
    {
        if (!PlayerDataManager.Instance.GetUnlockEndless())
        {
            PlayerDataManager.Instance.UpdateUnlockEndless(true);
            PlayerDataManager.Instance.SavePlayerData();

            yield return new WaitForSeconds(1.5f);
            objectsToDisable[0].SetActive(true);
        }
        else
        {
            objectsToDisable[0].SetActive(false);
        }
    }

    IEnumerator SetActiveObject()
    {
        currentIndex++;
        yield return new WaitForSecondsRealtime(1.5f);

        for (int i = 0; i < objectsToDisable.Length; i++)
        {
            if (objectsToDisable[i] != null)
                objectsToDisable[i].SetActive(false);
        }

        yield return null;

        for (int i = 0; i < objectsToEnable.Length; i++)
        {
            if (objectsToEnable[i] != null)
                objectsToEnable[i].SetActive(true);
        }

        //nextArrow will appear again
        StartCoroutine(SetActiveArrow(true, 6f));
    }

    IEnumerator GoToTitle()
    {
        currentIndex++;
        yield return new WaitForSecondsRealtime(2.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }

}
