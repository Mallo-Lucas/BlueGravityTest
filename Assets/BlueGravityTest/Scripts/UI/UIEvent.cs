using System.Collections.Generic;
using UnityEngine;

namespace BlueGravityTest.Scripts.UI
{
    [CreateAssetMenu(menuName = "ScriptableObjects/UIEvent", fileName = "UIEvent", order = 5)]
    public class UIEvent : ScriptableObject 
    {
        private List<UIEventListener> listeners = new();

        public void Raise(UIParams p) 
        {
            for (var i = listeners.Count - 1; i >= 0; i--) 
            {
                listeners[i].OnEventRaised(p); 
            }
        }

        public void RegisterListener(UIEventListener listener) 
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(UIEventListener listener) 
        {
            listeners.Remove(listener);
        }
    }
}