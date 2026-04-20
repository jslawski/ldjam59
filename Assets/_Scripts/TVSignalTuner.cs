using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVSignalTuner : MonoBehaviour
{
    public static TVSignalTuner instance;    

//Reference to Stephen's screen shader her

    private Dictionary<SideBanger, int> _remainingBangs;
    private Vector3 _correctAntennaAngle = Vector3.zero;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        this._remainingBangs = new Dictionary<SideBanger, int>();
    }

    public void SetupRandomTuning(int numBangs)
    { 
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
