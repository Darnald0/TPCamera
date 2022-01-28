using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve : MonoBehaviour {
    public Vector3 A;
    public Vector3 B;
    public Vector3 C;
    public Vector3 D;

    // Update is called once per frame
    void Update() {
        OnDrawGizmos();
    }

    public Vector3 GetPosition(float t) {
        return MathUtils.QuadraticBezier(A, B, C, t);
    }

    public Vector3 GetPosition(float t, Matrix4x4 localToWorldMatrix) {
        Vector3 pLocal = MathUtils.CubicBezier(A, B, C, D, t);
        Vector3 pWorld = localToWorldMatrix.MultiplyPoint(pLocal);
        return pWorld;
    }


    void OnDrawGizmos() {
        Gizmos.color = Color.red;

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawFrustum(Vector3.zero, 1f, 0.5f, 0f, Camera.main.aspect);
        Gizmos.matrix = Matrix4x4.identity;
    }
}
