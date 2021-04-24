using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private static AudioManager instance;

    public static AudioManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }


    [SerializeField]
    private AudioSource bgMusicSource;
    [SerializeField]
    private AudioSource sfxSource;


    [System.Serializable]
    public class SFXMapping
    {
        public AudioManager.SFX_Enums index;
        public AudioClip audioClip;
    }

    public SFXMapping[] sfxClips;


    [System.Serializable]
    public class MusicMapping
    {
        public AudioManager.BGMusic_Enums index;
        public AudioClip audioClip;
    }

    public MusicMapping[] musicClips;


    private bool bBgMusicOn, bSfxOn;

    [SerializeField]
    public enum SFX_Enums
    {
        Click = 0,
        Close,
        Pause,
        Resume,
        WindowOpen
    }

    [SerializeField]
    public enum BGMusic_Enums
    {
        Intro = 0,
        GameMusic,
        GameLost,
        GameWin
    }

    public void PlaySfx(SFX_Enums value)
    {
        foreach (SFXMapping pair in sfxClips)
        {
            if(pair.index == value)
                sfxSource.clip = pair.audioClip;
        }

        sfxSource.Play();
    }

    public void PlayMusic(BGMusic_Enums value, bool bLoop = true)
    {
        foreach (MusicMapping pair in musicClips)
        {
            if (pair.index == value)
                bgMusicSource.clip = pair.audioClip;
        }

        bgMusicSource.Play();
        bgMusicSource.loop = bLoop;
    }

    public void AllAudiosMute(bool bValue)
    {
        bgMusicSource.mute = bValue;
        sfxSource.mute = bValue;
    }

    public void AllAudiosPause(bool bValue)
    {
        if (bValue)
        {
            bgMusicSource.Pause();
            sfxSource.Pause();
        }
        else
        {
            bgMusicSource.UnPause();
            sfxSource.UnPause();
        }
    }

}
