using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSwitcher : MonoBehaviour
{
    //0: front camera, 1: back camera
    [SerializeField] private Camera[] cameras;
    [SerializeField] private Button switchButton;
    private int currentCameraIndex;

    void Start()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }

        if (cameras.Length > 0)
        {
            currentCameraIndex = 0;
            cameras[currentCameraIndex].gameObject.SetActive(true);
        }

        switchButton.onClick.AddListener(() =>
        {
            int nextIndex = (currentCameraIndex == 0) ? 1 : 0;
            SwitchCamera(nextIndex);
        });
    }

    public void SwitchCamera(int index)
    {
        if (index < 0 || index >= cameras.Length)
        {
            Debug.LogWarning("Camera index out of range.");
            return;
        }

        cameras[currentCameraIndex].gameObject.SetActive(false);
        currentCameraIndex = index;
        cameras[currentCameraIndex].gameObject.SetActive(true);
    }
}
