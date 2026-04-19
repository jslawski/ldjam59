using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntennaRotator : InteractableObject
{
    [SerializeField]
    private Transform _antennaBaseTransform;

    private float _maxAngle = 30.0f;


    private float GetClampedZAngle()
    {
        float deltaX = -PlayerControlsManager.instance.lookDelta.x;

        float xRotation = this._antennaBaseTransform.rotation.eulerAngles.x + deltaX;

        if (xRotation > 180.0f)
        {
            xRotation -= 360.0f;
        }

        xRotation = Mathf.Clamp(xRotation, -this._maxAngle, this._maxAngle);

        return xRotation;
    }

    private float GetClampedXAngle()
    {
        float deltaZ = -PlayerControlsManager.instance.lookDelta.y;

        float zRotation = this._antennaBaseTransform.rotation.eulerAngles.z + deltaZ;

        if (zRotation > 180.0f)
        {
            zRotation -= 360.0f;
        }

        zRotation = Mathf.Clamp(zRotation, -this._maxAngle, this._maxAngle);

        return zRotation;
    }

    public override void Interact()
    {
        //Quaternion zAxisRotation = Quaternion.Euler(0.0f, 0.0f, this.GetClampedXAngle());
        //Quaternion xAxisRotation = Quaternion.Euler(this.GetClampedZAngle(), 0.0f, 0.0f);

        Quaternion zAxisRotation = Quaternion.AngleAxis(this.GetClampedXAngle(), this._antennaBaseTransform.forward);        
        Quaternion xAxisRotation = Quaternion.AngleAxis(this.GetClampedZAngle(), this._antennaBaseTransform.right);

        this._antennaBaseTransform.rotation = (xAxisRotation * zAxisRotation);

    }
}
