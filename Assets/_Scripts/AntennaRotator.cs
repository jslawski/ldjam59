using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntennaRotator : InteractableObject
{
    [SerializeField]
    private Transform _antennaBaseTransform;

    private float _maxAngle = 30.0f;

    private Vector2 _previousDiff = Vector2.zero;

    private float GetClampedZAngle()
    {
        float deltaX = PlayerControlsManager.instance.lookDelta.x;

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
        float deltaZ = PlayerControlsManager.instance.lookDelta.y;

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

        if (this._previousDiff.magnitude != 0)
        {
            if (this.ShouldPlaySqueak() == true)
            {
                this.PlaySqueakSound();
            }
        }

        Quaternion zAxisRotation = Quaternion.AngleAxis(this.GetClampedXAngle(), this._antennaBaseTransform.forward);        
        Quaternion xAxisRotation = Quaternion.AngleAxis(this.GetClampedZAngle(), this._antennaBaseTransform.right);

        this._antennaBaseTransform.rotation = (xAxisRotation * zAxisRotation);

        TVSignalTuner.instance.UpdateDistortion();

        if (this.onInteracted != null)
        {
            this.onInteracted(this);
        }

        this._previousDiff = PlayerControlsManager.instance.lookDelta;
    }

    private void PlaySqueakSound()
    {
        AudioChannelSettings audioChannelSettings = new AudioChannelSettings(false, 0.8f, 1.2f, 0.3f, "SFX");
        AudioClip audioClip = Resources.Load<AudioClip>("Audio/AntennaJostleSound");

        AudioManager.instance.Play(audioClip, audioChannelSettings);
    }

    private bool ShouldPlaySqueak()
    {
        Debug.LogError("Delta: " + PlayerControlsManager.instance.lookDelta.magnitude);
        return (PlayerControlsManager.instance.lookDelta.magnitude > 3.0f);
    
        //bool check1 = (this._previousDiff.x > 0 && PlayerControlsManager.instance.lookDelta.x < 0);
        //bool check2 = (this._previousDiff.x < 0 && PlayerControlsManager.instance.lookDelta.x > 0);
        //bool check3 = (this._previousDiff.y > 0 && PlayerControlsManager.instance.lookDelta.y < 0);
        //bool check4 = (this._previousDiff.y < 0 && PlayerControlsManager.instance.lookDelta.y > 0);

        //return (check1 || check2 || check3 || check4);

    }
}
