using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] AudioMixer audioMixer;

    [Header("Volume Arrow Settings")]
    [SerializeField] private GameObject volumeArrow; 
    [SerializeField] private float minX = -0.46f;
    [SerializeField] private float maxX = 0.72f;

    private bool isDragging = false;
    private BoxCollider2D volumeArrowCollider;

    void Start()
    {
        volumeArrowCollider = volumeArrow.GetComponent<BoxCollider2D>();
        InitializeMasterVolume();
    }


    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (volumeArrowCollider != null && volumeArrowCollider.OverlapPoint(mousePosition))
            {
                isDragging = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            float clampedX = Mathf.Clamp(mousePosition.x, minX, maxX);
            volumeArrow.transform.position = new Vector3(clampedX, volumeArrow.transform.position.y, volumeArrow.transform.position.z); // ??? transform.position

            float normalizedVolume = Mathf.InverseLerp(minX, maxX, clampedX);
            SetMasterVolume(normalizedVolume);
        }
    }

    public void SetMasterVolume(float level)
    {
        if (level <= 0)
        {
            audioMixer.SetFloat("MasterVolume", -80.0f);
        }
        else
        {
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(level) * 20);
        }

        PlayerPrefs.SetFloat("masterVolume", level);
    }

    public void InitializeMasterVolume()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            float masterVolume = PlayerPrefs.GetFloat("masterVolume");
            SetMasterVolume(masterVolume);

            float xPos = Mathf.Lerp(minX, maxX, masterVolume);
            volumeArrow.transform.position = new Vector3(xPos, volumeArrow.transform.position.y, volumeArrow.transform.position.z);
        }
        else
        {
            SetMasterVolume(1.0f);
        }
    }
}
