using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//RotatePlayback
public class Puzzle13 : LevelPuzzle
{
    private float _minRotateThreshold = 0.25f;
    private float _maxRotateThreshold = 7.0f;

    private float _minPlaybackSpeed = 0.5f;
    private float _maxPlaybackSpeed = 5.0f;

    [SerializeField]
    private CameraRotator _cameraRotator;

    [SerializeField]
    private InteractableObject[] _targetObjects;

    public override void Setup()
    {
        base.Setup();

        TVSignalTuner.instance.SetupRandomTuning();

        TVScreenPlayer.instance.PlayVideo(this._videoFileName);
        TVScreenPlayer.instance.SetPlaybackSpeed(0.0f);

        this._maxPoints = 6;

        StartCoroutine(this.ProcessPuzzle());
    }

    private IEnumerator ProcessPuzzle()
    {
        while (true)
        {
            TVScreenPlayer.instance.SetPlaybackSpeed(0.0f);
        
            if (this._cameraRotator.GetYRotationDiff() >= this._minRotateThreshold)
            {
                TVScreenPlayer.instance.SetPlaybackSpeed(this.GetPlaybackSpeed());
            }
            
            yield return null;                
        }        
    }

    public override void UpdatePuzzleState(InteractableObject interactedObject, float value)
    {
        if (interactedObject == this._targetObjects[this._currentPoints])
        {
            interactedObject.ActivateLight();
            this._currentPoints++;            
        }
        else
        {
            this.ResetPuzzle();
        }

        if (this._currentPoints >= this._maxPoints)
        {
            StopAllCoroutines();
            this.CompletePuzzle();
            TVScreenPlayer.instance.SetPlaybackSpeed(1.0f);            
        }
    }

    private float GetPlaybackSpeed()
    {
        float normalizedRotationDiff = this.GetNormalizedValue(this._cameraRotator.GetYRotationDiff());

        float targetPlaybackSpeed = Mathf.Lerp(this._minPlaybackSpeed, this._maxPlaybackSpeed, normalizedRotationDiff);

        return targetPlaybackSpeed;
    }

    private float GetNormalizedValue(float unNormalizedValue)
    {
        float oldMin = this._minRotateThreshold;
        float oldMax = this._maxRotateThreshold;

        return ((unNormalizedValue - oldMin) / (oldMax - oldMin));
    }
}
