using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AView : MonoBehaviour
{
    public float weight;
    public bool isActiveOnStart;

    private void Start() 
    {
            SetActive(isActiveOnStart);
    }

    public virtual CameraConfiguration GetConfiguration() {
        CameraConfiguration config = new CameraConfiguration();
        return config;
    }

    public void SetActive(bool isActive) 
    {
        if (isActive)
            CameraController.instance.AddView(this);
        else
            CameraController.instance.RemoveView(this);
    }
}
