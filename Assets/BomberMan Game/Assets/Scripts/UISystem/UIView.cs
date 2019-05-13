using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;

namespace UISystem
{
    public class UIView : MonoBehaviour
    {        
        [SerializeField]
        private TextMeshProUGUI scoreText;
        [SerializeField]
        private GameOverPanelView gameOverPanel;
        private int score = 0;
        private void Start()
        {
            gameOverPanel.gameObject.SetActive(false);
            scoreText.text = "Score : 0";
        }

        public void ShowGameOverScreen(string message)
        {
            gameOverPanel.SetMessage(message);
            gameOverPanel.gameObject.SetActive(true);
        }

        public void UpdateScore()
        {
            score += 10;
            scoreText.text = "Score : " + score; 
        }
    }
}