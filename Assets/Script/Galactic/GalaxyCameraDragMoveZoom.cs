using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GalaxyCameraDragMoveZoom : MonoBehaviour //, IPointerClickHandler
{
    [SerializeField]
    private Camera galaxyCam;
    [SerializeField]
    private GameObject cameraHolder;
    [SerializeField]
    private float panSpeed = 400f;
    [SerializeField]
    private float zoomSpeed = 400f;
    [SerializeField]
    private float minY = 123f;
    [SerializeField]
    private float maxY = 800f;
    [SerializeField]
    private float mouseSpeed = 2f;
    [SerializeField]
    private float minX = -600f;
    [SerializeField]
    private float maxX = 600f;
    [SerializeField]
    private float minZ = -1140f;
    [SerializeField]
    private float maxZ = 500f;

   // private Vector3 dragOrigin;
    //private Vector3 cameraDragOrigin;
    //private Vector3 currentPosition;
    private Vector3 lastMousePosition;

    void Update()
    {
        DoZoom();
        KeyboardInputs();
        DrageCameraWithLeftMouse();
        RotateCamerWithRightMouse();
        CameraMoveLimits();   
    }

    private void MoveCamera(float xInput, float zInput)
    {
        float zMove = Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180) * zInput + Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180) * xInput;
        float xMove = Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180) * zInput - Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180) * xInput;
        transform.position = transform.position + new Vector3(xMove, 0, zMove);
    }
    private void DoZoom()
    {
        galaxyCam.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        if (Input.GetKey("q"))
        {
            galaxyCam.fieldOfView += 1f;
        }
        if (Input.GetKey("e"))
        {
            galaxyCam.fieldOfView -= 1f;
        }
        galaxyCam.fieldOfView = Mathf.Clamp(galaxyCam.fieldOfView, 25f, 90f);

    }

    void DrageCameraWithLeftMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            if (EventSystem.current != null)
            {
                if (!EventSystem.current.IsPointerOverGameObject()) // do not drage camera when over UI
                {
                    Vector3 delta = (Input.mousePosition - lastMousePosition) / mouseSpeed;//
                    MoveCamera(delta.x, delta.y);
                    lastMousePosition = Input.mousePosition;
                }
            }
        }
    }
    void RotateCamerWithRightMouse()
    {
        if (Input.GetMouseButtonDown(1))
        {
            
            lastMousePosition.y = Input.mousePosition.y;
        }
        if (Input.GetMouseButton(1))
        {
            var rotation = cameraHolder.transform.eulerAngles.x;
            //if(rotation != 0f)
            //{
            //    if (rotation > 60)
            //        cameraHolder.transform.eulerAngles = new Vector3(60f, transform.eulerAngles.y, transform.eulerAngles.z);
            //    else if (rotation < -45)
            //        cameraHolder.transform.eulerAngles = new Vector3(-45f, transform.eulerAngles.y, transform.eulerAngles.z);
            //    else
            //    {
            //        float delta = (rotation += (Input.mousePosition.y - lastMousePosition.y) / mouseSpeed);
            //        cameraHolder.transform.eulerAngles = new Vector3(delta, transform.eulerAngles.y, transform.eulerAngles.z);
            //    }
            //}
            float delta = (rotation += (Input.mousePosition.y - lastMousePosition.y) / mouseSpeed);
            cameraHolder.transform.eulerAngles = new Vector3(delta, transform.eulerAngles.y, transform.eulerAngles.z);

            //lastMousePosition.y = Input.mousePosition.y;
            //if (cameraHolder.transform.rotation.x > 60f)
            //    cameraHolder.transform.eulerAngles = new Vector3(60f, transform.eulerAngles.y, transform.eulerAngles.z);
            //else if (cameraHolder.transform.rotation.x < -45f)
            //    cameraHolder.transform.eulerAngles = new Vector3(-45f, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        //var rotation = transform.eulerAngles.x;
        //float scroll = Input.GetAxis("Mouse ScrollWheel");
        //pos.y += scroll * scrollSpeed * Time.deltaTime * 300f;
        //float xAngle = (pos.y - 300f) * 0.006f; 
        //transform.eulerAngles = new Vector3(xAngle, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    // get keyboard inputs
    void KeyboardInputs()
    {
        float inputZ = 0f;
        float inputX = 0f;

        if (Input.GetKey("w"))
        {
            inputZ += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            inputZ -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            inputX += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            inputX -= panSpeed * Time.deltaTime;
        }
        MoveCamera(inputX, inputZ);
    }

    void CameraMoveLimits()
    {
        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
        transform.position = pos;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }
}

