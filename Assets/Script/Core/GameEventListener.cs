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
        public GameEventSO gameEventSO;

        public UnityEvent response;

        private void OnEnable()
        {
            gameEventSO.RegisterListener(this);
        }
        private void OnDisable()
        {
            gameEventSO.UnRegisterListener(this);
        }

        public void OnEventRaised(Component sender, object data)
        {
            response.Invoke();
        }
    }
    
}

