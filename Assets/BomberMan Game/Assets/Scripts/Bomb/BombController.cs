using System;
using System.Collections.Generic;
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
                GameObject bomb = GameObject.Instantiate(bombPrefab.gameObject,position,Quaternion.identity);
                bombView = bomb.GetComponent<BombView>();
                await new WaitForSeconds(bombLife);
                bombView = null;
                GameObject.Destroy(bomb);
            }
        }
    }
}
