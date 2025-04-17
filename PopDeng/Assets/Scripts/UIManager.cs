using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    //0: change camera buttons 1: flashlight button 2: flashlight's life
    [SerializeField] private GameObject[] uiObject;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SetActiveUI(bool value)
    {
        for(int i = 0; i < uiObject.Length; i++)
        {
            //Event 5th power cut is unlock flashlight system
            if((i == 1 || i == 2) && !EventManager.Instance.IsEventTriggered(5))
            {
                Debug.Log("Flashlight isn't enable now");
            }
            else
            {
                uiObject[i].SetActive(value);
            }
        }   
    }
}
