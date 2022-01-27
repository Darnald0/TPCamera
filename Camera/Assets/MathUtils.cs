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
        float scalAC = Vector3.Dot(a, target);
        float normAB = Vector3.SqrMagnitude(b - a);
        float distAB = Vector3.Distance(a, b);

        float clampedScal = Mathf.Clamp(scalAC, 0, distAB);

        Vector3 projC = (new Vector3(a.x + normAB * clampedScal, a.y + normAB * clampedScal, a.z + normAB * clampedScal));

        return projC;
    }
}
