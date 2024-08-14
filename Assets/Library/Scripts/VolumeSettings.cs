using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public Slider volumeSlider;


    public void Start()
    {
        if(PlayerPrefs.HasKey("sfxVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetVolume();
        }
    }

    public void SetVolume()
    {
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("sfxVolume", AudioListener.volume);
    }
    public void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        SetVolume();
    }
}
