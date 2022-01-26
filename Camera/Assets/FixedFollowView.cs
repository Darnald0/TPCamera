using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedFollowView : AView
{
    public float roll;
    public float fov;

    public GameObject target;

    Vector3 direction = Vector3.zero;

    public GameObject centralPoint;
    public float yawOffsetMax;
    public float pitchOffsetMax;

    private void Update()
    {
        LookTarget();
    }

    void LookTarget()
    {
        direction = (target.transform.position - transform.position).normalized;
    }

    public override CameraConfiguration GetConfiguration()
    {
        CameraConfiguration config = new CameraConfiguration();
        //CameraConfiguration config = base.GetConfiguration();

        config.pivot = transform.position;
        config.distance = 0;
        var radianMax = yawOffsetMax * Mathf.PI / 180;
        var yaw = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        if (yaw < yawOffsetMax && yaw > -yawOffsetMax) {
            config.yaw = yaw;
        } else {
            if (yaw > 0) {
                config.yaw = yawOffsetMax;
            } else {
                config.yaw = -yawOffsetMax;
            }
        }

        var pitch = -Mathf.Asin(direction.y) * Mathf.Rad2Deg;
        if (pitch < pitchOffsetMax && pitch > -pitchOffsetMax) {
            config.pitch = pitch;
        } else {
            if (pitch > 0) {
                config.pitch = pitchOffsetMax;
            } else {
                config.pitch = -pitchOffsetMax;
            }
        }

        Debug.Log($"yaw: {yaw}");
        Debug.Log($"radianMax: {radianMax}");
        Debug.Log($"pitch: {pitch}");

        config.roll = roll;
        config.fov = fov;

        return config;
    }
}
