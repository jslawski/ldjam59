using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Reveal Vid, then Close Eyes
public class Puzzle6 : LevelPuzzle
{
    private Coroutine _waitCoroutine = null;

    private float _timeToWaitInSeconds = 3.0f;

    public override void Setup()
    {
        base.Setup();

        TVSignalTuner.instance.SetupRandomTuning();

        TVScreenPlayer.instance.PlayVideo(this._videoFileName);

        StartCoroutine(this.ProcessPuzzle());
    }

    private IEnumerator ProcessPuzzle()
    {
        while (TVSignalTuner.instance.IsSignalClean() == false)
        {
            yield return null;
        }

        StartCoroutine(this.TurnOffTVAfterWait());
    }

    private IEnumerator TurnOffTVAfterWait()
    {
        float currentWaitTime = 0.0f;

        while (TVSignalTuner.instance.IsSignalClean() && currentWaitTime < this._timeToWaitInSeconds)
        {
            currentWaitTime += Time.deltaTime;
            yield return null;
        }

        if (TVSignalTuner.instance.IsSignalClean())
        {
            FaceController.instance.CloseEyes();

            yield return new WaitForSeconds(0.5f);           

            this.CompletePuzzle();
        }
        else
        {
            StartCoroutine(this.ProcessPuzzle());
        }
    }
}
