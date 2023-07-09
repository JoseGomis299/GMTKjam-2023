using TMPro;
using UnityEngine;

public class UiController : MonoBehaviour
    {
        //[SerializeField] private TMP_Text timerText;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject gameOverMenu;

        //[SerializeField, GradientUsage(true)] private Gradient gradient;

        private bool _gameEnded;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
            
            if (MapManager.instance.aliveCharacters.Count <= 0)
            {
                _gameEnded = true;
                gameOverMenu.SetActive(true);
            }

        }
}


