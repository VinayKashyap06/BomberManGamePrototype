using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
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
            restartButton.onClick.AddListener(() => SceneManager.LoadScene(0));
            
        }
        public void SetMessage(string message)
        {
            messageText.text = message;
        }
    }
}