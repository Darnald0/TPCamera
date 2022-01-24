using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraConfiguration : MonoBehaviour
{
    public float yaw;
    public float pitch;
    public float roll;

    public Vector3 pivot;
    public float distance;

    public float fov;

    public Quaternion GetRotation()
    {
        return Quaternion.Euler(pitch, yaw, roll);
    }

    public Vector3 GetPosition()
    {
        return GetRotation() * (Vector3.back * distance);
    }
}

