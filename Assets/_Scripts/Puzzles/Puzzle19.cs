using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle19 : LevelPuzzle
{
    [SerializeField]
    private float _timeBeforeEndCutscene;

    [SerializeField]
    private Transform _tvTransform;

    public override void Setup()
    {
        base.Setup();

        TVSignalTuner.instance.SetupCleanScreen();       

        StartCoroutine(this.SetupCoroutine());
    }

    private IEnumerator SetupCoroutine()
    {
        FaceController.instance.DeactivateAllLights();
        FaceController.instance.LightFaceObject("_End_GlowLight");

        TVScreenPlayer.instance.PlayVideo(this._videoFileName);

        yield return new WaitForSeconds(2.0f);

        MusicManager.instance.PlaySong("ClimaxMusic");

        this._tvTransform.DOShakeRotation(20.0f, 5.0f, 10, 60.0f, false, ShakeRandomnessMode.Full);

        yield return new WaitForSeconds(13.0f);

        EndGame.instance.ExecuteEndGame();
    }
}
