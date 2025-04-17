using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramActive : MonoBehaviour
{
    [SerializeField] private GameObject[] programList;
    [SerializeField] private GameObject[] programTab;

    public void ActiveProgram()
    {
        for (int i = 0; i < programList.Length; i++)
        {
            programTab[i].SetActive(programList[i].activeSelf);
        }
    }
}
