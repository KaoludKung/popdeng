using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private SpriteRenderer spriteComponent; 
    private Image uiImage;

    private void Awake()
    {
        spriteComponent = GetComponent<SpriteRenderer>();
        uiImage = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (spriteComponent != null)
        {
            Color newColor = spriteComponent.color;
            newColor.a = 0.75f;
            spriteComponent.color = newColor;
        }

        if (uiImage != null)
        {
            Color newColor = uiImage.color;
            newColor.a = 0.75f;
            uiImage.color = newColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (spriteComponent != null)
        {
            Color newColor = spriteComponent.color;
            newColor.a = 1f;
            spriteComponent.color = newColor;
        }

        if (uiImage != null)
        {
            Color newColor = uiImage.color;
            newColor.a = 1f;
            uiImage.color = newColor;
        }
    }
}
