using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : EventObject
{
    [SerializeField] private float durationUnlock = 0;
    [SerializeField] private Conversation conversation;
    [SerializeField] private DialogueOption dialogueOption;
    [SerializeField] private DialogueUI dialogueUI;

    //Another option
    [SerializeField] List<ItemActive> itemActives;

    //[SerializeField] private GameObject uiManagerObject;
    //[SerializeField] private Animator[] animators;
    //[SerializeField] private bool resetAnimation = false;
    [SerializeField] private bool playerEnable = false;

    private float speed = 0.05f;
    private int index;
    private int charIndex;
    private bool started;
    private bool waitForNext;
    private bool isWriting;
    private bool isSkipping;

    private void Awake()
    {
        Time.timeScale = 0;
        UIManager.Instance.SetActiveUI(false);
    }


    void Start()
    {
        if (!started && !EventManager.Instance.IsEventTriggered(eventID))
            return;

        started = true;
        GetDialogue(0);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isWriting)
            {
                isSkipping = true;
            }
            else if (waitForNext)
            {
                dialogueUI.InitializeText();
                dialogueUI.ToggleArrow(false);
                waitForNext = false;
                index++;

                if (index < conversation.lines.Length)
                {
                    GetDialogue(index);
                }
                else
                {
                    EndDialogue();
                }
            }
        }
    }

    private void GetDialogue(int i)
    {
        index = i;
        charIndex = 0;
        isWriting = true;
        isSkipping = false;

        string currentName = conversation.lines[i].character.fullname;
        string currentText = conversation.lines[i].text;

        SetActiveItem(i);

        dialogueUI.ShowDialogue(currentName, currentText, dialogueOption);
        StartCoroutine(Writing());
    }

    void SetActiveItem(int i)
    {
        if(itemActives.Count > 0)
        {
            for (int j = 0; j < itemActives.Count; j++)
            {
                ItemActive option = itemActives[j];

                if (i == option.targetIndex)
                {
                    option.gameObjectActive.SetActive(true);
                }
            }
        }
    }

    IEnumerator Writing()
    {
        dialogueUI.PlaySound(conversation.lines[index].character);

        while (charIndex < conversation.lines[index].text.Length)
        {
            if (isSkipping)
            {
                dialogueUI.DialogueText.text = conversation.lines[index].text;
                break;
            }

            dialogueUI.DialogueText.text += conversation.lines[index].text[charIndex];
            charIndex++;

            yield return new WaitForSecondsRealtime(speed);
        }

        dialogueUI.StopSound();
        dialogueUI.ToggleArrow(true);

        yield return new WaitForSecondsRealtime(0.5f);

        isWriting = false;
        waitForNext = true;
    }

    private void EndDialogue()
    {
        started = false;
        waitForNext = false;
        StopAllCoroutines();
        dialogueUI.TogglePanel(false);
        dialogueUI.InitializeText();
        StartCoroutine(UnlockAndContinue());
    }

    
    private IEnumerator UnlockAndContinue()
    {
        yield return new WaitForSecondsRealtime(durationUnlock);
        EventManager.Instance.UpdateEventDataTrigger(TriggerEventID, true);

        if(playerEnable)
            UIManager.Instance.SetActiveUI(true);

        Time.timeScale = 1;  
    }
}


public enum DialogueOption
{
    TextOnly,
    FullDisplay
}

[System.Serializable]
public class ItemActive
{
    public GameObject gameObjectActive;
    public int targetIndex;
}
