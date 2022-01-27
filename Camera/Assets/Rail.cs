using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour
{
    private List<GameObject> railCheckpoints = new List<GameObject>();

    public bool isLoop = false;
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
        if (!isLoop)
        {
            if(distance <= length)
            {
                float totalLength = length;
                float currentDistance = distance;
                int travelIndex = 0;
                float distanceBetweenPoint = 0;
                while (totalLength > distance)
                {
                    var lengthToSubstract = (railCheckpoints[travelIndex].transform.position - railCheckpoints[travelIndex + 1].transform.position).magnitude;
                    totalLength -= lengthToSubstract;

                    distanceBetweenPoint = currentDistance - lengthToSubstract;
                    if (distanceBetweenPoint > 0)
                        currentDistance -= distanceBetweenPoint;

                    travelIndex++;

                }

                Debug.Log($"LENGTH LEFT: {totalLength} | PARCOURED DISTANCE: {distance} | CURRENTDISTANCE: {currentDistance} | DISTANCE BETWEEN POINT: {distanceBetweenPoint}");


                posToGo = railCheckpoints[travelIndex].transform.position +(railCheckpoints[travelIndex + 1].transform.position - railCheckpoints[travelIndex].transform.position).normalized * currentDistance;

            } else
            {
                posToGo = railCheckpoints[railCheckpoints.Count - 1].transform.position;
            }



            //if (distance <= length)
            //{
            //    // ça se dit pas mais c'est marrant
            //    float totalLength = length;
            //    int travelIndex = 0;

            //    while (totalLength > distance)
            //    {
            //        totalLength -= (railCheckpoints[travelIndex].transform.position - railCheckpoints[travelIndex + 1].transform.position).magnitude;
            //        travelIndex++;
            //    }

            //    if(travelIndex < railCheckpoints.Count - 1)
            //    {
            //        posToGo = (railCheckpoints[travelIndex].transform.position - railCheckpoints[travelIndex + 1].transform.position).normalized * distance;
            //    }

            //    //posToGo = railCheckpoints[travelIndex].transform.position;
            //}

            //else
            //{
            //    posToGo = railCheckpoints[railCheckpoints.Count - 1].transform.position;
            //}
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
