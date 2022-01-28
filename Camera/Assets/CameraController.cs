using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance = null;

    [SerializeField] private Camera camera;

    // configuration de départ
    [SerializeField] private CameraConfiguration baseConfiguration;

    // Configuration actuelle
    [SerializeField] private CameraConfiguration currentConfiguration;

    // Configuration ciblée
    [SerializeField] private CameraConfiguration targetConfiguration;

    [SerializeField] private List<AView> activeViews = new List<AView>();

    // Configuration du blend entre toutes les views
    CameraConfiguration blendViewConfig = new CameraConfiguration();

    [SerializeField] private float blendSpeed = 0.1f;
    float blendTimer = 0;
    bool isBlending = false;

    // Configuration lissée
    CameraConfiguration smoothedConfiguration = new CameraConfiguration();

    private bool isCutRequested = false;


    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        instance = this;
    }

    private void Start()
    {
        ApplyConfiguration(camera, baseConfiguration);
        currentConfiguration = baseConfiguration;

        BlendCameraConfig();
        isBlending = true;
    }

    private void Update()
    {
        BlendCameraConfig();
        //ApplyConfiguration(camera, targetConfiguration);

        if (!isCutRequested)
        {
            SmoothBlendBetweenConfigs();
        }
        else
        {
            Cut();
        }
    }

    void ApplyConfiguration(Camera camera, CameraConfiguration configuration)
    {
        camera.transform.position = configuration.GetPosition();
        camera.transform.rotation = configuration.GetRotation();
    }

    void SmoothBlendBetweenConfigs()
    {
           if(blendSpeed * Time.deltaTime < 1)
            {
                currentConfiguration = PlusConfig(currentConfiguration, MinusConfig(targetConfiguration, currentConfiguration));

                ApplyConfiguration(camera, currentConfiguration);
            }
            else
            {
                currentConfiguration = targetConfiguration;
                ApplyConfiguration(camera, currentConfiguration);
        }
    }

    CameraConfiguration PlusConfig(CameraConfiguration config1, CameraConfiguration config2)
    {
        CameraConfiguration plus = new CameraConfiguration();

        plus.pitch = config1.pitch + config2.pitch;
        plus.yaw = config1.yaw + config2.yaw;
        plus.roll = config1.roll + config2.roll;
        plus.distance = config1.distance + config2.distance;
        plus.fov = config1.fov + config2.fov;
        plus.pivot = config1.pivot + config2.pivot;

        return plus;
    }

    CameraConfiguration MinusConfig(CameraConfiguration config1, CameraConfiguration config2)
    {
        float timer = blendSpeed * Time.deltaTime;

        CameraConfiguration minus = new CameraConfiguration();

        minus.pitch = (config1.pitch - config2.pitch) * timer;
        minus.yaw = (config1.yaw - config2.yaw) * timer;
        minus.roll = (config1.roll - config2.roll) * timer;
        minus.distance = (config1.distance - config2.distance) * timer;
        minus.fov = (config1.fov - config2.fov) * timer;
        minus.pivot = (config1.pivot - config2.pivot) * timer;

        return minus;
    }


    public void AddView(AView view) {
        activeViews.Add(view);
    }

    public void RemoveView(AView view) {
        AView viewToRemove = activeViews.Find((viewToFind) => viewToFind == view);
        activeViews.Remove(viewToRemove);
    }

    public void BlendCameraConfig()
    {
        float totalPitch = 0;
        float totalRoll = 0;
        float totalDistance = 0;
        float totalFov = 0;
        Vector3 totalPivot = Vector3.zero;

        float totalWeigh = 0;

        foreach (AView view in activeViews)
        {
            totalPitch += view.GetConfiguration().pitch * view.weight;
            totalRoll += view.GetConfiguration().roll * view.weight;

            totalWeigh += view.weight;

            totalDistance += view.GetConfiguration().distance * view.weight;
            totalFov += view.GetConfiguration().fov * view.weight;

            totalPivot += view.GetConfiguration().pivot * view.weight;
        }

        float blendedPitch = totalPitch / totalWeigh;
        float blendedRoll = totalRoll / totalWeigh;
        float blendedYaw = ComputeAverageYaw();

        float blendedDistance = totalDistance / totalWeigh;
        float blendedFov = totalFov / totalWeigh;

        Vector3 blendedPivot = totalPivot / totalWeigh;

        blendViewConfig.pitch = blendedPitch;
        blendViewConfig.roll = blendedRoll;
        blendViewConfig.yaw = blendedYaw;

        blendViewConfig.distance = blendedDistance;
        blendViewConfig.fov = blendedFov;

        blendViewConfig.pivot = blendedPivot;

        targetConfiguration = blendViewConfig;
    }

    public float ComputeAverageYaw()
    {
        Vector2 sum = Vector2.zero;
        foreach (AView view in activeViews)
        {
            CameraConfiguration config = view.GetConfiguration();
            sum += new Vector2(Mathf.Cos(config.yaw * Mathf.Deg2Rad),
                Mathf.Sin(config.yaw * Mathf.Deg2Rad)) * view.weight;
        }
        return Vector2.SignedAngle(Vector2.right, sum);
    }

    public void Cut()
    {
        ApplyConfiguration(camera, targetConfiguration);
        currentConfiguration = targetConfiguration;
    }


    private void OnDrawGizmos()
    {
        if(currentConfiguration != null)
        {
            currentConfiguration.DrawGizmos(Color.red);
        }
    }
}
