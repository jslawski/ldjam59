using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVRotator : MonoBehaviour
{
    [SerializeField]
    private Transform _tvTransform;

    private Quaternion _targetRotation = Quaternion.identity;
    //private float _rotateSpeed = 8.0f;
    private float _maxVerticalAngle = 90.0f;

    private float GetClampedXAngle()
    {
        float deltaX = PlayerControlsManager.instance.lookDelta.x;

        float xRotation = this._tvTransform.rotation.eulerAngles.x + deltaX;

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
            this.UpdateTVRotation();
        }
    }

    private void UpdateTVRotation()
    {
        Quaternion yRotation = Quaternion.AngleAxis(PlayerControlsManager.instance.lookDelta.y, Vector3.up);
        //Quaternion xRotation = Quaternion.AngleAxis(this.GetClampedXAngle(), this._tvTransform.right);
        
        Quaternion xRotation = Quaternion.Euler(this.GetClampedXAngle(), this._tvTransform.rotation.eulerAngles.y, 0.0f);

        this._targetRotation = yRotation * xRotation;

        this._tvTransform.rotation = this._targetRotation;
    }    
}
