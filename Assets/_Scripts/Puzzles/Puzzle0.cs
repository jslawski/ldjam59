using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Title puzzle.  Player just interacts with anything and completes it.
public class Puzzle0 : LevelPuzzle
{
    public override void Setup()
    {
        base.Setup();

        TVSignalTuner.instance.SetupCleanScreen();

        TVScreenPlayer.instance.PlayVideo(this._videoFileName);
    }

    public override void UpdatePuzzleState(InteractableObject interactedObject, float value)
    {
        this.CompletePuzzle();
    }
}
