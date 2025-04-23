using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GitchRandom : MonoBehaviour
{
    [SerializeField] private GameObject GltichImage;
    [SerializeField] private GameObject[] moodengSprite;
    [SerializeField] private AudioClip noiseClip;

    private TitleManager titleManager;
    private AudioSource noiseSource;

    // Start is called before the first frame update
    void Start()
    {
        titleManager = FindObjectOfType<TitleManager>();
        noiseSource = GetComponent<AudioSource>();
        StartCoroutine(ActiveGlitchEffects());

        noiseSource.clip = noiseClip;
    }

    // Update is called once per frame
    void Update()
    {
        if (titleManager.IsClicked())
        {
            StopAllCoroutines();
        }
    }

    IEnumerator ActiveGlitchEffects()
    {
        while (true)
        {
            int random = Random.Range(8, 20);
            yield return new WaitForSeconds(random);

            GltichImage.SetActive(true);
            noiseSource.Play();
            moodengSprite[0].SetActive(false);
            moodengSprite[1].SetActive(true);
            

            yield return new WaitForSeconds(2.5f);
            GltichImage.SetActive(false);
            noiseSource.Stop();
            moodengSprite[1].SetActive(false);
            moodengSprite[0].SetActive(true);

        }
    }
}
