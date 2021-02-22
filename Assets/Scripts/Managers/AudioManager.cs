using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{   
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;

    /*void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("AudioManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }*/

    void Start()
    {
        soundSlider.value = GameSettings.soundVolume;
        musicSlider.value = GameSettings.musicVolume;
    }

    public void SetSoundVolume(float volume)
    {
        GameSettings.soundVolume = volume;

        AudioSource[] sounds = FindObjectsOfType<AudioSource>();
        foreach(AudioSource sound in sounds)
        {
            sound.volume = volume;
        }

        GetComponent<AudioSource>().volume = GameSettings.musicVolume;
    }

    public void SetMusicVolume(float volume)
    {
        GameSettings.musicVolume = volume;
        GetComponent<AudioSource>().volume = volume;
    }

    public void Mute()
    {
        soundSlider.value = 0;
        musicSlider.value = 0;

        GameSettings.soundVolume = 0;
        GameSettings.musicVolume = 0;

        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach(AudioSource audio in audioSources)
        {
            audio.volume = 0;
        }
    }
}
