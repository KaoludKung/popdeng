using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequirementEvent : MonoBehaviour
{
    [SerializeField] int[] scoreRequirement;
    [SerializeField] int[] eventID;
    private PopdengManager popdengManager;

    void Start()
    {
        popdengManager = FindAnyObjectByType<PopdengManager>();
        StartCoroutine(CheckRequirementAndUnlock());
    }

    IEnumerator CheckRequirementAndUnlock()
    {
        while (true)
        {
            for (int i = 0; i < scoreRequirement.Length; i++)
            {
                if (popdengManager.GetScore() >= scoreRequirement[i] && !EventManager.Instance.IsEventTriggered(eventID[i]))
                {
                    EventManager.Instance.UpdateEventDataTrigger(eventID[i], true);
                }
            }

            yield return new WaitForSeconds(1.2f);
        }
    }
}
