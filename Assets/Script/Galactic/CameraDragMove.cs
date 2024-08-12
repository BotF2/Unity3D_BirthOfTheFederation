using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Unity.VisualScripting;

public class CameraDragMove : MonoBehaviour //, IPointerClickHandler
{
    // Button Input Vars
    public float panSpeed = 400f;
    public float scrollSpeed = 200f;
    public float minY = 5f;
    public float maxY = 400f;
    public float mouseSpeed = 2f;
    public float minX = -600f;
    public float maxX = 600f;
    public float minZ = -1140f;
    public float maxZ = 500f;

    private Vector3 dragOrigin;
    private Vector3 cameraDragOrigin;
    private Vector3 currentPosition;
    private Vector3 lastMousePosition;

    void Update()
    {
        KeyboardInputs();
        DrageCameraWithMouse();
        UpDownCameraOnMouseWheel();       
    }

    private void MoveCamera(float xInput, float zInput)
    {
        float zMove = Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180) * zInput + Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180) * xInput;
        float xMove = Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180) * zInput - Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180) * xInput;
        transform.position = transform.position + new Vector3(xMove, 0, zMove);
    }


    // Get mouse drag inputs

    void DrageCameraWithMouse()
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

        if (Input.GetMouseButtonUp(0))
        {
        }
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
        if (Input.GetKey("d"))
        {
            inputX += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            inputX -= panSpeed * Time.deltaTime;
        }
        MoveCamera(inputX, inputZ);
    }

    // zoom via scrollwheel
    void UpDownCameraOnMouseWheel()
    {
        Vector3 pos = transform.position;
        //consider getting rotation so at max y=400 look at local angle 10 and at min y=5 look at angle 10
        var rotation = transform.eulerAngles.x;
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y += scroll * scrollSpeed * Time.deltaTime * 300f;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
        transform.eulerAngles = new Vector3((pos.y - 250f) * 0.06f, transform.eulerAngles.y, transform.eulerAngles.z);
        transform.position = pos;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }
}

