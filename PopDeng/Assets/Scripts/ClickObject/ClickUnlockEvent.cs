using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickUnlockEvent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int targetEvent;
    [SerializeField] private AudioClip eventClip;
    private bool isActive = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Clicked {gameObject.name}");

        if(!isActive)
            StartCoroutine(UnlockEvent());
    }

    IEnumerator UnlockEvent()
    {
        isActive = true;

        if(eventClip != null)
        {
            SoundFXManager.Instance.PlaySoundFXClip(eventClip, transform, false, 1.0f);
            yield return new WaitForSeconds(eventClip.length);
        }

        EventManager.Instance.UpdateEventDataTrigger(targetEvent, true);

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
