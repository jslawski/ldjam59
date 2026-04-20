using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Cutscene Puzzle, auto advance
public class PuzzleSplash : LevelPuzzle
{
    [SerializeField]
    private float _cutsceneTimeInSeconds = 5.0f;
    [SerializeField]
    private CameraRotator _cameraRotator;

    private Vector3 _startPosition =  new Vector3(0.0f, 0.0f, -1.288f);

    [SerializeField]
    private Vector3 _targetPosition;

    public override void Setup()
    {
        base.Setup();

        PlayerControlsManager.instance.playerCamera.transform.localPosition = this._startPosition;

        TVSignalTuner.instance.SetupCleanScreen();

        TVScreenPlayer.instance.PlayVideo(this._videoFileName, false);

        this._cameraRotator.LockRotation();

        Invoke("ZoomOut", 3.0f);
        Invoke("CompletePuzzle", this._cutsceneTimeInSeconds);
    }

    private void ZoomOut()
    {
        this._cameraRotator.ZoomToPosition(this._targetPosition);
    }

    public override void CompletePuzzle()
    {
        this._cameraRotator.UnlockRotation();        
        PuzzleManager.instance.LoadNextPuzzle();
    }
}
