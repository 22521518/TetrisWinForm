using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameBase.Block;
using WMPLib;

namespace GameBase
{
    public class GameState
    {
        public int Score { get; set; }
        private Block.Block currentBlock;
        public Block.Block CurrentBlock
        {
            get => currentBlock;
            private set
            {
                currentBlock = value;
                currentBlock.Reset();
            }
        }

        public GameGrid gameGrid { get; }
        public QueueBlock queue { get; }
        public bool GameOver { get; private set; }
        public GameState()
        {
            gameGrid = new GameGrid(22, 10);
            queue = new QueueBlock();
            currentBlock = queue.GetBlock();
            GameOver = false;
        }

        private bool isBlockLegit()
        {
            foreach (Position p in currentBlock.PositionInTiles())
            {
                if (!gameGrid.IsEmpty(p.Row, p.Column))
                    return false;
            }
            return true;
        }

        public bool BlockRotate90()
        {
            currentBlock.Rotate90();
            if (!isBlockLegit())
            {
                currentBlock.Rotate270();
                return false;
            }
            WindowsMediaPlayer soundplayer = new WindowsMediaPlayer();
            soundplayer.URL = (".\\Assets\\Sounds\\rotate.wav");
            soundplayer.settings.volume = 80;
            return true;
        }
        public void BlockRotate270()
        {
            currentBlock.Rotate270();
            if (!isBlockLegit())
                currentBlock.Rotate90();
            WindowsMediaPlayer soundplayer = new WindowsMediaPlayer();
            soundplayer.URL = (".\\Assets\\Sounds\\rotate.wav");
            soundplayer.settings.volume = 80;
        }

        public bool MoveLeft()
        {
            currentBlock.Move(0, -1);
            if (!isBlockLegit())
            {
                currentBlock.Move(0, 1);
                return false;
            }
            WindowsMediaPlayer soundplayer = new WindowsMediaPlayer();
            soundplayer.URL = (".\\Assets\\Sounds\\move.wav");
            soundplayer.settings.volume = 80;
            return true;
        }
        public bool MoveRight()
        {
            currentBlock.Move(0, 1);
            if (!isBlockLegit())
            {
                currentBlock.Move(0, -1);
                return false;
            }
            WindowsMediaPlayer soundplayer = new WindowsMediaPlayer();
            soundplayer.URL = (".\\Assets\\Sounds\\move.wav");
            soundplayer.settings.volume = 80;
            return true;
        }

        private bool IsGameOver() 
        {
            return !(gameGrid.IsRowEmpty(0) && gameGrid.IsRowEmpty(1));
        }
    
        public int PlaceBlock()
        {
            foreach (Position p in currentBlock.PositionInTiles())
                gameGrid[p.Row, p.Column] = CurrentBlock.Id;

            if (IsGameOver())
                GameOver = IsGameOver();
            else
            { 
                currentBlock = queue.GetBlock();
                WindowsMediaPlayer soundplayer = new WindowsMediaPlayer();
                soundplayer.URL = (".\\Assets\\Sounds\\land.wav");
                soundplayer.settings.volume = 80;
            }

            return gameGrid.MarkedFullRow();
        }
        public void ClearRow()
        {
            Score += gameGrid.ClearFullRow();
        }
        public bool MoveDown()
        {
            currentBlock.Move(1, 0);
            if (!isBlockLegit())
            {
                currentBlock.Move(-1, 0);
                return false;
            }
            return true;

        }
        int DropDistance(Position p)
        {
            int drop = 0;
            while (gameGrid.IsEmpty(p.Row + drop + 1, p.Column))
                drop++;
            return drop;
        }

        public int BlockDropDistance()
        {
            int drop = gameGrid.Row;
            foreach (Position p in currentBlock.PositionInTiles())  
                drop = System.Math.Min(drop, DropDistance(p));
            return drop;
        }

        public int Drop()
        {
            CurrentBlock.Move(BlockDropDistance(), 0);
            return PlaceBlock();
        }
    }
}
