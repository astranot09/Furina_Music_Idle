using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }


    [Header("Source")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("BGM")]
    public List<AudioClip> bgm;
    private int currentBGM = -1;
    private Coroutine bgmRoutine;

    [Header("SFX")]
    public AudioClip normalSFX;
    public AudioClip goldenSFX;

    private void Start()
    {
        StartCoroutine(PlayListBGM());
    }
    public void PlayBGM(AudioClip clip)
    {
        if (clip == null || bgmSource == null) return;

        bgmSource.Stop();
        bgmSource.clip = clip;
        bgmSource.loop = false;
        bgmSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxSource == null) return;

        sfxSource.PlayOneShot(clip);

    }

    public IEnumerator PlayListBGM()
    {
        while (true)
        {
            int index = GetNextBGMIndex();
            currentBGM = index;

            AudioClip currentClip = bgm[currentBGM];
            PlayBGM(currentClip);
            yield return new WaitForSeconds(currentClip.length);
        }
    }

    private int GetNextBGMIndex()
    {
        int index;
        do
        {
            index = Random.Range(0, bgm.Count);
        }
        while (index == currentBGM);

        return index;
    }
}
