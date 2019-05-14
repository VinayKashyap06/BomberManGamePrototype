using UnityEngine;
using System.Collections;
using GameSystem;
using UnityEngine.UI;
using TMPro;

namespace UISystem
{
    public class GameOverPanelView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI messageText;
        [SerializeField]
        private Button restartButton;
        [SerializeField]
        private Button exitButton;

        private void Start()
        {
            exitButton.onClick.AddListener(() => Application.Quit());
            restartButton.onClick.AddListener(OnRestartButton);
            
        }

        private void OnRestartButton()
        {
            Time.timeScale = 1f;
            GameService.Instance.InvokeGameReset();
        }

        public void SetMessage(string message)
        {
            messageText.text = message;
        }
    }
}