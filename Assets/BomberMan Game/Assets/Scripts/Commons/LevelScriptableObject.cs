using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Board;
using Bomb;
using Player;

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
        public SpriteRenderer explosion;
        public BombView bombPrefab;
        public int bombLife;
    }
}