using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioClip backgroundMusicClip;
    public AudioClip buttonClickClip;
    public AudioClip bounceClip;
    public AudioClip turboClip;
    public AudioClip halfLineClip;
    private AudioSource musicSource;
    private AudioSource sfxSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Initialize AudioSources
            musicSource = gameObject.AddComponent<AudioSource>();
            sfxSource = gameObject.AddComponent<AudioSource>();

            // Setup AudioSource properties
            musicSource.clip = backgroundMusicClip;
            musicSource.loop = true;
            musicSource.playOnAwake = false;

            // Play background music
            PlayBackgroundMusic();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBackgroundMusic()
    {
        if (!musicSource.isPlaying)
            musicSource.Play();
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    // Example methods to be called from other scripts
    public void PlayButtonClick()
    {
        PlaySoundEffect(buttonClickClip);
    }

    public void PlayBounceSound()
    {
        PlaySoundEffect(bounceClip);
    }

    public void PlayTurboItemSound()
    {
        PlaySoundEffect(turboClip);
    }

    public void HalfLineItemSound()
    {
        PlaySoundEffect(halfLineClip);
    }

    public void SetMusicEnabled(bool isEnabled)
    {
        musicSource.mute = !isEnabled;
    }

    public void SetSoundEffectsEnabled(bool isEnabled)
    {
        sfxSource.mute = !isEnabled;
    }
}