using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

//Head3 Chin5
public class Puzzle8 : LevelPuzzle
{
    [SerializeField]
    private InteractableObject[] _targetObjects;

    public override void Setup()
    {
        base.Setup();

        this._maxPoints = 2;
        this._currentPoints = 0;

        TVSignalTuner.instance.SetupCleanScreen();
        FaceController.instance.CloseMouth();

        TVScreenPlayer.instance.PlayVideo(this._videoFileName);
    }

    public override void UpdatePuzzleState(InteractableObject interactedObject, float value)
    {        
        if (this._currentPoints == 0)
        {
            if ((interactedObject == this._targetObjects[0]) && (value >= 2.5f))
            {
                interactedObject.ActivateLight();
                this._currentPoints++;
            }
            else
            {
                this.ResetPuzzle();
            }
        }
        else if (this._currentPoints == 1)
        {
            if ((interactedObject == this._targetObjects[1]) && (value >= 4.5f))
            {
                interactedObject.ActivateLight();
                this._currentPoints++;
            }
            else
            {
                this.ResetPuzzle();
            }
        }

        if (this._currentPoints >= this._maxPoints)
        {
            this.CompletePuzzle();
        }
    }
}
