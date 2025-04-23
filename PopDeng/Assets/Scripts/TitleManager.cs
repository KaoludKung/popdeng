using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleManager : MonoBehaviour
{
    //0: Let's Pop 1: Endless Pop 2: Shut Down
    [SerializeField] private Button[] titleButton;
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private GameObject powerOn;
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private AudioClip clickClip;
    
    private bool isClicked = false;
    const int MaxScore = 9999999;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        ShowHighScore();

        titleButton[0].onClick.AddListener(() => TryStartCoroutine(PlayPop()));
        titleButton[1].onClick.AddListener(() => TryStartCoroutine(EndlessPop()));
        titleButton[2].onClick.AddListener(() => TryStartCoroutine(ShutDown()));
    }

    void TryStartCoroutine(IEnumerator coroutine)
    {
        if (!isClicked)
        {
            StartCoroutine(coroutine);
        }
    }

    // Play Story Mode
    IEnumerator PlayPop()
    {
        ActivatePowerOnEffect();

        yield return new WaitForSeconds(1.5f);
        blackScreen.SetActive(true);

        yield return new WaitForSeconds(3.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainStory");
    }

    // Play Endless Mode
    IEnumerator EndlessPop()
    {
        if (PlayerDataManager.Instance.GetUnlockEndless())
        {
            ActivatePowerOnEffect();

            yield return new WaitForSeconds(1.5f);
            blackScreen.SetActive(true);

            yield return new WaitForSeconds(3.5f);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Endless");
        }
        else
        {
            isClicked = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    // Exit Game
    IEnumerator ShutDown()
    {
        ActivatePowerOnEffect();
        yield return new WaitForSeconds(2.5f);
        Application.Quit();
    }

    void ActivatePowerOnEffect(bool hideCursor = true)
    {
        isClicked = true;
        if (hideCursor)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        SoundFXManager.Instance.PlaySoundFXClip(clickClip, transform, false, 0.7f);
        powerOn.SetActive(true);
    }

    //Show highscore from Endless mode
    void ShowHighScore()
    {
        if (PlayerDataManager.Instance.GetHighScores() > MaxScore)
        {
            highscoreText.text = "Highscore: " + MaxScore;
        }
        else
        {
            highscoreText.text = "Highscore: " + PlayerDataManager.Instance.GetHighScores().ToString("D7");
        }
    }

    //Other script use this method
    public bool IsClicked()
    {
        return isClicked;
    }
}
