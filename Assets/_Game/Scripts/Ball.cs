using System;
using UnityEngine;

namespace Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 6f;

        private Rigidbody _rb;
        private SphereCollider _collider;
        private Vector3 _targetPoint;

        public bool CanMove { get; set; }

        public Action<Collision> OnCollision;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.useGravity = false;
            _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

            _collider = GetComponent<SphereCollider>();
        }

        private void FixedUpdate()
        {
            if (!CanMove) return;

            Debug.DrawLine(transform.position, _targetPoint, Color.red);

            var direction = (_targetPoint - transform.position).normalized;
            _rb.MovePosition(transform.position + direction * _moveSpeed * Time.fixedDeltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Collision with: " + collision.gameObject.name);
            OnCollision?.Invoke(collision);
        }

        public void SetMoveEndPoint(Vector3 targetPoint)
        {
            _targetPoint = targetPoint;
        }
    }
}
