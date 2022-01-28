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

    public static Vector3 LinearBezier(Vector3 A, Vector3 B, float t) {
        return (1 - t) * A + t * B;
    }

    public static Vector3 QuadraticBezier(Vector3 A, Vector3 B, Vector3 C, float t) {
        return (1 - t) * LinearBezier(A,B,t) + t * LinearBezier(B,C,t);
    }

    public static Vector3 CubicBezier(Vector3 A, Vector3 B, Vector3 C, Vector3 D, float t) {
            return (1 - t) * QuadraticBezier(A, B, C, t) + t * QuadraticBezier(B, C, D, t);
    }
}
