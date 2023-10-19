using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BlueGravityTest.Scripts.MVC
{
    public class PlayerController : MonoBehaviour
    {
        
        public Action<Vector2> OnMove;
        
        [SerializeField] private PlayerInput playerInput;
        
        private InputAction _moveAction;
        
        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (playerInput == null)
                return;

            _moveAction = playerInput.actions["Move"];
            SubscribeInputs();
        }

        private void SubscribeInputs()
        {
            _moveAction.performed += MoveAction;
        }

        private void MoveAction(InputAction.CallbackContext context)
        {
            OnMove?.Invoke(context.ReadValue<Vector2>());
        }
    }
}

