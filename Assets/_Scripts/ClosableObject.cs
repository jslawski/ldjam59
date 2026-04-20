using System.Collections;
using UnityEngine;

public class ClosableObject : InteractableObject
{
    private Coroutine _dragCoroutine;

    private float _currentDiff = 0.0f;
    private float _maxViewportDiff = 0.025f;

    [HideInInspector]
    public float _currentDragTime = 0.0f;

    [SerializeField]
    private SkinnedMeshRenderer _faceMesh;

    [SerializeField]
    private int _blendShapeIndex;

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

            //Update Blendshapes Here
            float normalizedValue = this.GetNormalizedValue(yViewportDiff);
            float tValue;


            if (this._blendShapeIndex == 0)
            {
                tValue = Mathf.Lerp(100.0f, 0.0f, normalizedValue);
            }
            else
            {
                tValue = Mathf.Lerp(0.0f, 100.0f, normalizedValue);
            }

            this._faceMesh.SetBlendShapeWeight(this._blendShapeIndex, tValue);

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
