using UnityEngine;

public class LevelPuzzle : MonoBehaviour
{
    [SerializeField]
    protected string _videoFileName;

    [SerializeField]
    protected InteractableObject[] _interactableObjects;

    protected int _maxPoints;
    protected int _currentPoints;

    //protected bool _isCurrentPuzzle = false;

    public virtual void Setup()
    {
        for (int i = 0; i < this._interactableObjects.Length; i++)
        {
            this._interactableObjects[i].onInteracted += this.UpdatePuzzleState;
            this._interactableObjects[i].DeactivateLight();
        }

        FaceController.instance.DeactivateAllLights();

        FaceController.instance.OpenEyes();
        FaceController.instance.OpenMouth();
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

    public virtual void ResetPuzzle()
    {
        Debug.LogError("RESET!");
        this._currentPoints = 0;

        for (int i = 0; i < this._interactableObjects.Length; i++)
        {
            this._interactableObjects[i].DeactivateLight();
        }        
    }
}
