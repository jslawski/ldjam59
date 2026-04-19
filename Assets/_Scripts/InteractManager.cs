using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    void Update()
    {
        if (PlayerControlsManager.instance.IsPressingLeftMouse() == true)
        {
            if (PlayerControlsManager.instance.targetObject != null)
            {
                PlayerControlsManager.instance.targetObject.Interact();
            }
        }
    }
}
