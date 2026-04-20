using UnityEngine;

public class LevelPuzzle : MonoBehaviour
{
    [SerializeField]
    protected string _videoFileName;

    [SerializeField]
    protected InteractableObject[] _interactableObjects;

    protected int _maxPoints;
    protected int _currentPoints;

    public virtual void Setup()
    {
        for (int i = 0; i < this._interactableObjects.Length; i++)
        {
            this._interactableObjects[i].onInteracted += this.UpdatePuzzleState;
        }
    }

    public virtual void CompletePuzzle()
    {
        for (int i = 0; i < this._interactableObjects.Length; i++)
        {
            this._interactableObjects[i].onInteracted -= this.UpdatePuzzleState;
        }

        TVSignalTuner.instance.SetFullStatic();

        PuzzleManager.instance.LoadNextPuzzle();
    }

    public virtual void UpdatePuzzleState(InteractableObject interactedObject, float value)
    { 
    
    }
}
