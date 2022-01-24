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

    public void DrawGizmos(Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(config.pivot, 0.25f);
        Vector3 position = config.GetPosition();
        Gizmos.DrawLine(config.pivot, config.GetPosition());
        Gizmos.matrix = Matrix4x4.TRS(config.GetPosition(), config.GetRotation(), Vector3.one);
        Gizmos.DrawFrustum(Vector3.zero, config.fov, 0.5f, 0f, Camera.main.aspect);
        Gizmos.matrix = Matrix4x4.identity;
    }

    private void OnDrawGizmos()
    {
        DrawGizmos(Color.red);
    }
}
