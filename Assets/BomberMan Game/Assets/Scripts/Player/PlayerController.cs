using System;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

namespace Player
{
    public class PlayerController
    {
        float x;
        float y;
        private PlayerView playerView;       

        public PlayerController()
        {
            GameService.Instance.OnPlayerKilled += OnPlayerKilled;
        }

        private void OnPlayerKilled()
        {
            playerView.DestroyView();
            playerView = null;
        }

        public void OnTick()
        {
            if (playerView == null)
                return;
            y = Input.GetAxis("Vertical");
            x = Input.GetAxis("Horizontal");
            if (y > 0)
            {
                playerView.MoveUp();
            }
            else if (y < 0)
            {
                playerView.MoveDown();
            }

            if (x > 0)
            {
                playerView.MoveRight();
            }
            else if (x < 0)
            {
                playerView.MoveLeft();
            }

            if (x == 0 && y == 0)
            {
                playerView.StayIdle();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {                
                GameService.Instance.SpawnBomb(playerView.transform.position);
            }
        }
        public void SetPlayerViewReference(PlayerView playerView)
        {
            this.playerView = playerView;
        }

        public bool IsPlayerPresent(int x, int y)
        {
            if (playerView == null)
                return false;
            return (Mathf.RoundToInt(playerView.transform.position.x) == x) && (Mathf.RoundToInt(playerView.transform.position.y) == y);
        }
    }
}
