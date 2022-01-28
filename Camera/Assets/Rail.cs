using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour
{
    private List<GameObject> railCheckpoints = new List<GameObject>();

    public bool isLoop = false;
    //public bool isAuto = false;

    private float length;

    Vector3 posToGo = Vector3.zero;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            railCheckpoints.Add(transform.GetChild(i).gameObject);
        }

        GetLength();
    }

    public float GetLength()
    {
        length = 0;

        for (int i = 0; i < railCheckpoints.Count; i++)
        {
            if(i < railCheckpoints.Count - 1)
            {
                length += (railCheckpoints[i].transform.position - railCheckpoints[i + 1].transform.position).magnitude;
            }

            else
            {
                if (isLoop)
                {
                    length += (railCheckpoints[i].transform.position - railCheckpoints[0].transform.position).magnitude;
                }
            }
        }

        return length;
    }

    public Vector3 GetPosition(float distance) {

        //if (!isAuto) {
            if (!isLoop) {
                if (distance <= length) {
                    float travelLength = 0;
                    float traveledDist = distance;
                    int travelIndex = 0;

                    while (traveledDist > 0) {
                        var distAtoB = (railCheckpoints[travelIndex].transform.position - railCheckpoints[travelIndex + 1].transform.position).magnitude;

                        travelLength += distAtoB;
                        traveledDist -= distAtoB;

                        travelIndex++;

                    }

                    //Debug.Log($"TravelIndex : {travelIndex}");
                    if (travelIndex > 0) {
                        //Debug.Log($"TravelIndex : {travelIndex} | count : {railCheckpoints.Count - 1} ");

                        //if (travelIndex > railCheckpoints.Count - 2) {
                        //    posToGo = railCheckpoints[travelIndex].transform.position + (railCheckpoints[travelIndex - 1].transform.position - railCheckpoints[travelIndex].transform.position).normalized * (travelLength - distance);
                        //    //Debug.Log("supérior");
                        //} else {
                        posToGo = railCheckpoints[travelIndex].transform.position + (railCheckpoints[travelIndex - 1].transform.position - railCheckpoints[travelIndex].transform.position).normalized * (travelLength - distance);
                        //Debug.Log("inférior");
                        //}
                    }
                    //else
                    //{
                    //    posToGo = railCheckpoints[0].transform.position + (railCheckpoints[1].transform.position - railCheckpoints[0].transform.position).normalized * (travelLength - distance);
                    //}
                    else if (travelIndex == 0) {
                        posToGo = railCheckpoints[0].transform.position;
                    } else {
                        posToGo = railCheckpoints[railCheckpoints.Count - 1].transform.position;
                    }
                }
            }

        //}
        return posToGo;
    }

    public Vector3 GetPositionOnSegment(Vector3 target) {

        float smalestDistance = 1000;
        Vector3 posOnSegment = Vector3.zero;
        for (int i = 0; i < railCheckpoints.Count - 1; i++) {
            Vector3 pointOnSegment = MathUtils.instance.GetNeareastPointOnSegment(railCheckpoints[i].transform.position, railCheckpoints[i + 1].transform.position, target);
            float distanceTargetSegment = (pointOnSegment - target).magnitude;
            //Debug.Log(distanceTargetSegment);
            //Debug.Log($"VECTOR = {pointOnSegment} | MAGNITUDE = {pointOnSegment.magnitude}| SMALLEST = {smalestDistance}");
            if (distanceTargetSegment < smalestDistance) {
                smalestDistance = distanceTargetSegment;
                posOnSegment = pointOnSegment;
            }
        }
        //Debug.Log($"POS ON SEGMENT = {posOnSegment}");

        return posOnSegment;
    }

    void OnDrawGizmos()
    {
        for (int i = 0; i < railCheckpoints.Count; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(railCheckpoints[i].transform.position, 0.25f);

            if(i < railCheckpoints.Count - 1)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(railCheckpoints[i].transform.position, railCheckpoints[i + 1].transform.position);
            }

            if (isLoop)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(railCheckpoints[railCheckpoints.Count - 1].transform.position, railCheckpoints[0].transform.position);
            }
        }
    }
}
