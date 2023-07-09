using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

    public class GameOverMenuUI : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button quitButton;

        [SerializeField] private TMP_Text scoreText;

        [SerializeField] private AudioClip buttonSound;

        private void Start()
        {
            if (restartButton != null) restartButton.onClick.AddListener(RestartButtonAction);
            if (quitButton != null) quitButton.onClick.AddListener(HomeButtonAction);

            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            restartButton.Select();
            SetScoreTexts(BetManager.instance.GetBalance());
            Time.timeScale = 0;
        }

        public void SetScoreTexts(int score)
        {
            scoreText.text = "BALANCE: $" + score;
        }

        private void HomeButtonAction()
        {
            AudioManager.Instance.PlaySound(buttonSound);
            quitButton.enabled = false;
            SceneManager.LoadScene(0);
        }

        private void RestartButtonAction()
        {
            AudioManager.Instance.PlaySound(buttonSound);
            restartButton.enabled = false;
            SceneManager.LoadScene(1);
        }
    }

