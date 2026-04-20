using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//HitPeeking
public class Puzzle9 : LevelPuzzle
{
    [SerializeField]
    private InteractableObject _targetObject;

    [SerializeField]
    private float _minTime1;
    [SerializeField]
    private float _maxTime1;
    [SerializeField]
    private float _minTime2;
    [SerializeField]
    private float _maxTime2;

    public override void Setup()
    {
        base.Setup();

        TVSignalTuner.instance.SetupCleanScreen();

        TVScreenPlayer.instance.PlayVideo(this._videoFileName);
    }    

    public override void UpdatePuzzleState(InteractableObject interactedObject, float value)
    {
        if (interactedObject == this._targetObject && this.IsWithinTimeWindow())
        {
            this.CompletePuzzle();
        }

    }

    private bool IsWithinTimeWindow()
    {
        double currentVideoTime = TVScreenPlayer.instance.GetVideoTime();

        bool withinWindow1 = (currentVideoTime >= this._minTime1 && currentVideoTime <= this._maxTime1);
        bool withinWindow2 = (currentVideoTime >= this._minTime2 && currentVideoTime <= this._maxTime2);

        return (withinWindow1 || withinWindow2);
    }
}
