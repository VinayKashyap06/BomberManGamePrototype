using System;
using System.Collections.Generic;
using GameSystem;
using UnityEngine;

namespace Bomb
{
    public class BombController
    {
        private BombView bombView;
        private BombView bombPrefab;
        private int bombLife;
        public BombController(BombView bombView, int bombLife)
        {
            bombPrefab = bombView;
            this.bombLife = bombLife;
        }
        public async void SpawnBomb(Vector3 position)
        {
            if (bombView != null)
                return;
            else
            {
                Vector3 pos = new Vector3(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y), position.z);
                GameObject bomb = GameObject.Instantiate(bombPrefab.gameObject,pos,Quaternion.identity);
                bombView = bomb.GetComponent<BombView>();
                GameService.Instance.AddBombToMatrix((int)pos.x, (int)pos.y, bomb);
                await new WaitForSeconds(bombLife);
                bombView = null;
                GameObject.Destroy(bomb);
            }
        }
    }
}
