using UnityEngine;

public class MultiCameraAspectRatio : MonoBehaviour
{
    public Camera[] cameras; 
    public float targetAspectRatio = 16f / 9f;

    void Update()
    {
        SetCameraAspects();
    }

    void SetCameraAspects()
    {
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspectRatio;

        for (int i = 0; i < cameras.Length; i++)
        {
            if (!cameras[i].gameObject.activeInHierarchy)
                continue;

            Rect rect;

            if (scaleHeight < 1.0f)
            {
                // Letterboxing
                rect = new Rect(0, (1.0f - scaleHeight) / 2.0f, 1.0f, scaleHeight);
            }
            else
            {
                // Pillarboxing
                float scaleWidth = 1.0f / scaleHeight;
                rect = new Rect((1.0f - scaleWidth) / 2.0f, 0, scaleWidth, 1.0f);
            }

            cameras[i].rect = rect;

            // ???? Clear Flags ??? Background
            cameras[i].clearFlags = CameraClearFlags.SolidColor;
            cameras[i].backgroundColor = Color.black;
        }
    }
}

