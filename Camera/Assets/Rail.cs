using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour
{
    private List<GameObject> railCheckpoints = new List<GameObject>();

    public bool isLoop = false;
    public bool isAuto = false;

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

    public Vector3 GetPosition(float distance)
    {

        if (!isAuto)
        {
            if (!isLoop)
            {
                if (distance <= length)
                {
                    float travelLength = 0;
                    float traveledDist = distance;
                    int travelIndex = 0;

                    while (traveledDist > 0)
                    {
                        var distAtoB = (railCheckpoints[travelIndex].transform.position - railCheckpoints[travelIndex + 1].transform.position).magnitude;

                        travelLength += distAtoB;
                        traveledDist -= distAtoB;

                        travelIndex++;
                    }

                    if (travelIndex > 0)
                    {
                        posToGo = railCheckpoints[travelIndex].transform.position + (railCheckpoints[travelIndex + 1].transform.position - railCheckpoints[travelIndex].transform.position).normalized * (travelLength - distance);
                    }
                    else
                    {
                        posToGo = railCheckpoints[0].transform.position + (railCheckpoints[1].transform.position - railCheckpoints[0].transform.position).normalized * (travelLength - distance);
                    }

                    Debug.Log($"Travel Length : {travelLength} & TraveledDist : {traveledDist} & pointOnDist : {travelLength - distance}");
                }
                else
                {
                    posToGo = railCheckpoints[railCheckpoints.Count - 1].transform.position;
                }
            }
        }

        else if (isAuto)
        {

        }
        

        return posToGo;
    }


    private void OnDrawGizmos()
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
