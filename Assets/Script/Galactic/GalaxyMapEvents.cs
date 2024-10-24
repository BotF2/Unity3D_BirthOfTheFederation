using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GalaxyMapEvents : MonoBehaviour
{
    public static GalaxyMapEvents current;

    private void Awake()
    {
        current = this;
    }
    public event Action<GameObject> onSetDestination;

    public void DestinationSet(GameObject destination)
    {
        if (onSetDestination != null)
        {
            onSetDestination(destination);
        }
    }
    public event Action<GameObject> onRemoveDestination;
    public void RemoveDestination(GameObject destination) 
    {
        if (onRemoveDestination != null)
        {
            onRemoveDestination(destination);
        }
    }
}
