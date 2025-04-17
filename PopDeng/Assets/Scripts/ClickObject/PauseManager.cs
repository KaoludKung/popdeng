using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject pausePanel;
    //protect from click object in computer while dialogue is playing
    [SerializeField] private GameObject detectPanel;
    [SerializeField] private AudioClip clickClip;
    private bool isPause = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Clicked {gameObject.name}");
        TogglePauseGame();
    }

    void TogglePauseGame()
    {
        SoundComputer.Instance.PlaySoundComputer(clickClip, 0.8f);
        isPause = !isPause;

        if (isPause)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            Debug.Log("Game Pause");
            detectPanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            Debug.Log("Game Unpause");
            detectPanel.SetActive(false);
        }
    }
}
