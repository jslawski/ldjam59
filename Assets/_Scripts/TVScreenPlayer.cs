using UnityEngine;
using UnityEngine.Video;

public class TVScreenPlayer : MonoBehaviour
{
    public static TVScreenPlayer instance;

    [SerializeField]
    private VideoPlayer _videoPlayer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        this.PlayVideo("TestFootage.mp4");
    }

    public void PlayVideo(string videoFileName)
    {
        this._videoPlayer.Stop();

        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
        this._videoPlayer.url = filePath;

        this._videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        this._videoPlayer.targetCameraAlpha = 1.0f;
        this._videoPlayer.Play();
    }
}
