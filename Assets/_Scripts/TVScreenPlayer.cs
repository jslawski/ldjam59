using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TVScreenPlayer : MonoBehaviour
{
    public static TVScreenPlayer instance;

    [SerializeField]
    private VideoPlayer _videoPlayer;

    private AudioSource[] _audioSources;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        this._audioSources = GetComponents<AudioSource>();
    }

    private void Start()
    {
    }

    public void PlayVideo(string videoFileName, bool loop = true)
    {
        this._videoPlayer.Stop();

        string filenameWithExtension = videoFileName + ".webm";

        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, filenameWithExtension);
        this._videoPlayer.url = filePath;

        this._videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        this._videoPlayer.targetCameraAlpha = 1.0f;
        this._videoPlayer.isLooping = loop;
        this._videoPlayer.Play();
    }

    public void UpdateVolume()
    {
        this._audioSources[0].volume = (1.0f - TVSignalTuner.instance.currentStaticAmount) * FaceController.instance.masterVolume;
        this._audioSources[1].volume = TVSignalTuner.instance.currentStaticAmount * FaceController.instance.masterVolume;
    }

    public double GetVideoTime()
    {
        return this._videoPlayer.clockTime;
    }
}
