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
        for (int i = 0; i < uiObject.Length; i++)
        {
            // Event 5th power cut unlocks the flashlight system
            if ((i == 1 || i == 2) && !EventManager.Instance.IsEventTriggered(5))
            {
                Debug.Log("Flashlight isn't enabled now");
                continue; // Skip setting active for flashlight-related UI if the event is not triggered
            }

            // Special condition for i == 1 and value == true
            if (i == 1 && value)
            {
                uiObject[i].SetActive(false); // Override to always set false for i == 1
            }
            else
            {
                uiObject[i].SetActive(value); // Default behavior
            }
        }
    }

}
