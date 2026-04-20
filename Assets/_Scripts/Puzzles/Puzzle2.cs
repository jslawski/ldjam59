using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Cutscene Puzzle, auto advance
public class Puzzle2 : LevelPuzzle
{
    [SerializeField]
    private float _cutsceneTimeInSeconds = 5.0f;    

    public override void Setup()
    {
        base.Setup();

        TVSignalTuner.instance.SetupCleanScreen();

        TVScreenPlayer.instance.PlayVideo(this._videoFileName, false);
        Invoke("CompletePuzzle", this._cutsceneTimeInSeconds);   
    }

    public override void CompletePuzzle()
    {
        PuzzleManager.instance.LoadNextPuzzle();
    }   
}
