using System;
using System.Collections;
using System.Collections.Generic;
using BlueGravityTest.ScriptableObjects.Player;
using BlueGravityTest.Scripts.Interactables;
using BlueGravityTest.Scripts.Inventory;
using UnityEngine;

namespace BlueGravityTest.Scripts.MVC
{
    public class PlayerModel : MonoBehaviour
    {
        public Action<Vector2> OnMove;
        public PlayerWallet GetPlayerWallet() => _wallet;
        
        [SerializeField] private PlayerData playerData;
        [SerializeField] private PlayerController controller;
        [SerializeField] private PlayerView view;
        [SerializeField] private Rigidbody2D rb;

        private Vector2 _movementDirection;
        private float _movementSpeed;
        private float _playerInteractRadius;
        private bool _canMove;
        private Collider2D[] _interactablesFound = new Collider2D[5];
        private PlayerWallet _wallet;
        
        private void Awake()
        {
            Subscribe();
            Initialize();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void SetMovementDirection(Vector2 dir)
        {
            _movementDirection = dir;
        }

        private void Move()
        {
            if (!_canMove)
                return;
            rb.MovePosition((Vector2)transform.position + _movementDirection * (_movementSpeed * Time.deltaTime));
            OnMove?.Invoke(_movementDirection);
        }
        
        private void Subscribe()
        {
            controller.OnMove += SetMovementDirection;
            controller.OnInteract += Interact;
            view.Subscribe(this);
        }

        private void Initialize()
        {
            _canMove = true;
            StartCoroutine(CheckForInteractables());
            _movementSpeed = playerData.playerSpeed;
            _playerInteractRadius = playerData.playerInteractRadius;
            _wallet = new PlayerWallet(playerData.defaultGold);
        }

        public void CanMove(bool state)
        {
            _canMove = state;
            controller.enabled = false;
        }

        private void Interact()
        {
            var interactablesFound = Physics2D.OverlapCircleNonAlloc(transform.position,_playerInteractRadius, _interactablesFound);
            for (int i = 0; i < interactablesFound; i++)
            {
                if (_interactablesFound[i].TryGetComponent(out Interactable interactable))
                    interactable.Interact(this);
            }
        }
        
        private IEnumerator CheckForInteractables()
        {
            while (true)
            {
                for (int i = 0; i < _interactablesFound.Length; i++)
                {
                    if (_interactablesFound[i] == null)
                        continue;
                    if (_interactablesFound[i].TryGetComponent(out Interactable interactable))
                        interactable.ShowInteractFeedback(false);
                }
                
                var interactablesFound = Physics2D.OverlapCircleNonAlloc(transform.position,_playerInteractRadius, _interactablesFound);
                for (int i = 0; i < interactablesFound; i++)
                {
                    if (_interactablesFound[i].TryGetComponent(out Interactable interactable))
                        interactable.ShowInteractFeedback(true);
                }
                yield return null;
            }
        }
    }
}
