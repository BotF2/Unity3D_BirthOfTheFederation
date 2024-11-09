using UnityEngine;

namespace Assets.Core
{
    public class Billboard : MonoBehaviour
    {

        private Camera theCam;

        void LateUpdate()
        {
            if (theCam == null)
                theCam = Camera.main;
            else
                transform.forward = Camera.main.transform.forward;
        }
    }
}