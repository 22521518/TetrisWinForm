using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameBase.Object;
using WMPLib;

namespace GameBase
{
    public class GameState
    {
        public Character Player { get; }
        public bool PlayerLose { get; private set; }
        public int Score { get; set; }
        private Object.Block currentBlock;
        public Object.Block CurrentBlock
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
            Player = new Character();
            GameOver = false;
        }
        private bool IsPositionLegit()
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
            if (!IsPositionLegit())
            {
                currentBlock.Rotate270();
                return false;
            }
            return true;
        }
        public void BlockRotate270()
        {
            currentBlock.Rotate270();
            if (!IsPositionLegit())
                currentBlock.Rotate90();
        }
        public bool MoveLeft()
        {
            currentBlock.Move(0, -1);
            if (!IsPositionLegit())
            {
                currentBlock.Move(0, 1);
                return false;
            }
            return true;
        }
        public bool MoveRight()
        {
            currentBlock.Move(0, 1);
            if (!IsPositionLegit())
            {
                currentBlock.Move(0, -1);
                return false;
            }
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
            if (!IsPositionLegit())
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
        // ---------------- CHARACTER ---------------- //
        public bool isGameOver()
        {
            foreach (Position pos in currentBlock.PositionInTiles())
            {
                Position head1 = new Position(Player.Body[2]);
                Position head2 = new Position(Player.Body[3]);
                Position body1 = new Position(Player.Body[0]);
                Position body2 = new Position(Player.Body[1]);
                if (Player.JumpSteps == 0 || Math.Abs(Player.JumpSteps) == 3 || Player.JumpSteps == 1)
                {
                    head1.Row--;
                    head2.Row--;

                }
                if (pos == head1 || pos == head2 || pos == body1 || pos == body2)
                {
                    PlayerLose = true;
                    return true;
                }
            }
            PlayerLose = false;
            return (!(gameGrid.IsRowEmpty(0) && gameGrid.IsRowEmpty(1)));
        }
        private bool isPositionLegit()
        {
            foreach (Position p in Player.PositionInTiles())
                if (!gameGrid.IsEmpty(p.Row, p.Column))
                    return false;
            return true;
        }
        public void moveLeft()
        {
            Player.TurnLeft(true);
            if (hitYourHead(-1) && Player.Steps == 0)
                return;
            Player.MoveForward(-1);
            if (isGameOver())
            {
                PlayerLose = true;
                return;
            }
            if (!isPositionLegit())
                Player.MoveBackward();
        }
        public void moveRight()
        {
            Player.TurnLeft(false);
            if (hitYourHead(1) && Player.Steps == 0)
                return;
            Player.MoveForward(1);
            if (isGameOver())
            {
                PlayerLose = true;
                return;
            }
            if (!isPositionLegit())
                Player.MoveBackward();
        }
        private bool hitYourHead(int c = 0)
        {
            ;
            if (Player.Body[2].Row == 2)
                return true;
            int RowAbove2, ColumnAbove2;
            int RowAbove3, ColumnAbove3;

            RowAbove2 = Player.Body[2].Row - 1;
            ColumnAbove2 = Player.Body[2].Column;
            RowAbove3 = Player.Body[3].Row - 1;
            ColumnAbove3 = Player.Body[3].Column;

            return !(gameGrid.IsEmpty(RowAbove2, ColumnAbove2 + c)) || !(gameGrid.IsEmpty(RowAbove3, ColumnAbove3 + c));
        }
        public bool jump(int g = 1)
        {
            Player.Jump(-g);
            if (isGameOver())
            {
                PlayerLose = true;
                return false;
            }
            if (!isPositionLegit())
            {
                Player.Jump(g);
                Player.Landed();
                return false;
            }
            if (hitYourHead() && Math.Abs(Player.JumpSteps) > 1)
                return false;
            return true;
        }
        public bool isOnland()
        {
            if (Player.Body[0].Row == gameGrid.Row - 1)
                return true;

            int RowBeneath, ColumnBeneath;
            int RowBeneath1, ColumnBeneath1;
            RowBeneath = Player.Body[0].Row + 1;
            ColumnBeneath = Player.Body[0].Column;
            RowBeneath1 = Player.Body[1].Row + 1;
            ColumnBeneath1 = Player.Body[1].Column;

            return !gameGrid.IsEmpty(RowBeneath, ColumnBeneath) || !gameGrid.IsEmpty(RowBeneath1, ColumnBeneath1);
        }
        public bool fall(int g)
        {
            if (isOnland())
            {
                Player.Landed();
                return false;
            }
            Player.Jump(g);
            return true;
        }
        public void takeBlock()
        {
            if (Player.Hold)
                return;
            if ((Player.Steps == 1 && !Player.isLeft) || (Player.Steps == -1 && Player.isLeft))
                return;
            if (Player.Body[1].Column == 9 && !Player.isLeft)
                return;
            if (Player.Body[1].Column == 0 && Player.isLeft)
                return;
            int block = Player.isLeft ? -1 : 1;
            if ((Player.Steps > 0 && Player.isLeft) || (Player.Steps < 0 && !Player.isLeft))
                block *= 2;
            if (gameGrid.IsEmpty(Player.Body[1].Row, Player.Body[1].Column + block))
                return;
            gameGrid[Player.Body[1].Row, Player.Body[1].Column + block] = 0;
            Player.Hold = true;
        }
        public void putBlock()
        {
            if (!Player.Hold)
                return;
            if ((Player.Steps == 1 && !Player.isLeft) || (Player.Steps == -1 && Player.isLeft))
                return;
            if (Player.Body[1].Column == 9 && !Player.isLeft)
                return;
            if (Player.Body[1].Column == 0 && Player.isLeft)
                return;
            int block = Player.isLeft ? -1 : 1;
            if ((Player.Steps > 0 && Player.isLeft) || (Player.Steps < 0 && !Player.isLeft))
                block *= 2;
            if (!gameGrid.IsEmpty(Player.Body[1].Row, Player.Body[1].Column + block))
                return;
            foreach (Position p in currentBlock.PositionInTiles())
                if (p.Row == Player.Body[1].Row && p.Column == Player.Body[1].Column + block)
                    return;
            gameGrid[Player.Body[1].Row, Player.Body[1].Column + block] = 1;
            Player.Hold = false;
        }
    }
}
