using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GalaxyMapDestinationEvent")]
public class GalaxyMapEvents : MonoBehaviour 
{
    public List<GalaxyMapOurEvent> GalaxyMapEventList = new List<GalaxyMapOurEvent>();
    // Rise event through different method signatures
    public void RaiseGalaxyMapEvent()
    {
        for (int i = 0; i < GalaxyMapEventList.Count; ++i)
        {
            //??? GalaxyMapEventList[i].;
        }
    }
    public void RegisterListener(GalaxyMapOurEvent listener)
    {
        if (!GalaxyMapEventList.Contains(listener))
        {
            GalaxyMapEventList.Add(listener);
        }
    }
    public void UnregisterListener(GalaxyMapOurEvent listener)
    {
        if (GalaxyMapEventList.Contains(listener))
        {
            GalaxyMapEventList.Remove(listener);
        }
    }
}
