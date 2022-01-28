using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereViewVolume : AViewVolume
{
    public GameObject target;
    public float innerRadius;
    public float outerRadius;

    private float distance;

    private void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        distance = (transform.position - target.transform.position).magnitude;

        if(distance <= outerRadius && !IsActive)
        {
            SetActive(true);
        }
        else if(distance > outerRadius && IsActive)
        {
            SetActive(false);
        }
    }

    public override float ComputeSelfWeight()
    {
        // faire la moyenne de la distance entre entre inner et outer (poids de 1 max)

        float sphereWeight = (outerRadius - innerRadius) / distance;

        sphereWeight = Mathf.Clamp(sphereWeight, 0, 1);

        return sphereWeight;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, innerRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, outerRadius);
    }
}
