using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NoseHold3
public class Puzzle1 : LevelPuzzle
{
    public override void Setup()
    {
        base.Setup();

        TVSignalTuner.instance.SetupCleanScreen();

        TVScreenPlayer.instance.PlayVideo(this._videoFileName);

        MusicManager.instance.PlaySong("GameLoop");
    }

    public override void UpdatePuzzleState(InteractableObject interactedObject, float value)
    {
        if (value >= 3.0f)
        {
            interactedObject.ActivateLight();
            this.CompletePuzzle();
            this.PlayPuzzleCompleteSound();
        }
        else
        {
            this.ResetPuzzle();
        }
    }
}
