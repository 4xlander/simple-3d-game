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

        private void Update()
        {
            _spawnTime -= Time.deltaTime;

            if (_spawnTime <= 0)
            {
                SpawnEnemy();
                _spawnTime = _spawnDelay;
            }
        }

        private void SpawnEnemy()
        {
            var spawnPoint = GetSpawnPoint();
            if (spawnPoint == null) return;

            Instantiate(_enemyPrefab, spawnPoint);
        }

        private Transform GetSpawnPoint()
        {
            if (_spawnPoints.Length == 0)
            {
                Debug.LogError("Spawn point is missing");
                return null;
            }

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
