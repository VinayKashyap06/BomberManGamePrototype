using System;
using System.Collections.Generic;
using UnityEngine;
using Commons;
using Board;
using Player;
using Bomb;
using UISystem;
using Enemy;

namespace GameSystem
{
    public class GameService :SingletonBase<GameService>
    {
        public LevelScriptableObject levelScriptableObject;
        private BoardController boardController;
        private  PlayerController playerController;
        private BombController bombController;
        private EnemyController enemyController;

       

        private UIService uIService;

        public Action<Vector3> OnBombDestroyed;
        public Action<EnemyView> OnEnemyKilled;
        public Action OnPlayerKilled;

        

        public Action OnGameWon;    
        public Action ResetEverything;
        public Action<Vector3> MoveToNewPos;
        protected override void OnInitialize()
        {
            base.OnInitialize();
            playerController = new PlayerController();
            enemyController = new EnemyController();
            bombController = new BombController(levelScriptableObject.bombPrefab,levelScriptableObject.bombLife);
            boardController = new BoardController(levelScriptableObject);
            uIService = new UIService();            
            uIService.OnStart();
        }
        public void AddBombToMatrix(int x, int y,GameObject bomb)
        {
            boardController.AddBombToMatrix(x,y,bomb);
            
        }
        public void InvokeGameWon()
        {
            OnGameWon?.Invoke();
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
        public void InvokeEnemyKilled(EnemyView enemyView)
        {
            OnEnemyKilled?.Invoke(enemyView);
        }
        public void InvokePlayerKilled()
        {
            OnPlayerKilled?.Invoke();
        }
        public void AddEnemyToList(EnemyView enemyView)
        {
            enemyController.AddEnemiesToList(enemyView);
        }
        public Vector3 FindNewPosition(Vector3 position,GameObject enemy)
        {
            Vector3 newPos = boardController.GetNewPosition(position,enemy);           
            return newPos;
        }
        public bool IsPlayerPresent(int x, int y)
        {
            return playerController.IsPlayerPresent(x,y);
        }
        public void InvokeGameReset()
        {
            ResetEverything?.Invoke();
        }
    }
}
