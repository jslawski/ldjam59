using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlsManager : MonoBehaviour
{
    public static PlayerControlsManager instance;
    
    public Camera playerCamera;

    [SerializeField]
    private LayerMask _interactableObjectLayer;


    private PlayerControls _playerControls;

    [HideInInspector]
    public Vector3 lookDelta = Vector3.zero;

    [HideInInspector]
    public InteractableObject targetObject;

    private static float mouseSensitivity = 0.25f;

    private void Awake()
    {        
        if (instance == null)
        {
            instance = this;
        }

        this._playerControls = new PlayerControls();

        this._playerControls.PlayerMap.MoveMouse.performed += this.UpdateRotateDirection;
        this._playerControls.PlayerMap.MoveMouse.canceled += this.StopRotateDirection;

        this._playerControls.PlayerMap.LeftClick.performed += this.UpdateTargetObject;
        this._playerControls.PlayerMap.LeftClick.canceled += this.ClearTargetObject;
    }

    private void OnEnable()
    {
        this._playerControls.Enable();
    }

    private void OnDisable()
    {
        this._playerControls.Disable();
    }

    private void UpdateRotateDirection(InputAction.CallbackContext context)
    {
        if (this._playerControls.PlayerMap.LeftClick.inProgress == false && this._playerControls.PlayerMap.RightClick.inProgress == false)
        {
            this.lookDelta = Vector3.zero;
            return;
        }        

        Vector2 mouseDelta = context.ReadValue<Vector2>();
        
        //This is cursed.  Why is this written this way?  I'm too scared to change it now...
        this.lookDelta = new Vector3(-mouseDelta.y, mouseDelta.x, 0.0f);
        this.lookDelta *= PlayerControlsManager.mouseSensitivity;
    }

    private void StopRotateDirection(InputAction.CallbackContext context)
    {
        this.lookDelta = Vector3.zero;
    }

    private void UpdateTargetObject(InputAction.CallbackContext context)
    {
        RaycastHit hitInfo = new RaycastHit();
        Ray mouseRay = this.playerCamera.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(mouseRay, out hitInfo, float.PositiveInfinity, this._interactableObjectLayer, QueryTriggerInteraction.Ignore))
        {
            InteractableObject potentialComponent = hitInfo.collider.gameObject.GetComponent<InteractableObject>();           

            if (potentialComponent != null)
            {
                this.targetObject = potentialComponent;
            }
            else
            {
                this.targetObject = null;
            }
        }
        else
        {
            this.targetObject = null;
        }
    }

    private void ClearTargetObject(InputAction.CallbackContext context)
    {
        this.targetObject = null;
    }

    public bool IsPressingRightMouse()
    {
        return this._playerControls.PlayerMap.RightClick.inProgress;
    }
    public bool IsPressingLeftMouse()
    {
        return this._playerControls.PlayerMap.LeftClick.inProgress;
    }
}
