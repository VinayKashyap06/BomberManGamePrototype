using System;
using UnityEngine;
using System.Collections.Generic;
using GameSystem;
using Commons;
using Player;

namespace Board
{
    public class BoardController
    {
        private GameObject[,] boardMatrix;
        private int height;
        private int width;
        private LevelScriptableObject levelScriptable;
        private Camera cam;
        private BoardFactory boardFactory;       

        public BoardController(LevelScriptableObject levelScriptableObject)
        {
            this.levelScriptable = levelScriptableObject;
            boardMatrix = new GameObject[levelScriptableObject.height, levelScriptableObject.width];
            height = levelScriptableObject.height;
            width = levelScriptableObject.width;
            
            cam = Camera.main;
            cam.transform.position = new Vector3(width / 2, width/2, -5);

            boardFactory = new BoardFactory(levelScriptable, this);
            boardMatrix = boardFactory.GetBoardMatrix();
            GameService.Instance.OnBombDestroyed += OnBombDestroyed;
        }

        private async void OnBombDestroyed(Vector3 position)
        {
            CheckForDestructibleTiles(position);
            GameObject explosion=GameObject.Instantiate(levelScriptable.explosion.gameObject, position,Quaternion.identity);
            await new WaitForSeconds(1);
            GameObject.Destroy(explosion);
        }

        private void CheckForDestructibleTiles(Vector3 position)
        {
               
            int x = Mathf.RoundToInt(position.x);
            int y = Mathf.RoundToInt(position.y);
            Debug.Log("Bomb position;> x" + x + "y" + y);

            //right
            if (x + 1 < height)
            {
                if (IsDestructible(boardMatrix[x + 1, y]))
                {

                    GameObject.Destroy(boardMatrix[x+1, y]);
                    boardMatrix[x+1, y] = null;
                }
                if (IsEnemy(boardMatrix[x+1, y]))
                {

                }
            }
            //left
            if (x - 1 >= 0)
            {
                if (IsDestructible(boardMatrix[x - 1, y]))
                {
                    GameObject.Destroy(boardMatrix[x-1, y]);
                    boardMatrix[x-1, y] = null;
                }
                if (IsEnemy(boardMatrix[x-1, y]))
                {

                }
            }
            //up
            if (y + 1 < width)
            {
                if (IsDestructible(boardMatrix[x , y+1]))
                {
                    GameObject.Destroy(boardMatrix[x, y+1]);
                    boardMatrix[x, y+1] = null;
                }
                if (IsEnemy(boardMatrix[x , y+1]))
                {

                }
            }
            //down
            if (y - 1 >=0)
            {
                if (IsDestructible(boardMatrix[x, y-1]))
                {

                    GameObject.Destroy(boardMatrix[x, y-1]);
                    boardMatrix[x, y-1] = null;
                }
                if (IsEnemy(boardMatrix[x, y-1]))
                {

                }
            }
        }

        private bool IsEnemy(GameObject gameObject)
        {
            return false;
        }

        private bool IsDestructible(GameObject tile)
        {
            if (tile == null)
                return false;
            if (tile.GetComponent<BlockView>())
                return tile.GetComponent<BlockView>().isDestructible;
            else
                return false;
        }
    }
}
