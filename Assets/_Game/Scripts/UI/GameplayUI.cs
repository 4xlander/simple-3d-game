using TMPro;
using UnityEngine;

namespace Game
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        private void Start()
        {
            GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
            GameManager.Instance.OnScoreChanged += GameManager_OnScoreChanged;
            Hide();
        }

        private void GameManager_OnScoreChanged()
        {
            var score = GameManager.Instance.GetScore();
            _scoreText.text = score.ToString();
        }

        private void GameManager_OnStateChanged()
        {
            if (GameManager.Instance.IsGamePlaying())
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }
    }
}
