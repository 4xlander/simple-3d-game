using UnityEngine;

namespace Game
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform _enemyPrefab;
        [SerializeField] private float _spawnDelay = 2f;
        [SerializeField] private Transform[] _spawnPoints;

        private float _spawnTime;
        private int _lastSpawPointIndex;
        private bool _isSpawning;

        private void Start()
        {
            GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        }

        private void Update()
        {
            if (!_isSpawning) return;

            _spawnTime -= Time.deltaTime;

            if (_spawnTime <= 0)
            {
                SpawnEnemy();
                _spawnTime = _spawnDelay;
            }
        }

        private void GameManager_OnStateChanged()
        {
            if (GameManager.Instance.IsGamePlaying())
                _isSpawning = true;
            else
                _isSpawning = false;
        }

        private void SpawnEnemy()
        {
            var spawnPoint = GetSpawnPoint();
            if (spawnPoint == null) return;

            Instantiate(_enemyPrefab, spawnPoint);
        }

        private Transform GetSpawnPoint()
        {
            int spawnPointIndex;
            do
            {
                spawnPointIndex = Random.Range(0, _spawnPoints.Length);
            } while (spawnPointIndex == _lastSpawPointIndex);

            _lastSpawPointIndex = spawnPointIndex;
            return _spawnPoints[_lastSpawPointIndex];
        }
    }
}
