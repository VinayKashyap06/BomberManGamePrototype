using System;
using UnityEngine;
using System.Collections.Generic;
using GameSystem;
using Commons;

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
        private int bombRange;

        public BoardController(LevelScriptableObject levelScriptableObject)
        {
            this.levelScriptable = levelScriptableObject;
            boardMatrix = new GameObject[levelScriptableObject.height, levelScriptableObject.width];
            height = levelScriptableObject.height;
            width = levelScriptableObject.width;
            bombRange = levelScriptable.bombRange;
            cam = Camera.main;
            cam.transform.position = new Vector3(width / 2, width/2, -5);

            boardFactory = new BoardFactory(levelScriptable);
            boardMatrix = boardFactory.GetBoardMatrix();
            GameService.Instance.OnBombDestroyed += OnBombDestroyed;
        }

        private async void OnBombDestroyed(Vector3 position)
        {
            GameObject explosion=GameObject.Instantiate(levelScriptable.explosion.gameObject, position,Quaternion.identity);
            CheckForDestructibleTiles(position);
            await new WaitForSeconds(1);
            GameObject.Destroy(explosion);
        }

        private void CheckForDestructibleTiles(Vector3 position)
        {

            int x = Mathf.RoundToInt(position.x);
            int y = Mathf.RoundToInt(position.y);
            Debug.Log("Bomb position>>> x" + x + "y" + y);

            DestroyNearbyElements(x, y);
        }

        private void DestroyNearbyElements(int x, int y)
        {
            int iterator = 1;
            while (iterator <= bombRange)
            {
                //right
                if (x + iterator < height)
                {
                    if (IsDestructible(boardMatrix[x + iterator, y]))
                    {
                        GameObject.Destroy(boardMatrix[x + iterator, y]);
                        boardMatrix[x + iterator, y] = null;
                    }
                    if (IsEnemy(boardMatrix[x + iterator, y]))
                    {

                    }
                }
                //left
                if (x - iterator >= 0)
                {
                    if (IsDestructible(boardMatrix[x - iterator, y]))
                    {
                        GameObject.Destroy(boardMatrix[x - iterator, y]);
                        boardMatrix[x - iterator, y] = null;
                    }
                    if (IsEnemy(boardMatrix[x - iterator, y]))
                    {

                    }
                }
                //up
                if (y + iterator < width)
                {
                    if (IsDestructible(boardMatrix[x, y + iterator]))
                    {
                        GameObject.Destroy(boardMatrix[x, y + iterator]);
                        boardMatrix[x, y + iterator] = null;
                    }
                    if (IsEnemy(boardMatrix[x, y + iterator]))
                    {

                    }
                }
                //down
                if (y - iterator >= 0)
                {
                    //Debug.Log("checking down");
                    if (IsDestructible(boardMatrix[x, y - iterator]))
                    {
                        GameObject.Destroy(boardMatrix[x, y - iterator]);
                        boardMatrix[x, y - iterator] = null;
                    }
                    if (IsEnemy(boardMatrix[x, y - iterator]))
                    {

                    }
                }

                iterator++;
            }
        }

        private bool IsEnemy(GameObject enemy)
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
