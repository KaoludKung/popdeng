using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : EventObject
{
    [SerializeField] private PlayableDirector timeline;
    [SerializeField] private bool playerEnable = true;

    private void Awake()
    {
        if (!playerEnable)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

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


    private IEnumerator UnlockPlayer()
    {
        if (playerEnable)
        {
            yield return new WaitForSeconds(0.5f);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

}
