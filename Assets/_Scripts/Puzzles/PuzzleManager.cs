using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;

    private LevelPuzzle[] _allPuzzles;

    [SerializeField]
    private int _currentPuzzleIndex = 0;

    [SerializeField]
    private Transform _tvTransform;

    [SerializeField]
    private CameraRotator _cameraRotator;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        this._allPuzzles = GetComponentsInChildren<LevelPuzzle>();
    }

    private void Start()
    {
        this._allPuzzles[this._currentPuzzleIndex].Setup();
    }

    public void LoadNextPuzzle()
    {
        Debug.LogError("PUZZLE " + this._currentPuzzleIndex + " COMPLETED!");
    
        this._currentPuzzleIndex++;

        StartCoroutine(this.TransitionToNextPuzzle());
    }

    public void JumpToPuzzle(int puzzleIndex)
    {
        this._currentPuzzleIndex = puzzleIndex;
        StartCoroutine(this.TransitionToNextPuzzle());
    }

    private IEnumerator TransitionToNextPuzzle()
    {
        TVSignalTuner.instance.SetFullStatic();
    
        yield return new WaitForSeconds(1.0f);

        if (this._currentPuzzleIndex < this._allPuzzles.Length)
        {
            this._allPuzzles[this._currentPuzzleIndex].Setup();
        }
        else
        {
            Debug.LogError("GAME COMPLETE!");
            StartCoroutine(this.PlayEndSequence());
        }
    }

    private IEnumerator PlayEndSequence()
    {
        this._cameraRotator.LockRotation();

        FaceController.instance.DeactivateAllLights();
        FaceController.instance.LightFaceObject("_End_GlowLight");

        yield return new WaitForSeconds(2.0f);

        this._tvTransform.DOShakeRotation(3.0f, 20.0f, 10, 60.0f, false, ShakeRandomnessMode.Full);

        yield return new WaitForSeconds(2.5f);

        EndGame.instance.ExecuteEndGame();
    }
}
