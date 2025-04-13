using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Button _mainMenuButton;

        private void OnEnable()
        {
            _mainMenuButton.onClick.AddListener(MainMenuButton_OnClick);
        }

        private void OnDisable()
        {
            _mainMenuButton.onClick.RemoveListener(MainMenuButton_OnClick);
        }

        private void Start()
        {
            GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
            Hide();
        }

        private void MainMenuButton_OnClick()
        {
            GameManager.Instance.WaitToStart();
        }

        private void GameManager_OnStateChanged()
        {
            if (GameManager.Instance.IsGameOver())
            {
                var score = GameManager.Instance.GetScore();
                _scoreText.text = $"Score: {score}";
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
