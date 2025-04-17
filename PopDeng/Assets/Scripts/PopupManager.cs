using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance;
    [SerializeField] private GameObject popupPrefab;
    [SerializeField] private SpriteRenderer screenRenderer;
    [SerializeField] private AudioClip popupClip;

    [SerializeField] private string[] randomTitles;
    [SerializeField] private string[] randomDetails;
    //0: warning 1: error
    [SerializeField] private Sprite[] randomIcons;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void CreateRandomPopup()
    {
        if (screenRenderer == null || popupPrefab == null)
        {
            Debug.LogError("screenRenderer or popupPrefab are not found!");
            return;
        }

        Bounds bounds = screenRenderer.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        Vector3 randomPosition = new Vector3(randomX, randomY, 0);

        GameObject newpopUp = Instantiate(popupPrefab, randomPosition, Quaternion.identity);
        SoundComputer.Instance.PlaySoundComputer(popupClip, 0.5f);

        TextMeshProUGUI titleText = newpopUp.transform.Find("popup_title").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI detailText = newpopUp.transform.Find("popup_detail").GetComponent<TextMeshProUGUI>();
        Image iconImage = newpopUp.transform.Find("popup_icon").GetComponent<Image>();

        string randomTitle = randomTitles[Random.Range(0, randomTitles.Length)];
        string randomDetail = randomDetails[Random.Range(0, randomDetails.Length)];
        Sprite randomIcon = randomIcons[Random.Range(0, randomIcons.Length)];

        titleText.text = randomTitle;
        detailText.text = randomDetail.Replace("\\n", "\n"); ;
        iconImage.sprite = randomIcon;
    }

    public void CreateManualPopup(string title, string detail, int iconID)
    {
        if (screenRenderer == null || popupPrefab == null)
        {
            Debug.LogError("screenRenderer or popupPrefab are not found!");
            return;
        }

        Bounds bounds = screenRenderer.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        Vector3 randomPosition = new Vector3(randomX, randomY, 0);

        GameObject newpopUp = Instantiate(popupPrefab, randomPosition, Quaternion.identity);

        if (ClickMute.Instance.GetIsMute())
        {
            SoundFXManager.Instance.PlaySoundFXClip(popupClip, transform, false, 0.0f);
        }
        else
        {
            SoundFXManager.Instance.PlaySoundFXClip(popupClip, transform, false, 0.3f);
        }

        TextMeshProUGUI titleText = newpopUp.transform.Find("popup_title").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI detailText = newpopUp.transform.Find("popup_detail").GetComponent<TextMeshProUGUI>();
        Image iconImage = newpopUp.transform.Find("popup_icon").GetComponent<Image>();

        titleText.text = title;
        detailText.text = detail.Replace("\\n", "\n"); ;
        iconImage.sprite = randomIcons[iconID];
    }

}
