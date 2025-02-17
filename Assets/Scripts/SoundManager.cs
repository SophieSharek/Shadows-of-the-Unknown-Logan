﻿using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField]
    private SoundLibrary sfxLibrary;
    [SerializeField]
    private AudioSource sfx2DSource, sfx2dSource2;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
    }

    public void SetVolume(float volume)
    {
        // Clamp the volume between 0 and 1
        volume = Mathf.Clamp01(volume);
        // Set the volume of the audio source
        sfx2DSource.volume = volume;
    }

    public void PlaySound3D(string soundName, Vector3 pos)
    {
        PlaySound3D(sfxLibrary.GetClipFromName(soundName), pos);
    }

    public void PlaySound3D(AudioClip clip, Vector3 pos)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, pos);
        }
    }

    public void PlaySound2D(string soundName)
    {
        AudioClip clip = sfxLibrary.GetClipFromName(soundName);
        clip.LoadAudioData();
        sfx2DSource.PlayOneShot(clip);
    }

    public void PlayLoopingSound2D(string soundName)
    {
        AudioClip clip = sfxLibrary.GetClipFromName(soundName);
        sfx2DSource.clip = clip;
        sfx2DSource.loop = true;
        sfx2DSource.Play();
    }

    public void PreloadSound(string soundName)
    {
        AudioClip clip = sfxLibrary.GetClipFromName(soundName);
        clip.LoadAudioData();
    }

    public void TurnOffSound()
    {
        sfx2DSource.Stop();
    }

    public IEnumerator FadeOut(string soundName, bool isfadeout)
    {
        float timeToFade = 1.50f;
        float timeElapsed = 0;

        AudioClip clip = sfxLibrary.GetClipFromName(soundName);
        sfx2dSource2.clip = clip;
        sfx2dSource2.loop = true;
        
        if (!isfadeout)
        {
            sfx2dSource2.Play();
            while(timeElapsed < timeToFade)
            {
                
                sfx2dSource2.volume = Mathf.Lerp(0, 0.5f, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

        }
        else
        {
            while(timeElapsed < timeToFade)
            {
                sfx2dSource2.volume = Mathf.Lerp(0.5f, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                
                yield return null;
            }
            sfx2dSource2.Stop();
        }
    }
}