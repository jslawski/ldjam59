using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Break antenna
public class Puzzle17 : LevelPuzzle
{
    [SerializeField]
    private string _reactionVideoFileName;
    [SerializeField]
    private CameraRotator _cameraRotator;

    private float _timeBeforeBreak = 20.0f;

    private Coroutine _videoChangeCoroutine = null;
    private Coroutine _antennaCoroutine = null;

    public override void Setup()
    {
        base.Setup();

        TVSignalTuner.instance.SetupCleanScreen();

        TVScreenPlayer.instance.PlayVideo(this._videoFileName);       
    }

    public override void UpdatePuzzleState(InteractableObject interactedObject, float value)
    {
        if (this._antennaCoroutine == null)
        {
            this._antennaCoroutine = StartCoroutine(this.AntennaCoroutine());
        }
    }

    public override void CompletePuzzle()
    {
        this._cameraRotator.UnconstrainCamera();
    
        base.CompletePuzzle();
    }

    private IEnumerator AntennaCoroutine()
    {
        float currentTime = 0.0f;

        this.ChangeVideo(this._reactionVideoFileName);

        while (currentTime < this._timeBeforeBreak && PlayerControlsManager.instance.IsPressingLeftMouse())
        {
            yield return null;
            currentTime += Time.deltaTime;
        }

        if (currentTime >= this._timeBeforeBreak)
        {            
            this.BreakAntenna();
        }
        else
        {
            this.ChangeVideo(this._videoFileName);            
        }

        this._antennaCoroutine = null;
    }

    private void ChangeVideo(string videoName)
    {
        if (this._videoChangeCoroutine != null)
        {
            StopCoroutine(this._videoChangeCoroutine);
            this._videoChangeCoroutine = null;
        }
    
        this._videoChangeCoroutine = StartCoroutine(ChangeVideoAfterStatic(videoName));
    }

    private IEnumerator ChangeVideoAfterStatic(string videoName)
    {
        TVSignalTuner.instance.SetFullStatic();

        yield return new WaitForSeconds(0.2f);

        TVScreenPlayer.instance.PlayVideo(videoName);

        TVSignalTuner.instance.SetupCleanScreen();

        this._videoChangeCoroutine = null;
    }

    private void BreakAntenna()
    {
        for (int i = 0; i < this._interactableObjects.Length; i++)
        {
            this._interactableObjects[i].onInteracted -= this.UpdatePuzzleState;
        }

        GameObject antennaGameObject = this._interactableObjects[0].gameObject;
        this._interactableObjects[0].enabled = false;

        antennaGameObject.transform.parent = null;
        antennaGameObject.layer = LayerMask.NameToLayer("Default");
        Rigidbody antennaRigidbody = antennaGameObject.AddComponent<Rigidbody>();
        antennaRigidbody.AddForce(Vector3.up * 1.0f, ForceMode.Impulse);

        this.CompletePuzzle();
    }
}
