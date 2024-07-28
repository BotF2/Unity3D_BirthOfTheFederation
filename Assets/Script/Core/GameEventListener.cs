using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Core
{
    [System.Serializable]
    public class CustomGameEvent : UnityEvent<Component, object>
    {
        
    }
    public class GameEventListener : MonoBehaviour
    {
        public TrekEventSO trekEventSO;

        public UnityEvent response; // link method calls in editor

        private void OnEnable()
        {
            
        }
        private void OnDisable()
        {
            
        }
        public void OnEventRaised(Component sender, object data)
        {
            response.Invoke();
        }
    }
    
}

