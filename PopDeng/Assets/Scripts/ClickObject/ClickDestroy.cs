using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickDestroy : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private AudioClip clickClip;
    [SerializeField] private float times = 0.8f;
    [SerializeField] private bool isDestroy = true;
    

    private bool isClicked = false;
    private ProgramActive programActive;

    private void Start()
    {
        if(!isDestroy)
            programActive = FindAnyObjectByType<ProgramActive>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log($"Clicked {gameObject.name}");
        
        if(!isClicked)
            StartCoroutine(DestroyObject());
    }

    IEnumerator DestroyObject()
    {
        isClicked = true;

        if(clickClip != null)
        {
            SoundComputer.Instance.PlaySoundComputer(clickClip, 0.8f);
        }

        yield return new WaitForSeconds(times);

        if (targetObject != null)
        {
            if (isDestroy) {
                Destroy(targetObject);
            }
            else
            {
                isClicked = false;
                targetObject.SetActive(false);

                if (programActive != null)
                    programActive.ActiveProgram();
            }       
        }
    }
}
