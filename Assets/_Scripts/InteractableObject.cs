using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class InteractableObject : MonoBehaviour
{
    [HideInInspector]
    public Vector3 interactContactPoint;

    public delegate void OnInteract(InteractableObject thisObject, float value = 0.0f);
    public OnInteract onInteracted;

    public abstract void Interact();
}
