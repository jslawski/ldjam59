using UnityEngine;

public class SideBanger : InteractableObject
{
    public override void Interact()
    {
        Debug.LogError("Player SMACKED " + this.gameObject.name);

        PlayerControlsManager.instance.targetObject = null;
    }
}
