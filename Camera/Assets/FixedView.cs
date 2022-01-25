using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedView : AView
{
    public float yaw;
    public float pitch;
    public float roll;
    public float fov;

    public override CameraConfiguration GetConfiguration() {
        // pivot  = transform.position;
        // distance = 0;
        return base.GetConfiguration();
    }
}
