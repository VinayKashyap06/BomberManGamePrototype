using System;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
using GameSystem;

namespace UISystem
{
    public class UIService
    {
        private UIView uIView;   
        public void OnStart()
        {
            uIView = GameObject.FindObjectOfType<UIView>();
            GameService.Instance.OnEnemyKilled += UpdateScore;
            GameService.Instance.OnPlayerKilled += OnPlayerLost;
            GameService.Instance.OnGameWon += OnGameWon;
            GameService.Instance.ResetEverything += ResetGame;
        }

        private void ResetGame()
        {
            uIView.ResetUI();
        }

        private void OnGameWon()
        {
            Time.timeScale = 0;
            uIView.ShowGameOverScreen("You Won");
        }

        private void OnPlayerLost()
        {
            Time.timeScale = 0;
            uIView.ShowGameOverScreen("You Lost");
        }

        private void UpdateScore(EnemyView enemyView)
        {
            uIView.UpdateScore();
        }
    }
}
