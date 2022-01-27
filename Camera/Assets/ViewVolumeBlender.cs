using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewVolumeBlender : MonoBehaviour
{
    public static ViewVolumeBlender instance = null;

    private List<AViewVolume> activeViewVolumes = new List<AViewVolume>();
    private Dictionary<AView, List<AViewVolume>> volumesPerViews = new Dictionary<AView, List<AViewVolume>>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        instance = this;
    }

    public void AddVolume(AViewVolume volumeToAdd)
    {
        activeViewVolumes.Add(volumeToAdd);

        if (!volumesPerViews.ContainsKey(volumeToAdd.view))
        {
            List<AViewVolume> newViewVolume = new List<AViewVolume>();
            newViewVolume.Add(volumeToAdd);

            volumesPerViews.Add(volumeToAdd.view, newViewVolume);
            volumeToAdd.view.SetActive(true);
        }
    }

    public void RemoveVolume(AViewVolume volumeToRemove)
    {
        activeViewVolumes.Remove(volumeToRemove);

        if (volumesPerViews.ContainsKey(volumeToRemove.view))
        {
            volumesPerViews.Remove(volumeToRemove.view);

            volumeToRemove.view.SetActive(false);
        }
    }
}
