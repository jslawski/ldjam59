using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVSignalTuner : MonoBehaviour
{
    public static TVSignalTuner instance;

    //Reference to Stephen's screen shader here
    [SerializeField]
    private Material _screenMaterial;

    [SerializeField]
    private Transform _antennaTransform;
    [SerializeField]
    private Transform _answerTransform;

    private List<int> _correctSideIndices;
    private Vector3 _correctAntennaAngle = Vector3.zero;

    private float _correctAngleBuffer = 0.1f;

    private float _currentStaticAmount = 1.0f;
    private float _currentDistortionAmount = 1.0f;

    private float _staticChangePerHit = 0.25f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        this._correctSideIndices = new List<int>();
    }

    private void Start()
    {
        this.SetupRandomTuning();
    }

    public void UpdateDistortion()
    {
        float correctRatio = Vector3.Dot(this._antennaTransform.up, this._answerTransform.up) + this._correctAngleBuffer;
        correctRatio = Mathf.Clamp(correctRatio, 0.0f, 1.0f);

        Debug.LogError("CorrectRatio: " + correctRatio);

        this._currentDistortionAmount = (1.0f - correctRatio);
        this._screenMaterial.SetFloat("_Distortion_Amount", this._currentDistortionAmount);
    }

    public void UpdateStatic(int index)
    {
        if (this._correctSideIndices.Contains(index) == true)
        {
            this._currentStaticAmount -= this._staticChangePerHit;
        }
        else
        {
            this._currentStaticAmount += this._staticChangePerHit;
        }

        this._currentStaticAmount = Mathf.Clamp(this._currentStaticAmount, 0.0f, 1.0f);

        this._screenMaterial.SetFloat("_Static", this._currentStaticAmount);
    }

    public void SetupRandomTuning()
    {
        this._currentStaticAmount = Random.Range(0.5f, 1.0f);
        this._screenMaterial.SetFloat("_Static", this._currentStaticAmount);

        this._currentDistortionAmount = Random.Range(0.5f, 1.0f);
        this._screenMaterial.SetFloat("_Distortion_Amount", this._currentDistortionAmount);

        this.SetupNewCorrectList();

        this.SetupCorrectAngle();
    }

    private void SetupNewCorrectList()
    {
        int numCorrectIndices = Random.Range(1, 4);
        this._correctSideIndices = new List<int>();

        while (this._correctSideIndices.Count < numCorrectIndices)
        {
            int randomIndex = Random.Range(0, 4);

            if (this._correctSideIndices.Contains(randomIndex) == false)
            {
                this._correctSideIndices.Add(randomIndex);
            }
        }
    }

    private void SetupCorrectAngle()
    {
        Vector3 currentAntennaRotation = this._antennaTransform.eulerAngles;

        int randomXDirection = (Random.Range(0, 1) == 0) ? -1 : 1 ;
        float randomXDiff = Random.Range(25.0f, 60.0f);
        int randomZDirection = (Random.Range(0, 1) == 0) ? -1 : 1;
        float randomZDiff = Random.Range(25.0f, 60.0f);

        float correctXAngle = currentAntennaRotation.x + (randomXDirection * randomXDiff);
        float correctZAngle = currentAntennaRotation.z + (randomZDirection * randomZDiff);

        correctXAngle = Mathf.Clamp(correctXAngle, -30.0f, 30.0f);
        correctZAngle = Mathf.Clamp(correctZAngle, -30.0f, 30.0f);

        this._answerTransform.rotation = Quaternion.Euler(new Vector3(correctXAngle, currentAntennaRotation.y, correctZAngle));
    }
}
