using BlueGravityTest.ScriptableObjects.Player;
using UnityEngine;

namespace BlueGravityTest.Scripts.MVC
{
    public class PlayerModel : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;
        [SerializeField] private PlayerController controller;
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
        }
        
        private void Subscribe()
        {
            controller.OnMove += SetMovementDirection;
        }

        private void InitializeData()
        {
            _movementSpeed = playerData.playerSpeed;
        }
    }
}
