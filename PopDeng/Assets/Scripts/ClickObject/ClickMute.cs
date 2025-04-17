using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickMute : MonoBehaviour, IPointerClickHandler
{
    public static ClickMute Instance;
    [SerializeField] GameObject speakerIcon;
    [SerializeField] Sprite[] speakerSprite;

    private bool isMute = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

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

    public bool GetIsMute()
    {
        return isMute;
    }
}
