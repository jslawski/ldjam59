using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GoStatic
public class Puzzle7 : LevelPuzzle
{    
    public override void Setup()
    {
        for (int i = 0; i < this._interactableObjects.Length; i++)
        {
            this._interactableObjects[i].onInteracted += this.UpdatePuzzleState;
        }

        TVSignalTuner.instance.SetupCleanScreen();

        TVScreenPlayer.instance.PlayVideo(this._videoFileName);

        FaceController.instance.OpenMouth();

        StartCoroutine(this.ProcessPuzzle());
    }

    private IEnumerator ProcessPuzzle()
    {
        while (TVSignalTuner.instance.currentStaticAmount < 1.0f)
        {
            yield return null;
        }

        CompletePuzzle();
    }
}
