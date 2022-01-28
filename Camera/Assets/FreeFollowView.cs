using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFollowView : AView {
    private List<float> pitch = new List<float>();
    private List<float> roll = new List<float>();
    private List<float> fov = new List<float>();
    [Range(0,2)]
    public int indexConf;
    public float yaw;
    public float yawSpeed;

    public GameObject target;

    public Curve curve;
    [Range(0,1)]
    public float curvePosition;
    public float curveSpeed;

    // Start is called before the first frame update
    void Start() {
        pitch.Add(0f);
        pitch.Add(0.5f);
        pitch.Add(1f);

        roll.Add(0f);
        roll.Add(0.5f);
        roll.Add(1f);

        fov.Add(0f);
        fov.Add(0.5f);
        fov.Add(1f);
    }

    // Update is called once per frame
    void Update() {
        CheckInput();
    }

    public override CameraConfiguration GetConfiguration() {
        CameraConfiguration config = new CameraConfiguration();

        Matrix4x4 curveToWorldMatric = ComputeCurveToWorldMatrix();

        float lerp = Mathf.Lerp(0, 1, pitch[indexConf]);
        Vector3 pos = curve.GetPosition(lerp, curveToWorldMatric);

        config.pivot = pos;
        config.distance = 5;
        config.yaw = yaw;

        //config.pitch = pitch[indexConf];
        //config.roll = roll[indexConf];
        //config.fov = fov[indexConf];

        return config;

    }

    public Matrix4x4 ComputeCurveToWorldMatrix() {
        Quaternion rotation = Quaternion.Euler(0, yaw, 0);
        return Matrix4x4.TRS(target.transform.position, rotation, Vector3.one);
    }

    void CheckInput() {
        if (Input.GetKey(KeyCode.Q) && yaw > 0) {
            yaw -= yawSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D)) {
            yaw += yawSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Z)) {
            curvePosition += curveSpeed * Time.deltaTime;
            if (curvePosition >= 1)
                curvePosition = 1;
        }

        if (Input.GetKey(KeyCode.S)) {
            curvePosition -= curveSpeed * Time.deltaTime;
            if (curvePosition <= 0)
                curvePosition = 0;
        }
    }
}