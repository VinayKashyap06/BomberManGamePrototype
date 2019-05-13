using System;
using System.Collections.Generic;
using UnityEngine;
using Commons;
using Board;
using Player;
using Bomb;
using UISystem;

namespace GameSystem
{
    public class GameService :SingletonBase<GameService>
    {
        public LevelScriptableObject levelScriptableObject;
        private BoardController boardController;
        private  PlayerController playerController;
        private BombController bombController;
        private UIService uIService;

        public Action<Vector3> OnBombDestroyed;
        public Action OnEnemyKilled;
        public Action OnPlayerKilled;
        protected override void OnInitialize()
        {
            base.OnInitialize();
            playerController = new PlayerController();
            boardController = new BoardController(levelScriptableObject);
            bombController = new BombController(levelScriptableObject.bombPrefab,levelScriptableObject.bombLife);
            uIService = new UIService();
            uIService.OnStart();
        }
        private void FixedUpdate()
        {
            playerController.OnTick();
        }

        public void SetPlayerViewRef(PlayerView playerView)
        {
            playerController.SetPlayerViewReference(playerView);
        }
        public void SpawnBomb(Vector3 position)
        {
            bombController.SpawnBomb(position);
        }
        public void InvokeBombDestroyed(Vector3 position)
        {
            OnBombDestroyed?.Invoke(position);
        }
        public void InvokeEnemyKilled()
        {
            OnEnemyKilled?.Invoke();
        }
        public void InvokePlayerKilled()
        {
            OnPlayerKilled?.Invoke();
        }
    }
}
