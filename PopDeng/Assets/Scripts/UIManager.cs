using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActiveUI(bool value)
    {
        for(int i = 0; i < uiObject.Length; i++)
        {
            if((i == 1 || i == 2) && !EventManager.Instance.IsEventTriggered(20))
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
