using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private float duration = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActiveNow());
    }

    IEnumerator ActiveNow()
    {
        yield return new WaitForSeconds(duration);

        if (targetObject != null)
        {
            targetObject.SetActive(!targetObject.activeSelf);
        }
    }

}


