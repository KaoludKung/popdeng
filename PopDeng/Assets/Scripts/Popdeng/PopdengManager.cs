using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopdengManager : MonoBehaviour
{
    [SerializeField] private GameObject moodengDesktop;
    [SerializeField] private TextMeshProUGUI scoreText;
    //0: click to start 1: trophy 2: leaderboard
    [SerializeField] private GameObject[] popdengUI;
    private int scores;
    private bool isStart = false;

    // Start is called before the first frame update
    void Start()
    {
        scores = -1;
    }

    // Update is called once per frame
    void Update()
    {
        //When player click moodeng first time then game start
        if(scores == 0 && moodengDesktop.activeSelf && !isStart)
        {
            isStart = true;
            StartCoroutine(StartGame());
        }

        //when popdeng is close then podeng will reset
        if(!moodengDesktop.activeSelf && isStart)
        {
            isStart = false;
            scores = -1;
            StartCoroutine(OpenUI());
            scoreText.gameObject.SetActive(false);
            scoreText.text = "0";
        }
    }

    IEnumerator StartGame()
    {
        StartCoroutine(CloseUI());
        yield return new WaitForSeconds(0.8f);
        scoreText.gameObject.SetActive(true);
    }

    IEnumerator OpenUI()
    {
        yield return new WaitForSeconds(0.5f);

        for(int i = 0; i < popdengUI.Length; i++)
        {  
            if(i != 2)
                popdengUI[i].SetActive(true);         
        }
    }

    IEnumerator CloseUI()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < popdengUI.Length; i++)
        {
            popdengUI[i].SetActive(false); 
        }
    }

    //Show score, Use in another Script
    public void ShowScoreText()
    {
        scoreText.text = scores.ToString();
    }

    //Click moodeng to add point, Use in another Script
    public void AddScore()
    {
        scores += 1;
    }

    //Another script can get score's data
    public int GetScore()
    {
        return scores;
    }
}
