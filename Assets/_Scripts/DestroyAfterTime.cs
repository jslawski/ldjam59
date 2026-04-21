using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyMe", 3.0f);
    }
    private void DestroyMe()
    {
        Destroy(this.gameObject);
    }    
}
