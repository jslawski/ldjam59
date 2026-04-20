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
    }

    private void Update()
    {
        if (TVSignalTuner.instance.IsSignalClean() == true)
        {
            if (this._waitCoroutine == null)
            {
                this._waitCoroutine = StartCoroutine(this.TurnOffTVAfterWait());
            }
        }
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
            
        }
    }
}
