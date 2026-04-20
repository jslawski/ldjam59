using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public static EndGame instance;

    [SerializeField]
    private GameObject _cutscenePanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ExecuteEndGame()
    {
        FaceController.instance.CloseEyes();
        this._cutscenePanel.SetActive(true);
    }
}
