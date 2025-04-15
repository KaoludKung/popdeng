using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (spriteRenderer != null)
        {
            Color newColor = spriteRenderer.color;
            newColor.a = 0.75f;
            spriteRenderer.color = newColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (spriteRenderer != null)
        {
            Color newColor = spriteRenderer.color;
            newColor.a = 1f;
            spriteRenderer.color = newColor;
        }
    }
}
