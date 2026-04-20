using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EndGame : MonoBehaviour
{
    public static EndGame instance;

    [SerializeField]
    private GameObject _cutscenePanel;

    [SerializeField]
    private VideoPlayer _cutscenePlayer;

    [SerializeField]
    private string _cutsceneName;

    [SerializeField]
    private float _cutsceneLength;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ExecuteEndGame()
    {
        FaceController.instance.CloseEyes();
        this._cutscenePanel.SetActive(true);
        this.PlayVideo();
    }

    private void PlayVideo()
    {
        this._cutscenePlayer.Stop();

        string filenameWithExtension = this._cutsceneName + ".webm";

        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, filenameWithExtension);
        this._cutscenePlayer.url = filePath;

        this._cutscenePlayer.renderMode = VideoRenderMode.RenderTexture;
        this._cutscenePlayer.targetCameraAlpha = 1.0f;
        this._cutscenePlayer.Play();

        Invoke("ResetGame", this._cutsceneLength);
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
