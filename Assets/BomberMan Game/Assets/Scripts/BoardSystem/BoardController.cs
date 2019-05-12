using System;
using UnityEngine;
using System.Collections.Generic;
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
        public BoardController(LevelScriptableObject levelScriptableObject)
        {
            this.levelScriptable = levelScriptableObject;
            boardMatrix = new GameObject[levelScriptableObject.height, levelScriptableObject.width];
            height = levelScriptableObject.height;
            width = levelScriptableObject.width;
            cam = Camera.main;
            cam.transform.position = new Vector3(width / 2, height / 2, -5);
            SpawnBoard();

        }
        private void SpawnBoard()
        {
            GameObject playerParent = new GameObject();
            playerParent.name = "Player Parent";
            playerParent.transform.position = new Vector3(0, width - 1, 0);
            boardMatrix[0, width - 1] = playerParent;
            GameObject player= GameObject.Instantiate(levelScriptable.player.gameObject) as GameObject;
            player.transform.SetParent(playerParent.transform);
            player.transform.localPosition = Vector3.zero;
            

            for (int i = height-1; i>=0; i--)
            {
                for (int j = width-1; j >=0; j--)
                {
                    if (boardMatrix[i, j] == null)
                        boardMatrix[i, j] = SpawnSingleTile(i,j);
                }
            }
        }
        private GameObject SpawnSingleTile(int x, int y)
        {
            int rand = UnityEngine.Random.Range(0, 100);
            GameObject parentObj = new GameObject();
            parentObj.name = "("+x+","+y+")";
            parentObj.transform.position = new Vector3(x, y, 0);


            return parentObj;
        }
    }
}
