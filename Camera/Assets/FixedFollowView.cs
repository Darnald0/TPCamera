using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedFollowView : AView
{
    public float roll;
    public float fov;

    public GameObject target;

    Vector3 dirToTarget = Vector3.zero;
    Vector3 dirToCenter = Vector3.zero;

    public GameObject centralPoint;
    public float yawOffsetMax;
    public float pitchOffsetMax;

    private void Update()
    {
        LookTarget();
    }

    void LookTarget()
    {
        dirToTarget = (target.transform.position - transform.position).normalized;
        dirToCenter = (centralPoint.transform.position - transform.position).normalized;
    }

    public override CameraConfiguration GetConfiguration()
    {
        CameraConfiguration config = new CameraConfiguration();
        //CameraConfiguration config = base.GetConfiguration();

        config.pivot = transform.position;
        config.distance = 0;
        var radianMax = yawOffsetMax * Mathf.PI / 180;
        var yawTarget = Mathf.Atan2(dirToTarget.x, dirToTarget.z) * Mathf.Rad2Deg;
        var yawCenter = Mathf.Atan2(dirToCenter.x, dirToCenter.z) * Mathf.Rad2Deg;
        var deltaYaw = yawTarget - yawCenter;

        while(deltaYaw > 180)
        {
            deltaYaw -= 360;
        }

        while(deltaYaw < -180)
        {
            deltaYaw += 360;
        }

        deltaYaw = Mathf.Clamp(deltaYaw, -yawOffsetMax, yawOffsetMax);

        var pitchToTarget = -Mathf.Asin(dirToTarget.y) * Mathf.Rad2Deg;
        var pitchToCenter = -Mathf.Asin(dirToCenter.y) * Mathf.Rad2Deg;
        var deltaPitch = pitchToTarget - pitchToCenter;
        deltaPitch = Mathf.Clamp(deltaPitch, -pitchOffsetMax, pitchOffsetMax);


        Debug.Log($"yaw: {yawTarget}");
        Debug.Log($"radianMax: {radianMax}");
        //Debug.Log($"pitch: {pitch}");

        config.yaw = deltaYaw + yawCenter;
        config.pitch = deltaPitch + pitchToCenter;
        config.roll = roll;
        config.fov = fov;

        return config;
    }
}
