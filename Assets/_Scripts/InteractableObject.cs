using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    [SerializeField]
    private string _associatedLightName;

    [HideInInspector]
    public Vector3 interactContactPoint;
    
    public delegate void OnInteract(InteractableObject thisObject, float value = 0.0f);
    public OnInteract onInteracted;

    public virtual void Interact() { }

    public virtual void ActivateLight()
    {
        if (this._associatedLightName != string.Empty)
        {
            FaceController.instance.LightFaceObject(this._associatedLightName);
        }
    }

    public virtual void DeactivateLight()
    {
        if (this._associatedLightName != string.Empty)
        {
            FaceController.instance.UnlightFaceObject(this._associatedLightName);
        }
    }
}
