using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGMName
{
    Game,
}

public enum SoundName
{
    Fix,
    Dig,
    Whistle,
}

public class MusicSystem : SingletonMono<MusicSystem>
{
    public AudioSource BGM = null;
    public AudioSource Sound = null;

    private void Awake()
    {
        if (BGM == null)
        {
            BGM = this.gameObject.AddComponent<AudioSource>();
        }
        if (Sound == null)
        {
            Sound = this.gameObject.AddComponent<AudioSource>();
        }
    }

    void Start()
    {

    }

    public void PlayBGM(BGMName bGMName, bool loop = true)
    {
        AudioClip bgm = Resources.Load<AudioClip>("Music/BGM/" + bGMName.ToString());
        if (bgm != null)
        {
            BGM.clip = bgm;
            BGM.loop = loop;
            BGM.Play();
        }
    }

    public void PlaySound(SoundName soundName)
    {
        AudioClip sound = Resources.Load<AudioClip>("Music/Sound/" + soundName.ToString());
        if (sound != null)
        {
            Sound.PlayOneShot(sound);
        }
    }
}