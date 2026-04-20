using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CloseEyesmouthforehead
public class Puzzle16 : LevelPuzzle
{
    [SerializeField]
    private CameraRotator _cameraRotator;

    public override void Setup()
    {
        base.Setup();

        TVSignalTuner.instance.SetupCleanScreen();

        TVScreenPlayer.instance.PlayVideo(this._videoFileName);
    }

    public override void CompletePuzzle()
    {
        for (int i = 0; i < this._interactableObjects.Length; i++)
        {
            this._interactableObjects[i].onInteracted -= this.UpdatePuzzleState;
        }

        TVSignalTuner.instance.SetFullStatic();

        StartCoroutine(this.PuzzleFinishedSequence());
    }

    public override void UpdatePuzzleState(InteractableObject interactedObject, float value)
    {
        if (FaceController.instance.AreEyesClosed() && FaceController.instance.IsMouthClosed())
        {
            interactedObject.ActivateLight();
            this.CompletePuzzle();
        }
    }

    private IEnumerator PuzzleFinishedSequence()
    {
        this._cameraRotator.LockRotation();

        FaceController.instance.OpenEyes();
        FaceController.instance.OpenMouth();

        yield return new WaitForSeconds(1.0f);

        this._cameraRotator.RotateToScreen();

        PuzzleManager.instance.LoadNextPuzzle();

        this._cameraRotator.ConstrainCamera();
    }
}
