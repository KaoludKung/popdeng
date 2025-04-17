using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private GameObject backCamera;
    [SerializeField] private Button flashlightButton;
    [SerializeField] private Light2D flashLight;
    [SerializeField] private AudioClip flashLightClip;
    [SerializeField] private GameObject[] batteryLife;

    private float batteryStamina = 100f;
    private bool isOpen = false;
    private Coroutine batteryCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        flashlightButton.onClick.AddListener(ToggleFlashlight);
    }

    // Update is called once per frame
    void Update()
    {
        ShowBatteryLife();
    }

    void ToggleFlashlight()
    {
        if (backCamera != null && backCamera.activeSelf)
        {
            SoundFXManager.Instance.PlaySoundFXClip(flashLightClip, transform, false, 1.0f);

            if (batteryStamina > 0)
            {
                isOpen = !isOpen;
                flashLight.intensity = isOpen ? 1.0f : 0.0f;

                if (isOpen)
                {
                    if (batteryCoroutine == null)
                        batteryCoroutine = StartCoroutine(DecreaseBattery());
                }
                else
                {
                    if (batteryCoroutine != null)
                    {
                        StopCoroutine(batteryCoroutine);
                        batteryCoroutine = null;
                    }
                }
            }
            else
            {
                Debug.Log("Battery dead");
            }
        }
        else
        {
            Debug.Log("You can't use flashlight");
        }
    }

    void ShowBatteryLife()
    {
        if (batteryStamina > 75)
        {
            ActivateBatteryLife(4);
        }
        else if (batteryStamina > 50)
        {
            ActivateBatteryLife(3);
        }
        else if (batteryStamina > 25)
        {
            ActivateBatteryLife(2);
        }
        else if (batteryStamina > 0)
        {
            ActivateBatteryLife(1);
        }
        else
        {
            ActivateBatteryLife(0);
        }
    }

    void ActivateBatteryLife(int activeCount)
    {
        for (int i = 0; i < batteryLife.Length; i++)
        {
            batteryLife[i].SetActive(i < activeCount);
        }
    }


    IEnumerator DecreaseBattery()
    {
        while (batteryStamina > 0)
        {
            batteryStamina = Mathf.Max(batteryStamina - 0.5f, 0);
            Debug.Log("Battery Life: " + batteryStamina);
            yield return new WaitForSeconds(1.0f);
        }

        if (batteryStamina <= 0)
        {
            flashLight.intensity = 0.0f;
            isOpen = false;
            batteryCoroutine = null;
        }
    }
}
