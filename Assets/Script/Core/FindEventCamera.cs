using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;

public class FindEventCamera : MonoBehaviour
{
    private Canvas Canvas;
    private Camera Camera;
    void Start()
    {
        Canvas = GetComponent<Canvas>();
        Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Canvas.worldCamera = Camera;
    }
    private void OnMouseDown()
    {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            //goName = hitObject.name;
            if (hitObject == gameObject)
            {
               //??????????? FleetUIManager.instance.LoadFleetUI(gameObject);
            }
        }

    }

}