using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class StartMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _exitButton;

        private void OnEnable()
        {
            _startButton.onClick.AddListener(StartButton_OnClick);
            _exitButton.onClick.AddListener(ExitButton_OnClick);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(StartButton_OnClick);
            _exitButton.onClick.RemoveListener(ExitButton_OnClick);
        }

        private void Start()
        {
            GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
            Show();
        }

        private void StartButton_OnClick()
        {
            SoundManager.Instance.PlayClickSound();
            GameManager.Instance.StartGame();
        }

        private void ExitButton_OnClick()
        {
            SoundManager.Instance.PlayClickSound();
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void GameManager_OnStateChanged()
        {
            if(GameManager.Instance.IsWaitingToStart())
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
