using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickChangeScene : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string nameScene;
    [SerializeField] private float times;
    [SerializeField] private AudioClip clickClip;

   
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Clicked {gameObject.name}");
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        if(clickClip != null)
        {
            SoundComputer.Instance.PlaySoundComputer(clickClip, 0.8f);
            yield return new WaitForSecondsRealtime(clickClip.length + times);
        }
        else
        {
            yield return new WaitForSecondsRealtime(times);
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(nameScene);
    }
}
