using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    private int _bgmChannelId = -1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySong(string songName)
    {
        if (this._bgmChannelId > 0)
        {
            AudioManager.instance.Stop(this._bgmChannelId);
        }

        AudioChannelSettings audioChannelSettings = new AudioChannelSettings(true, 1.0f, 1.0f, 0.3f, "BGM");
        AudioClip audioClip = Resources.Load<AudioClip>("Audio/" + songName);

        this._bgmChannelId = AudioManager.instance.Play(audioClip, audioChannelSettings);
    }

    public void StopSong()
    {
        if (this._bgmChannelId > 0)
        {
            AudioManager.instance.Stop(this._bgmChannelId);
        }
    }
}
