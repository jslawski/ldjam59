using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle5 : LevelPuzzle
{
    [SerializeField]
    private float _cutsceneTimeInSeconds = 5.0f;

    public override void Setup()
    {
        base.Setup();

        TVSignalTuner.instance.SetupCleanScreen();

        TVScreenPlayer.instance.PlayVideo(this._videoFileName);
        Invoke("CompletePuzzle", this._cutsceneTimeInSeconds);
    }

    public override void CompletePuzzle()
    {
        PuzzleManager.instance.LoadNextPuzzle();
    }
}
