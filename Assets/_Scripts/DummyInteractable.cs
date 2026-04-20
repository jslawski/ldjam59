using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyInteractable : InteractableObject
{
    public override void Interact()
    {
        if (this.onInteracted != null)
        {
            this.onInteracted(this);
        }
    }
}
