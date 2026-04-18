using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlsManager : MonoBehaviour
{
    public static PlayerControlsManager instance;

    private PlayerControls _playerControls;

    [HideInInspector]
    public Vector3 lookDelta = Vector3.zero;

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
        if (this._playerControls.PlayerMap.RightClick.inProgress == false)
        {
            this.lookDelta = Vector3.zero;
            return;
        }        

        Vector2 mouseDelta = context.ReadValue<Vector2>();
        
        this.lookDelta = new Vector3(mouseDelta.y, -mouseDelta.x, 0.0f);
        this.lookDelta *= PlayerControlsManager.mouseSensitivity;
    }

    private void StopRotateDirection(InputAction.CallbackContext context)
    {
        this.lookDelta = Vector3.zero;
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
