using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Mouth3
public class Puzzle3 : LevelPuzzle
{
    [SerializeField]
    private InteractableObject _targetObject;

    public override void Setup()
    {
        base.Setup();

        this._maxPoints = 3;
        this._currentPoints = 0;

        TVSignalTuner.instance.SetupRandomTuning();

        TVScreenPlayer.instance.PlayVideo(this._videoFileName);
    }

    public override void UpdatePuzzleState(InteractableObject interactedObject, float value)
    {
        if (interactedObject == this._targetObject && value <= 0.5f)
        {            
            this._currentPoints++;
            this.PlayPuzzleProgressSound();
        }
        else
        {
            this.ResetPuzzle();
        }

        if (this._currentPoints >= this._maxPoints)
        {
            interactedObject.ActivateLight();
            this.CompletePuzzle();
            this.PlayPuzzleCompleteSound();
        }
    }
}
