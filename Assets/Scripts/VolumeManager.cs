using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private AudioMixer Audiomixer;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSlider;

    private void Awake() {
        if(PlayerPrefs.HasKey("MusicVolume")) {
            SetSliders();
            SetAudioMixer();
        }
    }

    private void SetSliders() {
        MusicSlider.value = PlayerPrefs.GetFloat("MusicSlider");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXSlider");
    }

    private void SetAudioMixer() {
        Audiomixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
        Audiomixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
    }

    public void UpdateMusicVolume() {
        Audiomixer.SetFloat("MusicVolume", MusicSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", MusicSlider.value);
        Debug.Log(MusicSlider.value);
    }

    public void UpdateSFXVolume() {
        Audiomixer.SetFloat("SFXVolume", SFXSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", SFXSlider.value);
        Debug.Log(SFXSlider.value);
    }

}
