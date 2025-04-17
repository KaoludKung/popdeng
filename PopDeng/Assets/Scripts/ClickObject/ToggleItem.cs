using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToggleItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject targetObject;
    [SerializeField] AudioClip clickClip;

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(ToggleOpenCloseItem());
    }

    IEnumerator ToggleOpenCloseItem()
    {
        if (clickClip != null)
        {
            SoundComputer.Instance.PlaySoundComputer(clickClip, 0.8f);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
        }

        targetObject.SetActive(!targetObject.activeSelf);
    }
}
