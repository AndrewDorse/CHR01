using System;
using System.Collections;
using System.Collections.Generic;
using Hellmade.Sound;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioClipGameStorage : MonoBehaviour
{
    public static AudioClipGameStorage instance;


    public static bool EnableSoundsFX = true;
    public static bool EnableBackgroundMusic = true;
    
    public static void SetEnableSoundsFX(bool enable)
    {
       // EnableSoundsFX = enable;
       //Manager.SaveSoundSettings();
    }

    //public static void SetEnableBackgroundMusic(bool enable)
    //{
    //    EnableBackgroundMusic = enable;
    //    Manager.SaveSoundSettings();

    //    if (!instance)
    //    {
    //        return;
    //    }

    //    if (instance._backgroundMusicStarted && !enable)
    //    {
    //        PauseAllBackgroundMusic();
    //    }
    //    else if (instance._backgroundMusicStarted)
    //    {
    //        ResumeAllBackgroundMusic();
    //    }
    //    else if (enable)
    //    {
    //        PlayRandomBackgroundMusic();
    //    }
    //}

    [SerializeField] private AudioClip[] backgroundMusic;
    [Space]
    [SerializeField] private AudioClip[] clicks;
    [SerializeField] private AudioClip[] ballsImprove; 

    private bool _backgroundMusicStarted;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //PlayRandomBackgroundMusic();


        EventsProvider.OnAnyButtonClick += PlayClick;

        if (SaveManager.SaveModel.Sound == false)
        {
            EazySoundManager.GlobalVolume = 0f;
        }
        else
        {
            EazySoundManager.GlobalVolume = 0.86f;
        }
    }

    private void PlayClick()
    {
        EazySoundManager.PlaySound(AudioClipGameStorage.RandomClick, volume: 0.6f);
    }

    private void OnDestroy()
    {
        StopALlBackgroundMusic();
    }

    public static AudioClip RandomBackgroundMusic => GetRandomFXFromArray(instance.backgroundMusic);
    public static AudioClip RandomClick => GetRandomFXFromArray(instance.clicks);
    public static AudioClip RandomImprove => GetRandomFXFromArray(instance.ballsImprove); 
    
    public static void PlayRandomBackgroundMusic() => PlayRandomBackgroundMusic(RandomBackgroundMusic);
    public static void PlayRandomFireballFX() => PlayRandomFXFromArray(RandomClick);
    public static void PlayRandomIceFX() => PlayRandomFXFromArray(RandomImprove); 

    private static AudioClip GetRandomFXFromArray(AudioClip[] arrayFX)
    {
        if (arrayFX != null && arrayFX.Length > 0)
        {
            return arrayFX[Random.Range(0, arrayFX.Length)];
        }

        return null;
    }
    
    private static void PlayRandomFXFromArray(AudioClip soundFX)
    {
        if (!EnableSoundsFX)
        {
            return;
        }
        
        EazySoundManager.PlaySound(soundFX);
    }

    private static void PlayRandomBackgroundMusic(AudioClip soundFX)
    {
        if (!EnableBackgroundMusic)
        {
            return;
        }
        
        EazySoundManager.PlayMusic(soundFX, 1f, true, true, 1f, 1f);
        instance._backgroundMusicStarted = true;
    }
    
    public static void ResumeAllBackgroundMusic()
    {
        EazySoundManager.ResumeAllMusic();
    }
    
    public static void PauseAllBackgroundMusic()
    {
        EazySoundManager.PauseAllMusic();
    }

    public static void StopALlBackgroundMusic()
    {
        EazySoundManager.StopAllMusic();
    }
    
     
}
