using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathUtils : MonoBehaviour
{
    public static MathUtils instance = null;

    private void Awake()
    {
        instance = this;
    }

    public Vector3 GetNeareastPointOnSegment(Vector3 a, Vector3 b, Vector3 target)
    {
        Vector3 AT = target - a;
        Vector3 dir = (b - a).normalized;
        float scalAC = Vector3.Dot(AT, dir);
        scalAC = Mathf.Clamp(scalAC, 0, Vector3.Distance(b,a));
        Vector3 projC = a + dir * scalAC;

        return projC;
    }
}
