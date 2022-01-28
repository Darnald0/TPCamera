using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AViewVolume : MonoBehaviour
{
    public int priority = 0;
    public AView view;
    public bool isCutOnSwitch = false;

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

            if (isCutOnSwitch)
            {
                ViewVolumeBlender.instance.OnUpdate();
                CameraController.instance.Cut();
            }
        }
        else
        {
            ViewVolumeBlender.instance.RemoveVolume(this);
        }

        IsActive = active;
    }
}
