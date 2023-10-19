using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BlueGravityTest.Scripts.MVC
{
    public class PlayerController : MonoBehaviour
    {
        
        public Action<Vector2> OnMove;
        public Action OnInteract;
        
        [SerializeField] private PlayerInput playerInput;
        
        private InputAction _moveAction;
        private InputAction _onInteract;
        
        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (playerInput == null)
                return;

            _moveAction = playerInput.actions["Move"];
            _onInteract = playerInput.actions["Interact"];
            SubscribeInputs();
        }

        private void SubscribeInputs()
        {
            _moveAction.performed += MoveAction;
            _onInteract.performed += InteractAction;
        }

        private void MoveAction(InputAction.CallbackContext context)
        {
            OnMove?.Invoke(context.ReadValue<Vector2>());
        }
        
        private void InteractAction(InputAction.CallbackContext context)
        {
            OnInteract?.Invoke();
        }
    }
}

