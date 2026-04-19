using System.Collections;
using UnityEngine;

public class RubbableObject : InteractableObject
{
    [SerializeField]
    private Collider _rubbableCollider;

    [HideInInspector]
    public float timeDragged = 0.0f;

    private Coroutine _dragCoroutine = null;

    public override void Interact()
    {        
        if (this._dragCoroutine == null)
        {
            this._dragCoroutine = StartCoroutine(this.DragCoroutine());
        }
        
    }

    private bool IsInsideBounds()
    {
        RaycastHit hitInfo = new RaycastHit();
        Ray mouseRay = PlayerControlsManager.instance.playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(mouseRay, out hitInfo, float.PositiveInfinity, PlayerControlsManager.instance.interactableObjectLayer, QueryTriggerInteraction.Ignore))
        {
            if (hitInfo.collider.gameObject.GetComponent<InteractableObject>() == PlayerControlsManager.instance.targetObject)
            {
                return true;
            }                        
        }

        return false;
    }

    private IEnumerator DragCoroutine()
    {
        this.timeDragged = 0.0f;

        while (PlayerControlsManager.instance.IsPressingLeftMouse() == true)
        {
            if (this.IsInsideBounds() == true)
            {
                yield return null;
                this.timeDragged += Time.deltaTime;

            }
            else
            {
                yield return null;
            }        
        }

        this._dragCoroutine = null;
    }
}
