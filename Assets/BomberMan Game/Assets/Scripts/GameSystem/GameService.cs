using System;
using System.Collections.Generic;
using Commons;
using Board;

namespace GameSystem
{
    public class GameService :SingletonBase<GameService>
    {
        public LevelScriptableObject levelScriptableObject;
        private BoardController boardController;
        protected override void OnInitialize()
        {
            base.OnInitialize();
            boardController = new BoardController(levelScriptableObject);
        }
    }
}
