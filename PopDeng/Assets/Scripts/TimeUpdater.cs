using System.Collections;
using UnityEngine;
using TMPro;

public class TimeUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText; // Drag your TextMeshPro UI component here in the Inspector
    private int hour = 12; // Start time: 12:00
    private int minute = 0;
    private bool isAM = true; // True for AM, False for PM

    private void Start()
    {
        UpdateTimeText(); // Initialize the time display
        StartCoroutine(UpdateTime());
    }

    private IEnumerator UpdateTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f); // Change to 60f for real-time minutes
            IncrementTime();
            UpdateTimeText();
        }
    }

    private void IncrementTime()
    {
        minute++;
        if (minute >= 60)
        {
            minute = 0;
            hour++;
        }

        if (hour > 12)
        {
            hour = 1;
        }

        if (hour == 12 && minute == 0)
        {
            isAM = !isAM; // Toggle AM/PM when crossing 12:00
        }
    }

    private void UpdateTimeText()
    {
        string amPm = isAM ? "AM" : "PM";
        string formattedTime = $"{hour:D2}:{minute:D2} {amPm}";
        timeText.text = formattedTime;
    }
}

