using System;
using System.Collections.Generic;
using UnityEngine;
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
        }

        private void OnPlayerLost()
        {
            uIView.ShowGameOverScreen("Player Lost");
        }

        private void UpdateScore()
        {
            uIView.UpdateScore();
        }
    }
}
