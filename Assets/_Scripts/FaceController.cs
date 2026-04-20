using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceController : MonoBehaviour
{
    public static FaceController instance;    

    [SerializeField]
    private SkinnedMeshRenderer _faceMesh;
    [SerializeField]
    private Material _screenMaterial;

    public float masterVolume = 1.0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void FixedUpdate()
    {
        this.UpdateScreenState();
        this.UpdateMasterVolume();
    }

    private void UpdateScreenState()
    {
        if (this._faceMesh.GetBlendShapeWeight(0) >= 100.0f)
        {            
            this._screenMaterial.SetColor("_Color", Color.black);
        }
        else
        { 
            this._screenMaterial.SetColor("_Color", Color.white);
        }
    }

    private void UpdateMasterVolume()
    {
        if (this._faceMesh.GetBlendShapeWeight(0) >= 100.0f)
        {
            this.masterVolume = 0.0f;
            TVScreenPlayer.instance.UpdateVolume();
            return;
        }

        float mouthOpenWeight = this._faceMesh.GetBlendShapeWeight(1);
        this.masterVolume = (100.0f - mouthOpenWeight) / 100.0f;

        TVScreenPlayer.instance.UpdateVolume();
    }

    public void CloseEyes()
    {


        this._faceMesh.SetBlendShapeWeight(0, 100.0f);
    }

    public void CloseMouth()
    {
        this._faceMesh.SetBlendShapeWeight(1, 100.0f);
    }

    public bool AreEyesClosed()
    {
        return (this._faceMesh.GetBlendShapeWeight(0) >= 100.0f);
    }

    public bool IsMouthClosed()
    {
        return (this._faceMesh.GetBlendShapeWeight(1) >= 100.0f);
    }
}
