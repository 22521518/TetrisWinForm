using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SurvivalPvP
{
    public class GameGrid
    {
        public int Column { get; }
        public int Row { get; }
        private int[,] grid;
        public GameGrid(int row = 22, int col = 10) 
        {
            Column = col;
            Row = row;
            grid = new int[row, col];
        }
        public int this[int r, int c]
        {
            get => grid[r, c];
            set => grid[r, c] = value;
        }

        public bool isInside(int r, int c)
        {
            return r >= 0 && c >= 0 && r < Row && c < Column;
        }

        public bool isEmpty(int r, int c)
        {
            return isInside(r, c) && grid[r, c] == 0;
        }
        
        public bool isRowEmpty(int r)
        {
            for (int c = 0; c < Column; c++) 
            {
                if (grid[r, c] != 0)
                    return false;
            }
            return true;
        }
        private bool isRowFull(int r)
        {
            for (int c = 0; c < Column; c++)
                if (grid[r, c] == 0)
                    return false;
            return true;
        }
        private void MoveRow(int r, int numberOfRows)
        {
            for(int  c = 0; c < Column; c++)
            {
                try
                {
                    grid[r + numberOfRows, c] = grid[r, c];
                    grid[r, c] = 0;
                }
                catch
                {
                    Console.WriteLine($"?????{r}, {c}\n\n");
                    Console.WriteLine(this.ToString());
                }
            }
        }
        private void ClearRow(int r)
        {
            for (int c = 0; c < Column; c++)
                grid[r, c] = 0;
        }
        public int ClearFullRow()
        {
            int rowNeedToClear = 0;
            for (int r = Row - 1; r >= 0; r--)
            {
                if (isRowFull(r))
                {
                    rowNeedToClear++;
                    ClearRow(r);
                }
                else if (rowNeedToClear > 0)
                {
                     MoveRow(r, rowNeedToClear);
                }
            }
            return rowNeedToClear;
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
