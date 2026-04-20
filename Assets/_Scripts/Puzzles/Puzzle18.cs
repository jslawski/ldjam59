using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Factory Reset
public class Puzzle18 : LevelPuzzle
{
    [SerializeField]
    private InteractableObject[] _targetObjects;

    public override void Setup()
    {
        base.Setup();

        TVSignalTuner.instance.SetupCleanScreen();

        TVScreenPlayer.instance.PlayVideo(this._videoFileName);

        this._maxPoints = 7;
        this._currentPoints = 0;

        StartCoroutine(this.ProcessPuzzle());
    }

    private IEnumerator ProcessPuzzle()
    {
        while (true)
        {
            if (FaceController.instance.AreEyesClosed() == false && FaceController.instance.IsMouthClosed() == false)
            {
                if (this._currentPoints >= this._maxPoints)
                {
                    this.CompletePuzzle();
                }
            }

            yield return null;
        }
    }

    public override void CompletePuzzle()
    {
        StopAllCoroutines();
        base.CompletePuzzle();
    }

    public override void UpdatePuzzleState(InteractableObject interactedObject, float value)
    {
        if (FaceController.instance.AreEyesClosed() == true && FaceController.instance.IsMouthClosed() == true)
        {
            if (this._currentPoints >= this._maxPoints)
            {
                return;
            }
        
            if (interactedObject == this._targetObjects[this._currentPoints])
            {
                if (this._currentPoints == 0)
                {
                    if (value > 5.0f)
                    {
                        interactedObject.ActivateLight();
                        this._currentPoints++;
                    }
                    else
                    {
                        this.ResetPuzzle();
                    }
                }
                else if (this._currentPoints < this._maxPoints)
                {
                    interactedObject.ActivateLight();
                    this._currentPoints++;
                }                
            }
            else
            {
                this.ResetPuzzle();
            }
        }
        else if (this._currentPoints < this._maxPoints)
        {
            this.ResetPuzzle();
        }
    }
}
