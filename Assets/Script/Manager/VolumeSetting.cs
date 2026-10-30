using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        LoadVolume();
    }
    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        audioMixer.SetFloat("master", Mathf.Log10(volume)*20);
        SaveVolume();
    }
    public void SetBGMVolume()
    {
        float volume = bgmSlider.value;
        audioMixer.SetFloat("bgm", Mathf.Log10(volume) * 20);
        SaveVolume();
    }
    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        SaveVolume();
    }

    public void SaveVolume()
    {
        SaveManager.instance.SaveVolume(masterSlider.value, bgmSlider.value, sfxSlider.value);
    }
    public void LoadVolume()
    {
        SaveData data = SaveManager.instance.Load();
        masterSlider.value = data.masterVolume;
        bgmSlider.value = data.bgmVolume;
        sfxSlider.value = data.sfxVolume;
    }
}
