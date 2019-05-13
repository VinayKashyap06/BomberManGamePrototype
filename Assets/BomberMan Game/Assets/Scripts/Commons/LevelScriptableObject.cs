using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Board;
using Bomb;
using Player;
using Enemy;

namespace Commons
{
    [CreateAssetMenu(fileName = "Level Settings", menuName = "Custom Objects/Level/Level Settings", order = 0)]
    public class LevelScriptableObject : ScriptableObject
    {
        public int width;
        public int height;
        public BlockView destructibleBlock;
        public BlockView nonDestructibleBlock;
        public PlayerView player;
        public EnemyView enemyPrefab;
        public SpriteRenderer explosion;
        public BombView bombPrefab;
        public int bombLife;
        public int bombRange;
        public int enemyCount;
        
    }
}