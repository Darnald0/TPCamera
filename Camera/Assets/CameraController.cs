using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance = null;

    [SerializeField] private Camera camera;
    [SerializeField] private CameraConfiguration config;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        instance = this;
    }

    void ApplyConfiguration(Camera camera, CameraConfiguration configuration)
    {
        config = configuration;
        camera.transform.position = configuration.GetPosition();
        camera.transform.rotation = configuration.GetRotation();
    }

    private void OnDrawGizmos()
    {
        config.DrawGizmos(Color.red);
    }
}
