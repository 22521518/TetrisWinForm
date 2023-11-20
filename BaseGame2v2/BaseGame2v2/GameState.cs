using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SurvivalPvP.Object;

namespace SurvivalPvP
{
    public class GameState
    {
        public Character Player { get; }
        public GameGrid GameGrid { get; }
        public bool PlayerLose { get; private set; }
        public GameState()
        {
            BQueue = new QueueBlock();
            currentBlock = BQueue.GetBlock();
            Player = new Character();
            GameGrid = new GameGrid();
        }
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
            return (!(GameGrid.isRowEmpty(0) && GameGrid.isRowEmpty(1)));
        }
        // ---------------- CHARACTER ---------------- //
        private bool isPositionLegit()
        {
            foreach (Position p in Player.PositionInTiles())
                if (!GameGrid.isEmpty(p.Row, p.Column))
                    return false;
            return true;
        }
        public void MoveLeft()
        {
            Player.TurnLeft(true);
            if (HitYourHead(-1) && Player.Steps == 0)
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
        public void MoveRight()
        {
            Player.TurnLeft(false);
            if (HitYourHead(1) && Player.Steps == 0)
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

        private bool HitYourHead(int c = 0)
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

            return !(GameGrid.isEmpty(RowAbove2, ColumnAbove2 + c)) || !(GameGrid.isEmpty(RowAbove3, ColumnAbove3 + c));
        }
        public bool Jump(int g = 1)
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
            if (HitYourHead() && Math.Abs(Player.JumpSteps) > 1)
                return false;
            return true;
        }
        public bool isOnland()
        {
            if (Player.Body[0].Row == GameGrid.Row - 1)
                return true;

            int RowBeneath, ColumnBeneath;
            int RowBeneath1, ColumnBeneath1;
            RowBeneath = Player.Body[0].Row + 1;
            ColumnBeneath = Player.Body[0].Column;
            RowBeneath1 = Player.Body[1].Row + 1;
            ColumnBeneath1 = Player.Body[1].Column;

            return !GameGrid.isEmpty(RowBeneath, ColumnBeneath) || !GameGrid.isEmpty(RowBeneath1, ColumnBeneath1);
        }
        public bool Fall(int g)
        {
            if (isOnland())
            {
                Player.Landed();
                return false;
            }
            Player.Jump(g);
            return true;
        }
        public void TakeBlock()
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
            if (GameGrid.isEmpty(Player.Body[1].Row, Player.Body[1].Column + block))
                return;
            GameGrid[Player.Body[1].Row, Player.Body[1].Column + block] = 0;
            Player.Hold = true;
        }
        public void PutBlock()
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
            if (!GameGrid.isEmpty(Player.Body[1].Row, Player.Body[1].Column + block))
                return;
            foreach (Position p in currentBlock.PositionInTiles())
                if (p.Row == Player.Body[1].Row && p.Column == Player.Body[1].Column + block)
                    return;
            GameGrid[Player.Body[1].Row, Player.Body[1].Column + block] = 1;
            Player.Hold = false;
        }
        // ---------------- BLOCK ---------------- //
        public QueueBlock BQueue { get; }
        private Block currentBlock;
        public Block CurrentBlock
        {
            get => currentBlock;
            private set
            {
                currentBlock = value;
                currentBlock.Reset();
            }
        }
        private bool isBlockLegit()
        {
            foreach (Position p in currentBlock.PositionInTiles())
            {
                if (!GameGrid.isEmpty(p.Row, p.Column))
                    return false;
            }
            return true;
        }
        public void PlaceBlock()
        {
            foreach (Position p in currentBlock.PositionInTiles())
                GameGrid[p.Row, p.Column] = CurrentBlock.Id;
            GameGrid.ClearFullRow();
            if (isGameOver())
            {
                return;
            }
            else
                currentBlock = BQueue.GetBlock();
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
        public bool MoveBlockLeft()
        {
            currentBlock.Move(0, -1);
            if (!isBlockLegit())
            {
                currentBlock.Move(0, 1);
                return false;
            }
            return true;
        }
        public bool MoveBlockRight()
        {
            currentBlock.Move(0, 1);
            if (!isBlockLegit())
            {
                currentBlock.Move(0, -1);
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
            return true;
        }
        public void BlockRotate270()
        {
            currentBlock.Rotate270();
            if (!isBlockLegit())
                currentBlock.Rotate90();
        }
    }
}
