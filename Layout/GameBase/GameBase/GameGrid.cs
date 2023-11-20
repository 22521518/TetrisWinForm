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
        public readonly int Column;
        public readonly int Row;
        private readonly int[,] grid;
        public int this[int r, int c]
        {
            set => grid[r, c] = value;
            get => grid[r, c];
        }

        public GameGrid(int row, int col)
        {
            this.Column = col;
            this.Row = row;
            grid = new int[row, col];
        }
        public bool IsInside(int row, int col) => (col >= 0 && col < Column && row >= 0 && row < Row);

        public bool IsEmpty(int r, int c)
        {
            return IsInside(r, c) && grid[r, c] == 0;
        }
        public bool IsRowEmpty(int r)
        {
            for (int col = 0; col < Column; col++)
                if (grid[r,col] != 0)
                    return false;
            return true;
        }
        public bool IsRowFull(int r)
        {
            for (int col = 0; col < Column; col++)
                if (grid[r, col] == 0)
                    return false;
            return true;
        }

        public void ClearRow (int r, int x = 0)
        {
            for (int c = 0; c < Column; c++)
               grid[r, c] *= x;
        }

        private void MoveRow(int r, int numberOfClearedRow)
        {
            //this one for clearing from the top to bottom which is empty
            for (int c = 0; c < Column; c++)
            {
                grid[r + numberOfClearedRow, c] = grid[r, c];
                grid[r, c] = 0;
            }
            WindowsMediaPlayer soundplayer = new WindowsMediaPlayer();
            soundplayer.URL = (".\\Assets\\Sounds\\fall.wav");
            soundplayer.settings.volume = 80;
        }
        public int MarkedFullRow()
        {
            int cleared = 0;
            for (int r = Row - 1; r >= 0; r--)
            {
                //counting number of row which is full first
                if (IsRowFull(r))
                {
                    cleared++;
                    ClearRow(r, -1);
                }
            }

            return cleared; // for calculating points 
        }
        public int ClearFullRow()
        {
            int cleared= 0;
            for(int r = Row - 1; r >= 0; r--)
            {
                //counting number of row which is full first
                if (IsRowFull(r))
                {
                    cleared++;
                    ClearRow(r);

                }
                //then clearing from the top -> down 
                else if (cleared > 0)
                {
                    MoveRow(r, cleared);
                    WindowsMediaPlayer soundplayer = new WindowsMediaPlayer();
                    soundplayer.URL = (".\\Assets\\Sounds\\clear.wav");
                    soundplayer.settings.volume = 80;
                }
            }

            return cleared; // for calculating points 
        }
    }
}
