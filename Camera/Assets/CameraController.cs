using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance = null;

    [SerializeField] private Camera camera;
    //[SerializeField] private CameraConfiguration config;

    private List<AView> activeViews = new List<AView>();

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

    public void AddView(AView view) {
        activeViews.Add(view);
    }

    public void RemoveView(AView view) {
        AView viewToRemove = activeViews.Find((viewToFind) => viewToFind == view);
        activeViews.Remove(viewToRemove);
    }

    public void BlendCameraConfig() {
        float totalPitch = 0;
        float totalRoll = 0;

        float totalWeigh = 0;
        for (int i = 0; i < activeViews.Count; i++) {
            totalPitch += activeViews[i].GetConfiguration().pitch * activeViews[i].weight;
            totalRoll += activeViews[i].GetConfiguration().roll * activeViews[i].weight;

            totalWeigh += activeViews[i].weight;
        }

        float blendedPitch = totalPitch / totalWeigh;
        float blendedRoll = totalRoll / totalWeigh;
        float blendedYaw = ComputeAverageYaw();
    }

    public float ComputeAverageYaw() {
        Vector2 sum = Vector2.zero;
        foreach (AView view in activeViews) {
            sum += new Vector2(Mathf.Cos(view.GetConfiguration().yaw * Mathf.Deg2Rad),
            Mathf.Sin(view.GetConfiguration().yaw * Mathf.Deg2Rad)) * view.weight;
        }
        return Vector2.SignedAngle(Vector2.right, sum);
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
