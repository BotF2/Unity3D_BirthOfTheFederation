using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    [CreateAssetMenu(menuName = "GameEventSO")]
    public class GameEventSO : ScriptableObject
    {
        public List<GameEventListener> listeners = new List<GameEventListener>();

        public void Raise(Component sender, Object data)
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                listeners[i].OnEventRaised(sender, data);
            }
        }

        public void RegisterListener(GameEventListener listener)
        {
            if (!listeners.Contains(listener));
            listeners.Add(listener);
        }
        public void UnRegisterListener(GameEventListener listener)
        {
                if (listeners.Contains(listener))
                    listeners.Remove(listener);
        }
    }
}
