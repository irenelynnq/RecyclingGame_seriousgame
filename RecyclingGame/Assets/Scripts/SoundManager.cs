using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    public AudioSource bgmSource;
    public AudioSource fxSource;
    public AudioSource runFxSource;
    public AudioSource wrongFxSource;

    public AudioClip opening_bgm;
    public AudioClip run_bgm;
    public AudioClip treat_bgm;
    public AudioClip happyEnding_bgm;
    public AudioClip badEnding_bgm;

    public AudioClip cursor_fx;
    public AudioClip get_fx;
    public AudioClip wrong_fx;
    public AudioClip jump_fx;
    public AudioClip separate_fx;
    public AudioClip slide_fx;
    public AudioClip start_fx;
    public AudioClip wash_fx;
    public AudioClip wrap_fx;
    public AudioClip clear_fx;
    public AudioClip fail_fx;
    public AudioClip next_fx;
    public AudioClip youKnowIt_fx;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AudioStop(AudioSource audioSource)
    {
        audioSource.Stop();
    }

    public void AudioStart(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void AudioVolume(AudioSource audioSource, float volume)
    {
        audioSource.volume = volume;
    }

    public void FxSound(AudioClip audioClip)
    {
        fxSource.clip = audioClip;
        fxSource.Play();
    }

    public void RunFxSound(AudioClip audioClip)
    {
        runFxSource.clip = audioClip;
        runFxSource.Play();
    }

    public void RunFxStop()
    {
        runFxSource.Stop();
    }

    public void SlideFxStop()
    {
        if(runFxSource.clip == slide_fx)
        {
            RunFxStop();
        }
    }

    public void WrongFx()
    {
        //wrongFxSource.clip = wrong_fx;
        wrongFxSource.Play();
    }
}
