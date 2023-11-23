using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace GameBase
{
    public class GameGrid
    {
        public int Column { get; }
        public int Row { get; }
        private int[,] grid;
        public int this[int r, int c]
        {
            get => grid[r, c];
            set => grid[r, c] = value;
        }

        public GameGrid(int row = 22, int col = 10)
        {
            Column = col;
            Row = row;
            grid = new int[row, col];
        }
        public bool IsInside(int row, int col)
        {
            return row >= 0 && col >= 0 && row < Row && col < Column;
        }

        public bool IsEmpty(int row, int col)
        {
            return IsInside(row, col) && grid[row, col] == 0;
        }
        public bool IsRowEmpty(int row)
        {
            for (int col = 0; col < Column; col++)
            {
                if (grid[row, col] != 0)
                    return false;
            }
            return true;
        }
        public bool IsRowFull(int row)
        {
            for (int col = 0; col < Column; col++)
                if (grid[row, col] == 0)
                    return false;
            return true;
        }

        public void ClearRow (int row, int x = 0)
        {
            for (int col = 0; col < Column; col++)
               grid[row, col] *= x;
        }

        private void MoveRow(int row, int numberOfClearedRow)
        {
            //this one for clearing from the top to bottom which is empty
            for (int col = 0; col < Column; col++)
            {
                try
                {
                    grid[row + numberOfClearedRow, col] = grid[row, col];
                    grid[row, col] = 0;
                }
                catch 
                {
                    Console.WriteLine($"?????{row}, {col}\n\n");
                    Console.WriteLine(this.ToString());
                }
            }
        }
        public int MarkedFullRow()
        {
            int cleared = 0;
            for (int row = Row - 1; row >= 0; row--)
            {
                //counting number of row which is full first
                if (IsRowFull(row))
                {
                    cleared++;
                    ClearRow(row, -1);
                }
            }
            return cleared; // for calculating points 
        }
        public int ClearFullRow()
        {
            int cleared= 0;
            for(int row= Row - 1; row >= 0; row--)
            {
                //counting number of row which is full first
                if (IsRowFull(row))
                {
                    cleared++;
                    ClearRow(row);

                }
                //then clearing from the top -> down 
                else if (cleared > 0)
                {
                    MoveRow(row, cleared);
                }
            }

            return cleared; // for calculating points 
        }
        public override string ToString()
        {
            string str = "";
            for (int r = 0; r < Row; r++)
            {
                for (int c = 0; c < Column; c++)
                    str += grid[r, c].ToString() + ' ';
                str += "\n";
            }
            return str;
        }
    }
}
