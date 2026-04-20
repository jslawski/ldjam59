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
        }
    }
}
