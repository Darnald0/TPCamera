using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyView : AView
{
    public float roll;
    public float distance;
    public float fov;

    public GameObject target;

    public Rail rail;
    public float distanceOnRail;
    public float speed;

    private void Update()
    {
        CheckInput();
    }

    public override CameraConfiguration GetConfiguration()
    {
        CameraConfiguration config = new CameraConfiguration();

        config.pivot = rail.GetPosition(distanceOnRail);
        config.distance = 0;

        //config.yaw = yaw;
        //config.pitch = pitch;
        config.roll = roll;
        config.fov = fov;

        return config;

    }

    void CheckInput()
    {
        if (Input.GetKey(KeyCode.Q) && distanceOnRail > 0)
        {
            distanceOnRail -= speed * Time.deltaTime;

            if(distanceOnRail < 0)
            {
                distanceOnRail = 0;
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (rail.isLoop)
            {
                distanceOnRail += speed * Time.deltaTime;
            }
            else
            {
                if (distanceOnRail < rail.GetLength())
                {
                    distanceOnRail += speed * Time.deltaTime;

                    if (distanceOnRail > rail.GetLength())
                    {
                        distanceOnRail = rail.GetLength();
                    }
                }
            }
        }
    }
}
