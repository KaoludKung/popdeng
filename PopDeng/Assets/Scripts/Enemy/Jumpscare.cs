using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Jumpscare : MonoBehaviour
{
    [SerializeField] GameObject[] monster;
    [SerializeField] GameObject gameoverTimeline;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] AudioSource jumpscareSource;
    [SerializeField] AudioClip jumpscareClips;

    private int jumpscareIndex;
    const int MaxScore = 9999999;

    // Start is called before the first frame update
    void Start()
    {
        ManageHighScore();
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

    void ManageHighScore()
    {
        if (PlayerDataManager.Instance.GetIsEndlessmode())
        {
            PlayerDataManager.Instance.UpdateIsEndlessmode(false);

            if (PlayerDataManager.Instance.GetScores() > PlayerDataManager.Instance.GetHighScores())
            {
                PlayerDataManager.Instance.UpdateHighScores(PlayerDataManager.Instance.GetScores());
            }

            PlayerDataManager.Instance.SavePlayerData();
            ShowHighScore();
        }
    }

    void ShowHighScore()
    {
        if (PlayerDataManager.Instance.GetScores() > MaxScore)
        {
            scoreText.text = "Highscore: " + MaxScore;
        }
        else
        {
            scoreText.text = "Highscore: " + PlayerDataManager.Instance.GetScores().ToString("D7");
        }

        scoreText.gameObject.SetActive(true);
    }
}
