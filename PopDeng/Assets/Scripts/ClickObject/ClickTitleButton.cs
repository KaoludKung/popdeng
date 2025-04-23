using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ClickTitleButton : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Image iconSelect;
    [SerializeField] private bool isEndlessPop = false;

    // Start is called before the first frame update
    void Start()
    {
        if (isEndlessPop && PlayerDataManager.Instance.GetUnlockEndless())
        {
            buttonText.color = Color.white;
            iconSelect.color = Color.white;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isEndlessPop)
        {
            if (PlayerDataManager.Instance.GetUnlockEndless())
            {
                HoverButton(0.5f, true);
            }
        }
        else
        {
            HoverButton(0.5f, true);
        } 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isEndlessPop)
        {
            if (PlayerDataManager.Instance.GetUnlockEndless())
            {
                HoverButton(1.0f, false);
            }
        }
        else
        {
            HoverButton(1.0f, false);
        }
    }

    void HoverButton(float alpha, bool value)
    {
        Color newColorA = iconSelect.color;
        Color newColorB = buttonText.color;

        newColorA.a = alpha;
        newColorB.a = alpha;

        buttonText.color = newColorB;
        iconSelect.color = newColorA;
        iconSelect.gameObject.SetActive(value);
    }
}

[System.Serializable]
public class TitleOption
{
    public TextMeshProUGUI buttonText;
    public Image iconSelect;
}
