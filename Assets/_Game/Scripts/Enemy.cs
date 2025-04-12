using UnityEngine;

namespace Game
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Vector3 _moveDirection = Vector3.left;
        [SerializeField] private float _moveSpeed = 6f;
        [SerializeField] private float _lifetimeMax = 4f;
        [SerializeField] private Vector3 _rotationSpeed = new(30f, 45f, 60f);

        private float _lifetime;

        private void Update()
        {
            transform.position += _moveDirection.normalized * _moveSpeed * Time.deltaTime;
            transform.Rotate(_rotationSpeed * Time.deltaTime);

            _lifetime += Time.deltaTime;
            if (_lifetime >= _lifetimeMax)
            {
                Destroy(gameObject);
            }
        }
    }
}
