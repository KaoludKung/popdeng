using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundComputer : MonoBehaviour
{
    public static SoundComputer Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void PlaySoundComputer(AudioClip clip, float volume)
    {
        if (ClickMute.Instance.GetIsMute())
        {
            SoundFXManager.Instance.PlaySoundFXClip(clip, transform, false, 0.0f);
        }
        else
        {
            SoundFXManager.Instance.PlaySoundFXClip(clip, transform, false, volume);
        }
    }
}
