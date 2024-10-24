using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "GalaxyMapDestinationEvent")]
public class GalaxyMapEvent : ScriptableObject
{
    public List<GalaxyMapEvents> galaxyMapListeners = new List<GalaxyMapEvents>();
    // Rise event through different method signatures
    public void RaiseGalaxyMapEvent()
    {
        for (int i = 0; i < galaxyMapListeners.Count; ++i)
        {
            //galaxyMapListeners[i].OnEventRaised();
        }
    }
    public void RegisterListener(GalaxyMapEvents listener)
    {
        if (!galaxyMapListeners.Contains(listener))
        {
            galaxyMapListeners.Add(listener);
        }
    }
    public void UnregisterListener(GalaxyMapEvents listener)
    {
        if (galaxyMapListeners.Contains(listener))
        {
            galaxyMapListeners.Remove(listener);
        }
    }
}
