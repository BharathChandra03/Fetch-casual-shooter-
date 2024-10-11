using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;

    public AudioSource sfxSource;

    public Dictionary<string, AudioClip> sfxDictionary;

    [System.Serializable]
    public struct NamedSfx
    {
        public string name;
        public AudioClip clip;

    }

    public NamedSfx[] sfxClips;

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

        sfxDictionary = new Dictionary<string, AudioClip>();

        foreach(var sfx in sfxClips)
        {
            if(!sfxDictionary.ContainsKey(sfx.name))
            {
                sfxDictionary.Add(sfx.name, sfx.clip);
            }
        }

    }

    public void PlaySFX(string name)
    {
        if(sfxDictionary.ContainsKey(name))
        {
            sfxSource.PlayOneShot(sfxDictionary[name]);
        }
        else
        {
            Debug.LogWarning("SFX withe name " + name + "not found");
        }
    }
}
