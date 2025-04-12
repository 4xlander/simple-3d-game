using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Ball _ball;
        [SerializeField] private Transform[] _targets;

        private int _currentTargetIndex = 1;
        private int _direction = 1;

        private void Start()
        {
            if (_targets.Length > 1)
            {
                _ball.OnCollision += BallController_OnCollision;
                var endPoint = CalculateEndPoint(_targets[_currentTargetIndex], _ball.transform);
                _ball.SetMoveEndPoint(endPoint);
                _ball.CanMove = true;
            }
        }

        private void BallController_OnCollision(Collision collision)
        {
            if (collision.gameObject.transform == _targets[_currentTargetIndex])
            {
                _currentTargetIndex = GetNextTargetIndex();
                var target = _targets[_currentTargetIndex];
                _ball.SetMoveEndPoint(CalculateEndPoint(target, _ball.transform));
            }
        }

        private int GetNextTargetIndex()
        {
            return (_currentTargetIndex + _direction + _targets.Length) % _targets.Length;
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
