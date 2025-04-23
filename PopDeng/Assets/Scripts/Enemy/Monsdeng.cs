using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Monsdeng : MonoBehaviour
{
    public static Monsdeng Instance { get; private set; }

    //Moodengs have 5 phases
    [SerializeField] private GameObject[] monsterVisual;
    [SerializeField] private Light2D flashLight;
    [SerializeField] private AudioSource heartbeatSoure;
    [SerializeField] private int maxPhase = 5;

    [SerializeField] private int initialDelay = 10; // Initial delay for phase transition
    [SerializeField] private int minDelay = 4; // Minimum delay as the game progresses
    [SerializeField] private int delayReductionRate = 1; // How much the delay reduces each phase

    private int currentDelay;
    private int currentPhase = 0;
    private Coroutine huntingCoroutine;
    private PopdengManager popdengManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentDelay = initialDelay;
        huntingCoroutine = StartCoroutine(Hunting());
        popdengManager = FindAnyObjectByType<PopdengManager>();
    }

    private void Update()
    {
        if (currentPhase > 3 && !heartbeatSoure.isPlaying)
        {
            heartbeatSoure.Play();
        }
        else if (currentPhase <= 3 && heartbeatSoure.isPlaying)
        {
            heartbeatSoure.Stop();
        }
    }

    IEnumerator Hunting()
    {
        while (true)
        {
            if(currentPhase == 0)
            {
                int firstAppear = Random.Range(20, 40);
                yield return new WaitForSeconds(firstAppear);
            }
            else
            {
                int randomDelay = Random.Range(3, currentDelay + 1);
                yield return new WaitForSeconds(randomDelay);
            }
            
            currentPhase++;

            if (currentPhase >= maxPhase)
            {
                // Player failed to stop the enemy
                StartCoroutine(GameOver());
                yield break;
            }

            ChangePhase();
        }
    }

    void ChangePhase()
    {
        //Monsdeng change her visual according to the currentPhase.
        for (int i = 0; i < monsterVisual.Length; i++)
        {
            //MonsterVisual are array 
            if (i == (currentPhase - 1))
            {
                monsterVisual[i].SetActive(true);
            }
            else
            {
                monsterVisual[i].SetActive(false);
            }
        }
    }

    //Another script use this method
    public void StopEnemy()
    {
        if(currentPhase > 1 && flashLight.intensity == 1)
            StartCoroutine(ResetEnemy());
    }

    IEnumerator ResetEnemy()
    {
        //When Player open flashlight (flashLight.intensity = 1)
        if (huntingCoroutine != null)
        {
            StopCoroutine(huntingCoroutine);
        }

        //Player can't move for a moment
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //A signal that the player can stop Monsdeng.
        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(0.25f);
            flashLight.intensity = i % 2 == 0 ? 0.0f : 1.0f;

            if (i == 4)
                DeactivateVisual();
        }

        yield return new WaitForSeconds(0.5f);
        
        currentPhase = 0;

        //Player can move
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //Moodeng hunts again
        currentDelay = Mathf.Max(currentDelay - delayReductionRate, minDelay); // Reduce delay
        huntingCoroutine = StartCoroutine(Hunting());
    }

    void DeactivateVisual()
    {
        for(int i = 0; i < monsterVisual.Length; i++)
        {
            monsterVisual[i].SetActive(false);
        }
    }

    IEnumerator GameOver()
    {
        //When currentIndex => maxPhase then Game Over!!!
        PlayerPrefs.SetInt("Monster", 0);

        if (SceneManager.GetActiveScene().name == "Endless")
        {
            PlayerDataManager.Instance.UpdateScores(popdengManager.GetScore());
            PlayerDataManager.Instance.UpdateIsEndlessmode(true);
            PlayerDataManager.Instance.SavePlayerData();
        }
        
        yield return null;

        SceneManager.LoadScene("Jumpscare");
    }

    void OnDisable()
    {
        StopAllCoroutines(); 
        Debug.Log("Monsdeng disabled and Coroutines stopped.");
    }
}
