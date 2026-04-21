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
    private GameObject _blackScreen;

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
        this._blackScreen.SetActive(true);
        StartCoroutine(this.EndCoroutine());

        MusicManager.instance.StopSong();

        AudioChannelSettings audioChannelSettings = new AudioChannelSettings(false, 1.0f, 1.0f, 0.3f, "SFX");
        AudioClip audioClip = Resources.Load<AudioClip>("Audio/LightbulbPop");

        AudioManager.instance.Play(audioClip, audioChannelSettings);
    }

    private IEnumerator EndCoroutine()
    {
        yield return new WaitForSeconds(3.0f);

        this._cutscenePanel.SetActive(true);
        this.PlayVideo();
    }

    private void PlayVideo()
    {
        this._cutscenePlayer.Stop();

        string filenameWithExtension = this._cutsceneName + ".mov";

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
