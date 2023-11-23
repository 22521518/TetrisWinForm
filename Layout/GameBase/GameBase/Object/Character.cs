using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameBase.Object
{
    public class Character
    {
        // every steps will move the body[1], if entire body in a block body[0] = body[1]
        // [3] will move first which has the same column with [1], in turn [0].Row == [2].Row
        // every jump will move body[2], body[3] - involved when body[1] exist, if body in 1 row --> merge [0] = [2],,,[1] = [3] 
        public Position[] Body = new Position[4];
        readonly int ratio = 4;
        int steps = 0, jumpSteps = 0;
        public int Steps { get => steps; }
        public int JumpSteps { get => jumpSteps; }
        public bool isLanded = true;
        bool isleft;
        public bool isLeft { get => isleft; }
        public bool Hold { set; get; }
        public Character(int x = 21, int y = 1)
        {
            Hold = false;
            isleft = true;
            isLanded = true;
            Body[0] = Body[1] = Body[2] = Body[3] = new Position(x, y);
        }
        public Character(Character other)
        {
            for (int i = 0; i < 4; i++)
                this.Body[i] = new Position(other.Body[i]);

            this.isLanded = other.isLanded;
        }
        public IEnumerable<Position> PositionInTiles()
        {
            foreach (Position p in Body)
            {
                yield return new Position(p.Row, p.Column);
            }
        }
        public void MoveForward(int x = 1)
        {
            Body[0].Row += x / 4;
            if (jumpSteps != 0)
                Body[2].Row += x / 4;
            if ((steps + x) % ratio == 0 && steps + x != 0)
            {
                if (jumpSteps != 0) // when in 2 rows
                {
                    Body[2] = Body[3];
                    Body[0] = Body[1];
                }
                else
                {
                    Body[2] = Body[3] = Body[0] = Body[1];
                }
                steps = 0;
                return;
            }
            else if (steps == 0)
            {
                int next = x >= 0 ? 1 : -1;
                Body[3] = new Position(Body[2].Row, Body[2].Column + next);
                Body[1] = new Position(Body[0].Row, Body[0].Column + next);
            }

            steps = (steps + x);

            if (steps == 0)
            {
                Body[3] = Body[2];
                Body[1] = Body[0];
            }

        }
        public void MoveBackward() //When hit your face into the wall
        {
            if (jumpSteps != 0)
            {
                Body[3] = Body[2];
                Body[1] = Body[0];
            }
            else
            {
                Body[3] = Body[2] = Body[1] = Body[0];
            }

            steps = 0;
        }
        public void Jump(int y)
        {
            isLanded = false;
            if ((jumpSteps + y) % ratio == 0 && jumpSteps + y != 0)
            {
                if (steps != 0) // when in 2 rows
                {
                    Body[1] = Body[3];
                    Body[0] = Body[2];
                }
                else
                {
                    Body[1] = Body[3] = Body[0] = Body[2];
                }
                jumpSteps = 0;
                return;
            }
            else if (jumpSteps == 0)
            {
                int next = y >= 0 ? 1 : -1;
                Body[3] = new Position(Body[1].Row + next, Body[1].Column);
                Body[2] = new Position(Body[0].Row + next, Body[0].Column);
            }

            jumpSteps = jumpSteps + y;

            if (jumpSteps == 0)
            {
                Body[3] = Body[1];
                Body[2] = Body[0];
            }
        }
        public void Landed()
        {
            isLanded = true;
            jumpSteps = 0;
            if (steps != 0)
                Body[3] = Body[1];
            Body[2] = Body[0];
        }
        public void TurnLeft(bool dir)
        {
            isleft = dir;
        }
    }
}
