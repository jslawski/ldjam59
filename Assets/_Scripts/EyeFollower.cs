using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFollower : MonoBehaviour
{
    private Transform _subjectTransform;

    private float _cameraZDiff = 2.65f;

    private float _zOffset = 1.5f;

    private void Awake()
    { 
        this._subjectTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseVector = Input.mousePosition;
        mouseVector.z = this._cameraZDiff;

        Vector3 mouseWorldPosition = PlayerControlsManager.instance.playerCamera.ScreenToWorldPoint(mouseVector);

        this._subjectTransform.position = new Vector3(-mouseWorldPosition.x, -mouseWorldPosition.y, this._zOffset);
    }
}
