using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core 
{
    public class BillboardCameraGalactica : MonoBehaviour
    {
        private Camera cameraGal;

        void Start()
        {
            foreach (Camera camera in Camera.allCameras)
            {
                if (camera.tag == "Galactic Camera")
                {
                    cameraGal = camera;
                }
            }
        }

        // Update is called once per frame
        void LateUpdate()
        {
            transform.LookAt(cameraGal.transform, Vector3.up);
            transform.rotation = cameraGal.transform.rotation;
        }
    }
}