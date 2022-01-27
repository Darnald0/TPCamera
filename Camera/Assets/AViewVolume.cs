using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AViewVolume : MonoBehaviour
{
    public int priority = 0;
    public AView view;

    protected bool IsActive { get; private set; }

    public virtual float ComputeSelfWeight()
    {
        return 1.0f;
    }

    protected void SetActive(bool active)
    {
        view.SetActive(active);

        if (active)
        {
            ViewVolumeBlender.instance.AddVolume(this);
        }
        else
        {
            ViewVolumeBlender.instance.RemoveVolume(this);
        }

        IsActive = active;
    }
}
