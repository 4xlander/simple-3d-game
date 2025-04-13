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
        private Vector3 _targetPosition;
        private Transform _collisionTransform;

        public bool CanMove { get; set; }

        public Action<Transform> OnCollision;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.useGravity = false;
            _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }

        private void FixedUpdate()
        {
            if (!CanMove) return;

            Debug.DrawLine(transform.position, _targetPosition, Color.red);

            var direction = (_targetPosition - transform.position).normalized;
            _rb.MovePosition(transform.position + direction * _moveSpeed * Time.fixedDeltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            _collisionTransform = collision.transform;
            OnCollision?.Invoke(_collisionTransform);
        }

        private void OnCollisionExit(Collision collision)
        {
            _collisionTransform = null;
        }

        public void SetTargetPosition(Transform targetTransform)
        {
            if (_collisionTransform != null && _collisionTransform.Equals(targetTransform))
            {
                OnCollision?.Invoke(_collisionTransform);
                _collisionTransform = null;
                return;
            }

            _targetPosition = CalculateEndPoint(targetTransform, transform);
        }

        private Vector3 CalculateEndPoint(Transform target, Transform ball)
        {
            var targetCollider = target.GetComponent<CapsuleCollider>();
            var ballCollider = ball.GetComponent<SphereCollider>();

            if (targetCollider == null || ballCollider == null)
            {
                Debug.LogError("The required collider is missing");
                return target.position;
            }

            var dirToBall = (ball.position - target.position).normalized;
            var perpendicular = Vector3.ProjectOnPlane(dirToBall, target.up).normalized;

            float targetRadius = targetCollider.radius * Mathf.Max(
                target.lossyScale.x,
                target.lossyScale.z);

            float ballRadius = ballCollider.radius * Mathf.Max(
                ball.lossyScale.x,
                ball.lossyScale.y,
                ball.lossyScale.z);

            float pointOffset = targetRadius + ballRadius;

            return target.position + perpendicular * pointOffset;
        }
    }
}
