using System;
using System.Collections.Generic;
using Player;
using Commons;
using GameSystem;
using Enemy;
using UnityEngine;

namespace Board
{
    class BoardFactory
    {
        private LevelScriptableObject levelScriptable;
        private int height;
        private int width;
        private GameObject[,] boardMatrix;
        private GameObject board;
        private int enemiesSpawned = 0;
        private int enemyCount;
        public BoardFactory(LevelScriptableObject levelScriptable)
        {
            this.levelScriptable = levelScriptable;
            height = levelScriptable.height;
            width = levelScriptable.width;
            enemyCount = levelScriptable.enemyCount;
            boardMatrix = new GameObject[height,width];
            board = new GameObject("Board");
        }
        
        public GameObject[,] GetBoardMatrix()
        {
            SpawnBoard();
            return boardMatrix;
        }
        private void SpawnBoard()
        {
            //player parent            
            GameObject player = GameObject.Instantiate(levelScriptable.player.gameObject) as GameObject;
            boardMatrix[0, width - 1] = player;            
            player.transform.position = new Vector3(0,width-1,0);
            player.transform.SetParent(board.transform);
            GameService.Instance.SetPlayerViewRef(player.GetComponent<PlayerView>());          

            for (int i = height - 1; i >= 0; i--)
            {
                for (int j = width - 1; j >= 0; j--)
                {
                    if (j % 2 == 0 && i%2==0)
                        boardMatrix[i, j]=SpawnSingleTile(i, j,true);

                    if (boardMatrix[i, j] != player && boardMatrix[i,j]==null)
                        boardMatrix[i, j] = SpawnSingleTile(i, j,false);

                }
            }
            //Mandatory empty spots
            if (boardMatrix[0, width - 2] != null)
                GameObject.Destroy(boardMatrix[0, width - 2]);
            if (boardMatrix[1, width - 1] != null)
                GameObject.Destroy(boardMatrix[1, width - 1]);

            boardMatrix[0, width - 2] = null;
            boardMatrix[1, width - 1] = null;

            SpawnBounds();
        }
        private GameObject SpawnSingleTile(int x, int y,bool spawnNonDestructible)
        {
            int rand = UnityEngine.Random.Range(0, 100);
            GameObject obj=null;
          
            if (rand >= 50)
            {
                obj = GameObject.Instantiate(levelScriptable.destructibleBlock.gameObject) as GameObject;
                obj.name = "(" + x + "," + y + ")";
                obj.transform.position = new Vector3(x, y, 0);
                obj.transform.SetParent(board.transform);
            }
            else
            {
                if (enemiesSpawned <= enemyCount)
                {
                    obj = GameObject.Instantiate(levelScriptable.enemyPrefab.gameObject) as GameObject;
                    obj.transform.position = new Vector3(x, y, 0);
                    obj.transform.SetParent(board.transform);
                    enemiesSpawned++;
                }
            }
            if (spawnNonDestructible && obj==null)
            {
                obj = GameObject.Instantiate(levelScriptable.nonDestructibleBlock.gameObject) as GameObject;
                obj.name = "(" + x + "," + y + ")";
                obj.transform.position = new Vector3(x, y, 0);
                obj.transform.SetParent(board.transform);                
            }

            return obj;
        }

        private void SpawnBounds()
        {
            GameObject bounds = new GameObject("Bounds");
            for(int i=-1;i< width + 1; i++)
            {
                GameObject negativeYBounds= GameObject.Instantiate(levelScriptable.nonDestructibleBlock.gameObject) as GameObject;
                negativeYBounds.transform.position = new Vector3(-1, i, 0);
                negativeYBounds.transform.SetParent(bounds.transform);
            }
            for (int i=-1;i< height + 1; i++)
            {
                GameObject negativeXBounds= GameObject.Instantiate(levelScriptable.nonDestructibleBlock.gameObject) as GameObject;
                negativeXBounds.transform.position = new Vector3(i, -1, 0);
                negativeXBounds.transform.SetParent(bounds.transform);
            }
            for (int i=0;i<width+1; i++)
            {
                GameObject positiveYBounds= GameObject.Instantiate(levelScriptable.nonDestructibleBlock.gameObject) as GameObject;
                positiveYBounds.transform.position = new Vector3(height,i, 0);
                positiveYBounds.transform.SetParent(bounds.transform);
            }
            for (int i=0;i<height+1; i++)
            {
                GameObject positiveXBounds= GameObject.Instantiate(levelScriptable.nonDestructibleBlock.gameObject) as GameObject;
                positiveXBounds.transform.position = new Vector3(i,width, 0);
                positiveXBounds.transform.SetParent(bounds.transform);
            }
        }
    }
}
