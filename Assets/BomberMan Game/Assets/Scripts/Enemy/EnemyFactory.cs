using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyFactory
    {
        private int count;
        private EnemyView enemyPrefab;
        private List<EnemyView> enemies = new List<EnemyView>();
        public EnemyFactory(EnemyView enemyPrefab, int count)
        {
            this.count = count;
            this.enemyPrefab = enemyPrefab;
            SpawnEnemies();
        }

        private void SpawnEnemies()
        {
            //for (int i = 0; i < count; i++)
            //{
            //    int rand= UnityEngine.Random.Range(0, )
            //}
        }

        public List<EnemyView> GetEnemyList()
        {
            return enemies;
        }
    }
}
