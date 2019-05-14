using System;
using UnityEngine;
using System.Collections.Generic;
using GameSystem;
using Enemy;
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
        private Vector3 positionToIgnore;

        public BoardController(LevelScriptableObject levelScriptableObject)
        {
            this.levelScriptable = levelScriptableObject;
            boardMatrix = new GameObject[levelScriptableObject.height, levelScriptableObject.width];
            height = levelScriptableObject.height;
            width = levelScriptableObject.width;
            bombRange = levelScriptable.bombRange;
            cam = Camera.main;
            cam.transform.position = new Vector3(width / 2, width / 2, -5);

            boardFactory = new BoardFactory(levelScriptable);
            boardMatrix = boardFactory.GetBoardMatrix(true);
            GameService.Instance.OnBombDestroyed += OnBombDestroyed;
            GameService.Instance.ResetEverything += ResetGame;
        }

        private void ResetGame()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if(boardMatrix[i,j])
                        GameObject.Destroy(boardMatrix[i, j]);
                }
            }
            boardMatrix = null;            
            boardMatrix = boardFactory.GetBoardMatrix(false);
        }

        public Vector3 GetNewPosition(Vector3 position, GameObject enemy)
        {
            Vector3 newPos = position;
            int x = Mathf.RoundToInt(position.x);
            int y = Mathf.RoundToInt(position.y);
            List<Vector3> possiblePositions = new List<Vector3>();
            int iterator = 1;

            //right
            if (x + iterator < height)
            {
                if (IsEnemyMovable(boardMatrix[x + iterator, y]))
                {
                    Vector3 positionToAdd = new Vector3(x+iterator, y, 0);
                    if (positionToIgnore != positionToAdd)
                        possiblePositions.Add(positionToAdd);
                }
            }
            //left
            if (x - iterator >= 0)
            {
                if (IsEnemyMovable(boardMatrix[x - iterator, y]))
                {
                    Vector3 positionToAdd = new Vector3(x-iterator, y, 0);
                    if (positionToIgnore != positionToAdd)
                        possiblePositions.Add(positionToAdd);
                }
            }
            //up
            if (y + iterator < width)
            {
                if (IsEnemyMovable(boardMatrix[x, y + iterator]))
                {
                    Vector3 positionToAdd = new Vector3(x, y + iterator, 0);
                    if (positionToIgnore != positionToAdd)
                        possiblePositions.Add(positionToAdd);
                }
            }
            //down
            if (y - iterator >= 0)
            {
                //Debug.Log("checking down");
                if (IsEnemyMovable(boardMatrix[x, y - iterator]))
                {
                    Vector3 positionToAdd = new Vector3(x, y - iterator, 0);
                    if (positionToIgnore != positionToAdd)
                        possiblePositions.Add(positionToAdd);

                }
            }

            if (possiblePositions.Count > 0)
            {
                int rand = UnityEngine.Random.Range(0, possiblePositions.Count);
                newPos = possiblePositions[rand];
                Debug.Log("new possiblePosition for enemy" + newPos, boardMatrix[x, y]);
                boardMatrix[x, y] = null;
                boardMatrix[(int)newPos.x, (int)newPos.y] = enemy;
            }
            possiblePositions.Clear();
            return newPos;
        }
        public void AddBombToMatrix(int x, int y, GameObject bomb)
        {
            boardMatrix[x, y] = bomb;
            positionToIgnore = new Vector3(x, y, 0);
        }

        private bool IsEnemyMovable(GameObject boardValue)
        {
            return (boardValue == null || boardValue.GetComponent<EnemyView>());
        }

        private async void OnBombDestroyed(Vector3 position)
        {
            Debug.Log("Bomb position>>>" + position);
            GameObject explosion = GameObject.Instantiate(levelScriptable.explosion.gameObject, position, Quaternion.identity);
            boardMatrix[(int)position.x, (int)position.y] = explosion;
            CheckForDestructibleTiles(position);
            await new WaitForSeconds(1);
            GameObject.Destroy(explosion);
            boardMatrix[(int)position.x, (int)position.y] = null;
            positionToIgnore = new Vector3(-1, -1, -1);
        }

        private void CheckForDestructibleTiles(Vector3 position)
        {
            int x = (int)position.x;
            int y = (int)position.y;
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
                    if (IsDestructible(boardMatrix[x + iterator, y]) || IsEnemy(boardMatrix[x + iterator, y]))
                    {
                        GameObject.Destroy(boardMatrix[x + iterator, y]);
                        boardMatrix[x + iterator, y] = null;
                    }
                    if (IsPlayerPresent(x+iterator, y))
                    {
                        GameService.Instance.InvokePlayerKilled();
                    }
                }
                //left
                if (x - iterator >= 0)
                {
                    if (IsDestructible(boardMatrix[x - iterator, y]) || IsEnemy(boardMatrix[x - iterator, y]))
                    {
                        GameObject.Destroy(boardMatrix[x - iterator, y]);
                        boardMatrix[x - iterator, y] = null;
                    }
                    if (IsPlayerPresent(x-iterator, y))
                    {
                        GameService.Instance.InvokePlayerKilled();
                    }
                }
                //up
                if (y + iterator < width)
                {
                    if (IsDestructible(boardMatrix[x, y + iterator]) || IsEnemy(boardMatrix[x, y + iterator]))
                    {
                        GameObject.Destroy(boardMatrix[x, y + iterator]);
                        boardMatrix[x, y + iterator] = null;
                    }
                    if (IsPlayerPresent(x, y + iterator))
                    {
                        GameService.Instance.InvokePlayerKilled();
                    }
                }
                //down
                if (y - iterator >= 0)
                {
                    //Debug.Log("checking down");
                    if (IsDestructible(boardMatrix[x, y - iterator]) || IsEnemy(boardMatrix[x, y - iterator]))
                    {
                        GameObject.Destroy(boardMatrix[x, y - iterator]);
                        boardMatrix[x, y - iterator] = null;
                    }
                    if (IsPlayerPresent(x, y - iterator))
                    {
                        GameService.Instance.InvokePlayerKilled();
                    }
                }
                iterator++;
            }
        }

        private bool IsPlayerPresent(int x, int y)
        {
            return GameService.Instance.IsPlayerPresent(x, y);
        }

        private bool IsEnemy(GameObject enemy)
        {
            if (enemy == null)
                return false;
            if (enemy.GetComponent<EnemyView>())
            {
                GameService.Instance.InvokeEnemyKilled(enemy.GetComponent<EnemyView>());
                return true;
            }
            else
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
