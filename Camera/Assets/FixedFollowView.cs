using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedFollowView : AView
{
    public float roll;
    public float fov;

    public GameObject target;

    Vector3 direction = Vector3.zero;

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

        config.yaw = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        config.pitch = -Mathf.Asin(direction.y) * Mathf.Rad2Deg;
        config.roll = roll;
        config.fov = fov;

        return config;
    }
}
