using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AView : MonoBehaviour
{
    public float weight;
    public bool isActiveOnStart;

    private void Start() 
    {
        if (isActiveOnStart) 
        {
            SetActive(true);
        }
    }

    public virtual CameraConfiguration GetConfiguration() {
        CameraConfiguration config = new CameraConfiguration();
        return config;
    }

    public void SetActive(bool isActive) 
    {
        gameObject.SetActive(true);
    }
}
