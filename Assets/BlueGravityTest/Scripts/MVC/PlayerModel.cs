using System;
using BlueGravityTest.ScriptableObjects.Player;
using UnityEngine;

namespace BlueGravityTest.Scripts.MVC
{
    public class PlayerModel : MonoBehaviour
    {
        public Action<Vector2> OnMove;
        
        [SerializeField] private PlayerData playerData;
        [SerializeField] private PlayerController controller;
        [SerializeField] private PlayerView view;
        [SerializeField] private Rigidbody2D rb;

        private Vector2 _movementDirection;
        private float _movementSpeed;
        
        private void Awake()
        {
            Subscribe();
            InitializeData();
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
            rb.MovePosition((Vector2)transform.position + _movementDirection * (_movementSpeed * Time.deltaTime));
            OnMove?.Invoke(_movementDirection);
        }
        
        private void Subscribe()
        {
            controller.OnMove += SetMovementDirection;
            view.Subscribe(this);
        }

        private void InitializeData()
        {
            _movementSpeed = playerData.playerSpeed;
        }
    }
}
