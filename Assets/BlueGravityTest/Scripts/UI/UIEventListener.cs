using System.Collections.Generic;
using BlueGravityTest.ScriptableObjects.Items;
using BlueGravityTest.Scripts.MVC;
using UnityEngine;
using UnityEngine.Events;

namespace BlueGravityTest.Scripts.UI
{
    public enum UICommands
    {
       OPEN_INVENTORY,
       SET_PLAYER
    }

    [System.Serializable]
    public struct UIParams
    {
        public UICommands Command;
        public PlayerModel Player;
    }

    [System.Serializable]
    public class UIUnityEvent : UnityEvent<UIParams>
    {
    }

    public class UIEventListener : MonoBehaviour
    {
        [SerializeField] private UIEvent uiEvent;
        [SerializeField] private UIUnityEvent response;

        private void OnEnable()
        {
            uiEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            uiEvent.UnregisterListener(this);
        }

        public void OnEventRaised(UIParams parameters)
        {
            response.Invoke(parameters);
        }
    }
}