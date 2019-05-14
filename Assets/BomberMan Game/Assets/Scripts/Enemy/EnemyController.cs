using System;
using System.Collections.Generic;
using GameSystem;
using UnityEngine;

namespace Enemy
{
    public class EnemyController
    {
        List<EnemyView> enemies = new List<EnemyView>();

        public EnemyController()
        {
            GameService.Instance.OnEnemyKilled += RemoveEnemiesFromList;
            GameService.Instance.ResetEverything += ResetGame;
        }

        private void ResetGame()
        {
            foreach (EnemyView item in enemies)
            {
                GameObject.Destroy(item.gameObject);
            }
            enemies.Clear();
        }

        public void AddEnemiesToList(EnemyView enemyView)
        {
            enemies.Add(enemyView);
            //Debug.Log("Enemy count after adding" + enemies.Count);
        }
        public void RemoveEnemiesFromList(EnemyView enemyView)
        {            
            enemies.Remove(enemyView);
            //Debug.Log("Enemy count after removing" + enemies.Count);
            if (enemies.Count == 0)
            {
                GameService.Instance.InvokeGameWon();
            }
        }
    }
}
