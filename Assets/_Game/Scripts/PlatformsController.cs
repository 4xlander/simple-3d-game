using System.Linq;
using UnityEngine;

namespace Game
{
    public class PlatformsController : MonoBehaviour
    {
        [SerializeField] private Transform[] _platforms;
        [SerializeField] private Material _defaultPlatformMaterial;
        [SerializeField] private Material _targetPlatformMaterial;

        private int _nextPlatformIndex = 0;
        private int _direction = 1;
        private const int DIRECTION_MODIFIER = -1;

        private Transform _targetPlatform;

        private void Start()
        {
            GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        }

        private void GameManager_OnStateChanged()
        {
            if (GameManager.Instance.IsGamePlaying())
            {
                _nextPlatformIndex = 0;
                _direction = 1;
            }
        }

        public Transform GetNextPlatform()
        {
            _nextPlatformIndex = (_nextPlatformIndex + _direction + _platforms.Length) % _platforms.Length;
            return _platforms[_nextPlatformIndex];
        }

        public void InvertDirection()
        {
            _direction *= DIRECTION_MODIFIER;
        }

        public bool IsPlatform(Transform transform)
        {
            return _platforms.Contains(transform);
        }

        public bool IsTargetPlatform(Transform platform)
        {
            return platform == _targetPlatform;
        }

        public void SetNextTargetPlatform()
        {
            int newTargetIndex;
            do
            {
                newTargetIndex = Random.Range(0, _platforms.Length);
            }
            while (_targetPlatform == _platforms[newTargetIndex]);

            if (_targetPlatform != null)
                SetColor(_targetPlatform, _defaultPlatformMaterial);

            _targetPlatform = _platforms[newTargetIndex];
            SetColor(_targetPlatform, _targetPlatformMaterial);
        }

        private void SetColor(Transform target, Material material)
        {
            if (_targetPlatform.TryGetComponent<Renderer>(out var renderer))
                renderer.material = material;
        }
    }
}
