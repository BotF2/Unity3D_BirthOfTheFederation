using UnityEngine;

namespace Assets.Core
{
    public abstract class InputManagerGalactica : MonoBehaviour
    {
        public delegate void MoveInputHandler(Vector3 moveVector);
        public delegate void RotateInputHandler(float rotateAmount);
        public delegate void ZoomInputHandler(float zomeAmount);

    }
}
