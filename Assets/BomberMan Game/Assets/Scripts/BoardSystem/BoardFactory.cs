using System;
using System.Collections.Generic;
using Player;
using Commons;
using GameSystem;
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
        private BoardController boardController;

        public BoardFactory(LevelScriptableObject levelScriptable, BoardController boardController)
        {
            this.levelScriptable = levelScriptable;
            height = levelScriptable.height;
            width = levelScriptable.width;
            boardMatrix = new GameObject[height,width];
            board = new GameObject("Board");
            this.boardController = boardController;
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
                    if (boardMatrix[i, j] != player)
                        boardMatrix[i, j] = SpawnSingleTile(i, j);
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
        private GameObject SpawnSingleTile(int x, int y)
        {
            int rand = UnityEngine.Random.Range(0, 100);
            GameObject obj=null;
           
            if (rand > 70)
            {
                obj = GameObject.Instantiate(levelScriptable.nonDestructibleBlock.gameObject) as GameObject;
                obj.name = "(" + x + "," + y + ")";
                obj.transform.position = new Vector3(x, y, 0);
                obj.transform.SetParent(board.transform);
            }
            else if (rand > 30 && rand <= 70)
            {
                obj = GameObject.Instantiate(levelScriptable.destructibleBlock.gameObject) as GameObject;
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
