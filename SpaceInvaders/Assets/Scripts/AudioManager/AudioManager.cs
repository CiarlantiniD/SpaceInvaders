using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hellmade.Sound;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instanse;

    public static AudioManager Instance
    {
        get
        {
            if (instanse == null)
                instanse = FindObjectOfType<AudioManager>();

            return instanse;
        }
    }

    [SerializeField] private AudioClip musicMenu;
    [Space(20)]
    [SerializeField] private AudioClip soundShootAlien;
    [SerializeField] private AudioClip soundExplotionAlien;
    [Space(10)]
    [SerializeField] private AudioClip soundShootPlayer;
    [SerializeField] private AudioClip soundExplotionPlayer;
    [Space(20)]
    [SerializeField] private AudioClip fxMenuEnter;
    [SerializeField] private AudioClip fxMenuBack;
    [SerializeField] private AudioClip fxMenuStarGame;


    public enum Music {Menu = 0 }
    public enum Sounds { ShootAlien = 0, ExplotionAlien, ShootPlayer , ExplotionPlayer }
    public enum FXSounds { MenuEnter = 0, MenuBack, MenuStarGame }

    Dictionary<Music, AudioClip> musicDictionary;
    Dictionary<Sounds, AudioClip> soundsDictionary;
    Dictionary<FXSounds, AudioClip> fxDictionary;

    private void Start()
    {

        if (instanse == null)
        {
            instanse = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
            return;
        }

        musicDictionary = new Dictionary<Music, AudioClip>();
        soundsDictionary = new Dictionary<Sounds, AudioClip>();
        fxDictionary = new Dictionary<FXSounds, AudioClip>();

        musicDictionary.Add(Music.Menu, musicMenu);
        
        soundsDictionary.Add(Sounds.ExplotionAlien, soundExplotionAlien);
        soundsDictionary.Add(Sounds.ExplotionPlayer, soundExplotionPlayer);
        soundsDictionary.Add(Sounds.ShootAlien, soundShootAlien);
        soundsDictionary.Add(Sounds.ShootPlayer, soundShootPlayer);

        fxDictionary.Add(FXSounds.MenuBack, fxMenuBack);
        fxDictionary.Add(FXSounds.MenuEnter, fxMenuEnter);
        fxDictionary.Add(FXSounds.MenuStarGame, fxMenuStarGame);
    }


    public void PlayMusic(Music music)
    {
        if(musicDictionary.TryGetValue(music,out AudioClip audioClip))
            EazySoundManager.GetMusicAudio(EazySoundManager.PrepareMusic(audioClip)).Play();
    }


    public void PlaySound(Sounds sound)
    {
        if (soundsDictionary.TryGetValue(sound, out AudioClip audioClip))
            EazySoundManager.GetSoundAudio(EazySoundManager.PrepareSound(audioClip)).Play();
    }


    public void PlayFXSound(FXSounds fXSound)
    {
        if (fxDictionary.TryGetValue(fXSound, out AudioClip audioClip))
            EazySoundManager.GetUISoundAudio(EazySoundManager.PrepareUISound(audioClip)).Play();
    }

}
