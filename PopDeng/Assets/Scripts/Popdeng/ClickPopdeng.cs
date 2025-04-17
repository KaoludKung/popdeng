using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickPopdeng : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject mooDeng;
    [SerializeField] private Sprite[] moodengSprite;
    [SerializeField] private AudioClip[] popClick;

    private PopdengManager popdengManager;
    private AudioSource popdengSource;
    private bool isClicked = false;

    // Start is called before the first frame update
    void Start()
    {
        popdengSource = GetComponent<AudioSource>();
        popdengManager = FindAnyObjectByType<PopdengManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!isClicked)
            StartCoroutine(PopClick());
    }

    IEnumerator PopClick()
    {
        isClicked = true;
        popdengManager.AddScore();
        
        if(popdengManager.GetScore() > 0)
        {
            popdengManager.ShowScoreText();

            mooDeng.GetComponent<SpriteRenderer>().sprite = moodengSprite[1];

            if (popdengSource.isPlaying)
                popdengSource.Stop();

            int random = Random.Range(0, popClick.Length);
            popdengSource.clip = popClick[random];

            popdengSource.volume = ClickMute.Instance.GetIsMute()? 0.0f : 0.8f;
            yield return null;
            popdengSource.Play();

            yield return new WaitForSecondsRealtime(0.5f);
            mooDeng.GetComponent<SpriteRenderer>().sprite = moodengSprite[0];
            isClicked = false;
        }
        else
        {
            yield return new WaitForSecondsRealtime(1.0f);
            isClicked = false;
        }
    }
}
