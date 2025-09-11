using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{

    public Slider volumeSlider;

    public static VolumeSettings instance;

    public void Awake()
    {
        if(instance = this)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //prevents the volume settings from being destroyed when loading a new scene
        }
        else
        {
            Destroy(gameObject); //ensures only one instance of the volume settings exists
        }
        
    }

    public void Start()
    {
        volumeSlider.onValueChanged.AddListener(delegate { SetVolume(); });

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
        PlayerPrefs.Save();
    }

    public void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        SetVolume();
    }
}
