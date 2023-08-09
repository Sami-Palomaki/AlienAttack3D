using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound [] sounds, musicSound;
    public AudioSource musicSource, sfxSource;
    AudioSource aS;
    private int currentMusicIndex = 0;

    void Awake()
    {
        aS = gameObject.AddComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            Destroy(this.gameObject);
        }       
    }

    private void Start() 
    {
        AudioManager.instance.PlayMusic(0);
    }

    public void PlayMusic(int index)
    {

        if (index < 0 || index >= musicSound.Length)
        {
            Debug.LogError("Invalid music index: " + index);
            return;
        }

        Sound s = musicSound[index];
        AudioClip[] clips = s.clip;

        AudioClip clip = clips[0];

        musicSource.clip = clip;
        musicSource.Play();

        Invoke("PlayNextMusicTrack", musicSource.clip.length);
    }

    private void PlayNextMusicTrack()
    {
        currentMusicIndex++;
    if (currentMusicIndex >= musicSound.Length)
    {
        currentMusicIndex = 0;
    }

        AudioManager.instance.PlayMusic(currentMusicIndex);
        
        // Get the index of the current music track
        int currentTrackIndex = Array.IndexOf(musicSound, musicSource.clip);

        if (currentTrackIndex < 0 || currentTrackIndex == musicSound.Length - 1)
        {
            return;
        }

        // Get the next music track to play
        int nextTrackIndex = currentTrackIndex + 1;
        PlayMusic(nextTrackIndex);
    }

    public void Play (string name)
        {
           Sound s = Array.Find(sounds, sound => sound.name == name);
           if (s != null)
            {
                aS.PlayOneShot(s.clip[UnityEngine.Random.Range(0, s.clip.Length)], s.volume);
            }
        }

    public void Play (string name, GameObject targetAs)
        {
            if (targetAs.GetComponent<AudioSource>() == null)
            {
                targetAs.AddComponent<AudioSource>();
            }
           Sound s = Array.Find(sounds, sound => sound.name == name);
           if (s != null)
            {
                targetAs.GetComponent<AudioSource>().PlayOneShot(s.clip[UnityEngine.Random.Range(0, s.clip.Length)], s.volume);
            }
        }




    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
