using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleController : MonoBehaviour
{
    public Canvas CanvasToolTip;
    public Camera galaxyEventCamera;

    void Start()
    {
        CanvasToolTip.worldCamera = galaxyEventCamera;
    }


}
