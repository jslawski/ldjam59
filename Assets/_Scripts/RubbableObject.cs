using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RubbableObject : InteractableObject
{
    [SerializeField]
    private Collider _rubbableCollider;

    private Vector3 _minWorldPoint;
    private Vector3 _maxWorldPoint;

    public float timeDragged = 0.0f;

    private Coroutine _dragCoroutine = null;

    public override void Interact()
    {        
        if (this._dragCoroutine == null)
        {
            this._dragCoroutine = StartCoroutine(this.DragCoroutine());
        }
        
    }
    /*
    private void SetBoundValues()
    {
        Vector3 colliderPosition = this._rubbableCollider.gameObject.transform.position;
        float xExtent = this._rubbableCollider.bounds.extents.x;
        float yExtent = this._rubbableCollider.bounds.extents.y;


        this._minWorldPoint = new Vector3(colliderPosition.x - xExtent, colliderPosition.y - yExtent, colliderPosition.z);
        this._maxWorldPoint = new Vector3(colliderPosition.x + xExtent, colliderPosition.y + yExtent, colliderPosition.z);

        //Debug.LogError("Min WorldPoint: " + minWorldPoint + "\nMax WorldPoint: " + maxWorldPoint);

        //this._minScreenPoint = PlayerControlsManager.instance.playerCamera.WorldToScreenPoint(minWorldPoint);
        //this._maxScreenPoint = PlayerControlsManager.instance.playerCamera.WorldToScreenPoint(maxWorldPoint);

        //Debug.LogError("Min ScreenPoint: " + this._minScreenPoint + "\nMax Screenpoint: " + this._maxScreenPoint + "\nMouse Position: " + Input.mousePosition);
    }
    */
    private bool IsInsideBounds()
    {
        RaycastHit hitInfo = new RaycastHit();
        Ray mouseRay = PlayerControlsManager.instance.playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(mouseRay, out hitInfo, float.PositiveInfinity, PlayerControlsManager.instance.interactableObjectLayer, QueryTriggerInteraction.Ignore))
        {
            Debug.LogError("Hit Something! " + hitInfo.collider.gameObject.name);
            if (hitInfo.collider.gameObject.GetComponent<InteractableObject>() == PlayerControlsManager.instance.targetObject)
            {
                return true;
            }                        
        }

        Debug.LogError("Hit nothing...");

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

                Debug.LogError("Dragging...");
            }
            else
            {
                Debug.LogError("Out of Bounds!");
                yield return null;
            }        
        }

        Debug.LogError("Drag Complete!");
        this._dragCoroutine = null;
    }
}
