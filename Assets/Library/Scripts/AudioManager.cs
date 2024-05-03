using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;

    public AudioSource audioSource;

    public AudioClip collectableAudio;
    public AudioClip playerAudio;
    public AudioClip animalFoodAudio;

    public Slider volumeSlider;

    [SerializeField] private float defaultVolume;
    [SerializeField] private float volume;

    private void Awake()
    {
        if (audioManager == null)
        {
            audioManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //var initialVolume = PlayerPrefs.GetFloat("Volume", defaultVolume);
    }

    private void Start()
    {
        audioSource.volume = PlayerPrefs.GetFloat("Volume", volume);
        volumeSlider.value = audioSource.volume;
        volumeSlider.onValueChanged.AddListener(UpdateVolume);
    }

    public void CollectableAudio()
    {
        audioSource.PlayOneShot(collectableAudio);
    }

    public void PlayerAudio()
    {
        audioSource.PlayOneShot(playerAudio);
    }    

    public void AnimalFoodAudio()
    {
        audioSource.PlayOneShot(animalFoodAudio);
    }


    public void UpdateVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void ResetVolume()
    {
        volumeSlider.value = audioSource.volume;
    }
}
