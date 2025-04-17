using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ClickInbox : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Sprite[] inboxSprite;
    [SerializeField] private GameObject[] inboxList;
    [SerializeField] private GameObject[] mailList;
    [SerializeField] private int inboxSelected;
    [SerializeField] private TextMeshProUGUI inboxContent;
    [SerializeField] private string[] inboxDetail;
    [SerializeField] private AudioClip clickClip;
 

    private SpriteRenderer[] spriteComponent;

    private void Awake()
    {
        spriteComponent = new SpriteRenderer[inboxList.Length];
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < inboxList.Length; i++)
        {
            spriteComponent[i] = GetComponent<SpriteRenderer>();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Clicked {gameObject.name}");
        UpdateInbox();
    }

    void UpdateInbox()
    {
        SoundComputer.Instance.PlaySoundComputer(clickClip, 0.8f);

        for(int i = 0; i < inboxList.Length; i++)
        {
            TextMeshProUGUI fromText = mailList[i].transform.Find("name").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI subjectText = mailList[i].transform.Find("subject").GetComponent<TextMeshProUGUI>();

            if (i != inboxSelected)
            {
                inboxList[i].GetComponent<SpriteRenderer>().sprite = inboxSprite[0];
                fromText.color = Color.black;
                subjectText.color = Color.black;
            }
            else
            {
                inboxList[i].GetComponent<SpriteRenderer>().sprite = inboxSprite[1];
                fromText.color = Color.white;
                subjectText.color = Color.white;
                inboxContent.text = "";
                inboxContent.text = inboxDetail[i].Replace("\\n", "\n");
            }
        }
    }

    /*
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (inboxList[inboxSelected] != null)
        {
            Color newColor = spriteComponent[inboxSelected].color;
            newColor.a = 0.75f;
            spriteComponent[inboxSelected].color = newColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (inboxList[inboxSelected] != null)
        {
            Color newColor = spriteComponent[inboxSelected].color;
            newColor.a = 0.1f;
            spriteComponent[inboxSelected].color = newColor;
        }
    }*/
}
