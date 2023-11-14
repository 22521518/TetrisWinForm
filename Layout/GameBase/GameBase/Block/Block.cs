using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase.Block
{
    public class Position
    {
        public int Column { get; set; }
        public int Row { get; set; }
        public Position (int row, int column)
        {
            this.Column = column;
            this.Row = row;
        }
    }

    public abstract class Block
    {
        protected abstract Position[][] Tiles { get; } // like a box containing each block (4 x 4)
                                                       // tile is a block
                                                       // this contain diffent Rotation State
        protected abstract Position startOffSet { get; } // born

        public abstract int Id { get; } // distinguish different blocks (like I, Z, L, Sqr, S, T)

        private Position offset; //position
        private int rotationState; // indexer for Tiles

        public Block ()
        {
            offset = new Position(startOffSet.Row, startOffSet.Column);
        }

        public IEnumerable<Position> PositionInTiles() // for iterating the tile
        {
            foreach (Position p in Tiles[rotationState])
            {
                yield return new Position(p.Row + offset.Row, p.Column + offset.Column);
            }
        }

        public void Rotate90()
        {
            rotationState = (rotationState + 1) % Tiles.Length;
        }

        public void Rotate270()
        {
            if (rotationState == 0)
                rotationState = Tiles.Length - 1;
            else
                rotationState--;
        }
        public void Move(int y, int x)
        {
            this.offset.Row += y;
            this.offset.Column += x;
        }

        public void Reset()
        {
            rotationState = 0;
            offset.Row = startOffSet.Row;
            offset.Column = startOffSet.Column; 
        }


    }
}
