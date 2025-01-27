using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private AudioMixer Audiomixer;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSlider;

    private float DefaultVolume = 0.75f;

    private void Awake() {
        SetSliders();
        SetAudioMixer();
    }

    private void SetSliders() {
        MusicSlider.value = PlayerPrefs.GetFloat("MusicSlider", DefaultVolume);
        SFXSlider.value = PlayerPrefs.GetFloat("SFXSlider", DefaultVolume);
    }

    private void SetAudioMixer() {
        Audiomixer.SetFloat("SFXVolume", Mathf.Log10(PlayerPrefs.GetFloat("SFXVolume", DefaultVolume)) * 20);
        Audiomixer.SetFloat("MusicVolume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume", DefaultVolume)) * 20);
    }

    public void UpdateMusicVolume() {
        Audiomixer.SetFloat("MusicVolume", Mathf.Log10(MusicSlider.value) * 20);
        PlayerPrefs.SetFloat("MusicVolume", MusicSlider.value);
    }

    public void UpdateSFXVolume() {
        Audiomixer.SetFloat("SFXVolume", Mathf.Log10(SFXSlider.value) * 20);
        PlayerPrefs.SetFloat("SFXVolume", SFXSlider.value);
    }

}
