using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//FixHair
public class Puzzle4 : LevelPuzzle
{
    [SerializeField]
    private InteractableObject _targetObject;

    public override void Setup()
    {
        base.Setup();

        TVSignalTuner.instance.SetupCleanScreen();

        TVScreenPlayer.instance.PlayVideo(this._videoFileName);
    }

    public override void UpdatePuzzleState(InteractableObject interactedObject, float value)
    {
        if (interactedObject == this._targetObject)
        {
            this.CompletePuzzle();
        }        
    }
}
