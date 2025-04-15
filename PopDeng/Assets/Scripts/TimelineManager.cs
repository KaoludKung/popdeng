using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : EventObject
{
    [SerializeField] private PlayableDirector timeline;
    //[SerializeField] private bool playerEnable = false;

    void Start()
    {
        timeline.stopped += OnTimelineStopped;
        StartTimeline();
    }

    void StartTimeline()
    {
        timeline.Play();
    }

    void OnTimelineStopped(PlayableDirector director)
    {
        EventManager.Instance.UpdateEventDataTrigger(TriggerEventID, true);

        /*
        if (this != null && gameObject.activeInHierarchy)
        {
            StartCoroutine(UnlockPlayer());
        }
        */
    }

    /*
    private IEnumerator UnlockPlayer()
    {
        if (playerEnable)
        {
            yield return new WaitForSeconds(1.5f);
        }
    }
    */
}
