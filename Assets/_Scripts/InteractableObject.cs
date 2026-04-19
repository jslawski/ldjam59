using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    [HideInInspector]
    public Vector3 interactContactPoint;

    public abstract void Interact();
}
