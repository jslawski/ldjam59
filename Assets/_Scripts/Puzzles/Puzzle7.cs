using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GoStatic
public class Puzzle7 : LevelPuzzle
{
    private bool _playedSoundEffect = false;
    
    [SerializeField]
    private CameraRotator _cameraRotator;

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
            if (this._cameraRotator.ScreenIsVisible() == true && this._playedSoundEffect == false)
            {
                this._playedSoundEffect = true;

                AudioChannelSettings audioChannelSettings = new AudioChannelSettings(false, 1.0f, 1.0f, 0.3f, "SFX");
                AudioClip audioClip = Resources.Load<AudioClip>("Audio/HorrorStringSound");

                AudioManager.instance.Play(audioClip, audioChannelSettings);

            }
            
            yield return null;
        }

        CompletePuzzle();
    }
}
