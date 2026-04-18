using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField]
    private Transform _cameraHolderTransform;

    private Quaternion _targetRotation = Quaternion.identity;    
    private float _maxVerticalAngle = 30.0f;

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
        if (PlayerControlsManager.instance.IsPressingRightMouse() == true)
        {
            this.UpdateCameraRotation();
        }
    }

    private void UpdateCameraRotation()
    {
        Quaternion yRotation = Quaternion.AngleAxis(PlayerControlsManager.instance.lookDelta.y, Vector3.up);        
        Quaternion xRotation = Quaternion.Euler(this.GetClampedXAngle(), this._cameraHolderTransform.rotation.eulerAngles.y, 0.0f);

        this._targetRotation = yRotation * xRotation;

        this._cameraHolderTransform.rotation = this._targetRotation;
    }
}
