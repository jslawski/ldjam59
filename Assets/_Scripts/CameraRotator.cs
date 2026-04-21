using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraRotator : MonoBehaviour
{
    [SerializeField]
    private Transform _cameraHolderTransform;

    private Quaternion _targetRotation = Quaternion.identity;
    private float _maxVerticalAngle = 30.0f;

    private float _constrainedAngleDiff = 15.0f;

    private float _previousYRotation = 0.0f;

    private bool _rotateLocked = false;
    private bool _rotateConstrained = false;

    private void Start()
    {
        this._previousYRotation = this._cameraHolderTransform.rotation.eulerAngles.y;
    }

    private float GetClampedXAngle()
    {
        float deltaX = PlayerControlsManager.instance.lookDelta.x;

        float xRotation = this._cameraHolderTransform.rotation.eulerAngles.x + deltaX;

        if (xRotation > 180.0f)
        {
            xRotation -= 360.0f;
        }

        xRotation = Mathf.Clamp(xRotation, -this._maxVerticalAngle, this._maxVerticalAngle);

        return xRotation;
    }

    private void FixedUpdate()
    {
        if (this._rotateLocked == false && PlayerControlsManager.instance.IsPressingRightMouse() == true)
        {
            this.UpdateCameraRotation();
        }
        else
        {
            this._previousYRotation = this._cameraHolderTransform.rotation.eulerAngles.y;
        }
    }

    private void UpdateCameraRotation()
    {
        this._previousYRotation = this._cameraHolderTransform.rotation.eulerAngles.y;

        float newYAngle = _previousYRotation + PlayerControlsManager.instance.lookDelta.y;
        float rotateDiff = PlayerControlsManager.instance.lookDelta.y;

        if (this._rotateConstrained == true)
        {
            if (newYAngle < (180.0 - this._constrainedAngleDiff) || newYAngle > (180 + this._constrainedAngleDiff))
            {
                rotateDiff = 0.0f;
            }
        }

        Quaternion yRotation = Quaternion.AngleAxis(rotateDiff, Vector3.up);
        Quaternion xRotation = Quaternion.Euler(this.GetClampedXAngle(), this._cameraHolderTransform.rotation.eulerAngles.y, 0.0f);

        this._targetRotation = yRotation * xRotation;

        this._cameraHolderTransform.rotation = this._targetRotation;
    }

    public float GetYRotationDiff()
    {
        return (this._previousYRotation - this._cameraHolderTransform.rotation.eulerAngles.y);
    }

    public void LockRotation()
    {
        this._rotateLocked = true;
    }

    public void UnlockRotation()
    {
        this._rotateLocked = false;
    }

    public void ConstrainCamera()
    {
        this._rotateConstrained = true;
    }

    public void UnconstrainCamera()
    {
        this._rotateConstrained = false;
    }

    public void RotateToScreen()
    {
        StartCoroutine(this.ForceRotation(0.5f));
    }

    private IEnumerator ForceRotation(float duration)
    {
        this._rotateLocked = true;
        this._cameraHolderTransform.DORotate(new Vector3(0.0f, 180.0f, 0.0f), duration);
        yield return new WaitForSeconds(duration);
        this._rotateLocked = false;
    }

    public void ZoomToPosition(Vector3 targetPosition)
    {
        PlayerControlsManager.instance.playerCamera.transform.DOLocalMove(targetPosition, 6.0f);
    }

    public bool ScreenIsVisible()
    {
        return (this._cameraHolderTransform.rotation.eulerAngles.y < 240.0 && this._cameraHolderTransform.rotation.eulerAngles.y > 120.0f);
    }
}
