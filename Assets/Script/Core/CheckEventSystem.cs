using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{

    public class CheckEventSystem : MonoBehaviour
    {
        public GameObject eventSystemGameObject;

        void Update()
        {
            if (eventSystemGameObject != null && !eventSystemGameObject.activeInHierarchy)
            {
                Debug.LogWarning("EventSystem is inactive!");
                eventSystemGameObject.SetActive(true);
            }
        }
    }
}