using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickMute : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject speakerIcon;
    [SerializeField] Sprite[] speakerSprite;

    private bool isMute = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Clicked {gameObject.name}");
        ToggleSound();
    }

    void ToggleSound()
    {
        isMute = !isMute;

        if (isMute)
        {
            speakerIcon.GetComponent<SpriteRenderer>().sprite = speakerSprite[1];
        }
        else
        {
            speakerIcon.GetComponent<SpriteRenderer>().sprite = speakerSprite[0];
        }

    }
}
