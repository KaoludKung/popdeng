using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableObject2D : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ClickType clickType;
    [SerializeField] private int targetEvent;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Clicked {gameObject.name}");
        PerformAction();
    }

    void PerformAction()
    {
        switch (clickType)
        {
            case ClickType.unlockEvent:
                EventManager.Instance.UpdateEventDataTrigger(targetEvent, true);
                break;
        }
    }
}

public enum ClickType
{
    unlockEvent
}
