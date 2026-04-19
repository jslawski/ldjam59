using System.Collections;
using UnityEngine;

public class ClosableObject : InteractableObject
{
    private Coroutine _dragCoroutine;

    private float _currentDiff = 0.0f;
    private float _maxViewportDiff = 0.025f;

    [HideInInspector]
    public float _currentDragTime = 0.0f;

    public override void Interact()
    {
        if (this._dragCoroutine == null)
        {
            this._dragCoroutine = StartCoroutine(this.DragCoroutine()); 
        }        
    }

    private IEnumerator DragCoroutine()
    {
        Vector3 interactScreenSpacePoint = PlayerControlsManager.instance.playerCamera.WorldToScreenPoint(this.interactContactPoint);
        Vector3 interactViewportSpacePoint = PlayerControlsManager.instance.playerCamera.ScreenToViewportPoint(interactScreenSpacePoint);

        this._currentDiff = 0.0f;
        this._currentDragTime = 0.0f;

        while (PlayerControlsManager.instance.IsPressingLeftMouse() == true)
        {                           
            this._currentDiff -= PlayerControlsManager.instance.lookDelta.x;           

            Vector3 diffScreenPoint = new Vector3(interactScreenSpacePoint.x, interactScreenSpacePoint.y + this._currentDiff, interactScreenSpacePoint.z);
            Vector3 diffViewportPoint = PlayerControlsManager.instance.playerCamera.ScreenToViewportPoint(diffScreenPoint);

            float yViewportDiff = (diffViewportPoint.y - interactViewportSpacePoint.y);
            yViewportDiff = Mathf.Clamp(yViewportDiff, -this._maxViewportDiff, this._maxViewportDiff);

            Debug.LogError("Current Diff: " + yViewportDiff);

            //Update Blendshapes Here


            float normalizedValue = this.GetNormalizedValue(yViewportDiff);
            float tValue = Mathf.Lerp(0.0f, 100.0f, normalizedValue);

            Debug.LogError("Normalized Value: " + normalizedValue + "\nT-Value: " + tValue);

            yield return null;

            this._currentDragTime += Time.deltaTime;
        }

        this._dragCoroutine = null;        
    }

    private float GetNormalizedValue(float unNormalizedValue)
    {
        float oldMin = -this._maxViewportDiff;
        float oldMax = this._maxViewportDiff;

        return ((unNormalizedValue - oldMin) / (oldMax - oldMin));
    }
}
