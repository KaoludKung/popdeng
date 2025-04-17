using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Program : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject programObject;
    [SerializeField] string titlePrompt;
    [SerializeField] string descriptionPrompt;
    [SerializeField] int promptID;
    [SerializeField] AudioClip clickClip;
    [SerializeField] bool activePrompt = true;

    private bool isClicked = false;
    private ProgramActive programActive;

    private void Start()
    {
        programActive = FindAnyObjectByType<ProgramActive>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log($"Clicked {gameObject.name}");
        if (!isClicked)
            StartCoroutine(OpenProgram());
    }

    IEnumerator OpenProgram()
    {
        isClicked = true;

        SoundComputer.Instance.PlaySoundComputer(clickClip, 0.8f);

        yield return new WaitForSeconds(clickClip.length + 0.2f);

        if (activePrompt)
        {
            PopupManager.Instance.CreateManualPopup(titlePrompt, descriptionPrompt, promptID);
        }
        else
        {
            if(programObject != null)
                programObject.SetActive(true);

            if (programActive != null)
                programActive.ActiveProgram();
        }

        yield return new WaitForSeconds(0.5f);

        isClicked = false;
    }
}
