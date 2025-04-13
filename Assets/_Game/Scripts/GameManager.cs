using System;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public Action OnStateChanged;
        public Action OnScoreChanged;

        private enum State
        {
            WaitingToStart,
            GamePlaying,
            GameOver,
        }

        [SerializeField] private GameInput _gameInput;
        [SerializeField] private Ball _ballPrefab;
        [SerializeField] private Transform _ballSpawnPoint;
        [SerializeField] private PlatformsController _platformsController;

        private State _state = State.WaitingToStart;
        private int _score = 0;
        private Ball _ball;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _gameInput.OnInvertDirection += GameInput_OnInvertDirection;
        }

        public void WaitToStart()
        {
            SetState(State.WaitingToStart);
        }

        public void StartGame()
        {
            _score = 0;
            OnScoreChanged?.Invoke();

            SetState(State.GamePlaying);

            _ball = Instantiate(_ballPrefab, _ballSpawnPoint);
            _ball.OnCollision += Ball_OnCollision;
            SetBallNextPosition();
            _ball.CanMove = true;

            _platformsController.SetNextTargetPlatform();
        }

        public bool IsWaitingToStart()
        {
            return _state == State.WaitingToStart;
        }

        public bool IsGamePlaying()
        {
            return _state == State.GamePlaying;
        }

        public bool IsGameOver()
        {
            return _state == State.GameOver;
        }

        public int GetScore()
        {
            return _score;
        }

        private void GameInput_OnInvertDirection()
        {
            if (!IsGamePlaying()) return;

            _platformsController.InvertDirection();
            SetBallNextPosition();
        }

        private void Ball_OnCollision(Transform collisionTransform)
        {
            if (_platformsController.IsPlatform(collisionTransform))
            {
                SetBallNextPosition();
                if (_platformsController.IsTargetPlatform(collisionTransform))
                {
                    _score++;
                    OnScoreChanged?.Invoke();
                    _platformsController.SetNextTargetPlatform();
                }
            }
            else
            {
                Destroy(_ball.gameObject);
                SetState(State.GameOver);
            }
        }

        private void SetBallNextPosition()
        {
            var nextPlatform = _platformsController.GetNextPlatform();
            _ball.SetTargetPosition(nextPlatform);
        }

        private void SetState(State newState)
        {
            _state = newState;
            OnStateChanged?.Invoke();
        }
    }
}
