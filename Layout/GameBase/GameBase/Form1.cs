using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameBase.Block;
using System.Threading;
using System.Reflection.Emit;
using WMPLib;

namespace GameBase
{
    public partial class Form1 : Form
    {
        //O GACH
        Image[] TileImage = new Image[]
        {
            Image.FromFile(".\\Assets\\theme1\\emptytile.png"),
            Image.FromFile(".\\Assets\\theme1\\i.png"),
            Image.FromFile(".\\Assets\\theme1\\j.png"),
            Image.FromFile(".\\Assets\\theme1\\l.png"),
            Image.FromFile(".\\Assets\\theme1\\o.png"),
            Image.FromFile(".\\Assets\\theme1\\s.png"),
            Image.FromFile(".\\Assets\\theme1\\t.png"),
            Image.FromFile(".\\Assets\\theme1\\z.png"),
        };

        //Gach duoi day
        Bitmap[] TileImageBlur = new Bitmap[]
        {
            new Bitmap(".\\Assets\\theme1\\emptytile.png"),
            new Bitmap(".\\Assets\\theme1\\i.png"),
            new Bitmap(".\\Assets\\theme1\\j.png"),
            new Bitmap(".\\Assets\\theme1\\l.png"),
            new Bitmap(".\\Assets\\theme1\\o.png"),
            new Bitmap(".\\Assets\\theme1\\s.png"),
            new Bitmap(".\\Assets\\theme1\\t.png"),
            new Bitmap(".\\Assets\\theme1\\z.png"),

        };

        Image[] FullBlock = new Image[]
        {
            Image.FromFile(".\\Assets\\theme1\\blockempty.png"),
            Image.FromFile(".\\Assets\\theme1\\iblock.png"),
            Image.FromFile(".\\Assets\\theme1\\jblock.png"),
            Image.FromFile(".\\Assets\\theme1\\lblock.png"),
            Image.FromFile(".\\Assets\\theme1\\oblock.png"),
            Image.FromFile(".\\Assets\\theme1\\sblock.png"),
            Image.FromFile(".\\Assets\\theme1\\tblock.png"),
            Image.FromFile(".\\Assets\\theme1\\zblock.png"),
        };
        void MakeBlur()
        {
            foreach (Bitmap pic in TileImageBlur)
            {
                for (int w = 0; w < pic.Width; w++)
                    for (int h = 0; h < pic.Height; h++)
                    {
                        Color c = pic.GetPixel(w, h);
                        Color newC = Color.FromArgb(50, c);
                        pic.SetPixel(w, h, newC);
                    }
            }
        }
        System.Windows.Forms.PictureBox[,] canvas;
        int cellSize = 32;
        GameState GameState;
        public Form1()
        {
            InitializeComponent();
            SetUpCanvas(22, 12);
            canvas[3, 4].Image = TileImage[4];
            GameState = new GameState();
            Draw(GameState);
        }

        void SetUpCanvas(int Row, int Column) //GameGrid grid
        {
            canvas = new System.Windows.Forms.PictureBox[Row, Column];
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Column; j++)
                {
                    canvas[i, j] = new System.Windows.Forms.PictureBox();
                    canvas[i, j].Location = new System.Drawing.Point(32 + cellSize * j, cellSize * (i - 2));
                    canvas[i, j].Size = new System.Drawing.Size(cellSize, cellSize);
                    canvas[i, j].BackColor = Color.Transparent;
                    canvas[i, j].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;


                    if (i > 1)
                        this.Controls.Add(canvas[i, j]);
                }
            }
            MakeBlur();
   

        }
        void DrawGrid(GameGrid grid, bool blink = false)
        {
            for (int i = 0; i < grid.Row; i++)
                for (int j = 0; j < grid.Column; j++)
                {
                    try
                    {
                        int id = grid[i, j];
                        if (id > 0)
                           canvas[i, j].Image = TileImage[id];
                        else if (!blink && id == 0)
                           canvas[i, j].Image = TileImage[id];
                        else
                            canvas[i, j].Image = TileImageBlur[-id];
                    }
                    catch { MessageBox.Show(grid[i, j].ToString()); }

                }
        }
        void DrawBlock(Block.Block block)
        {
            foreach (Position p in block.PositionInTiles())
                canvas[p.Row, p.Column].Image = TileImage[block.Id];
        }
        void DrawGhostBlock(Block.Block block, bool status = true)
        {
            if (!status)
                return;
            int drop = GameState.BlockDropDistance();
            foreach (Position p in block.PositionInTiles())
            {
                canvas[p.Row + drop, p.Column].Image = TileImageBlur[block.Id];
            }


        }
        private void DrawNextBlock(QueueBlock queue)
        {
            Block.Block next = queue.nextBlock;
            this.pictureBox1.Image = FullBlock[next.Id];
        }
        bool ghost = true;
        void Draw(GameState gamestate, bool block = false)
        {
            DrawGrid(gamestate.gameGrid, block);
            DrawGhostBlock(gamestate.CurrentBlock, ghost);
            DrawBlock(gamestate.CurrentBlock);
            DrawNextBlock(gamestate.queue);
            label1.Text = gamestate.Score.ToString();
            label1.TextAlign = ContentAlignment.MiddleRight;
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(".\\Assets\\theme1\\font.ttf");
            foreach (Control c in this.Controls)
            {
                c.Font = new Font(pfc.Families[0], 17, FontStyle.Regular);
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (GameState.GameOver)
            {
                DownBlock.Stop();
                MessageBox.Show("LOSER");
                GameState = new GameState();
                DownBlock.Start();
            }
            else
            {
                if (this.DelayKey.Enabled)
                    return;
                switch (e.KeyCode)
                {
                    case Keys.Left:
                    case Keys.A:
                        if (GameState.MoveLeft())
                            this.PlaceBlock.Enabled = false;
                        break;
                    case Keys.Right:
                    case Keys.D:
                        if (GameState.MoveRight())
                            this.PlaceBlock.Enabled = false;
                        break;
                    case Keys.Down:
                    case Keys.S:
                        GameState.MoveDown();
                        break;
                    case Keys.Up:
                    case Keys.W:
                        if (GameState.BlockRotate90())
                            this.PlaceBlock.Enabled = false;
                        break;
                    case Keys.Space:
                        int row = GameState.Drop();
                        if (row >= 1)
                        {
                            blink = row >= 4 ? true : false;
                            this.DownBlock.Enabled = false;
                            this.Clear.Enabled = true;
                        }    
                        break;
                    default:
                        return;
                }
                this.DelayKey.Enabled = true;
            }

            Draw(GameState);
            this.Invalidate();
        }
        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            DelayKey.Enabled = ++i > 1 ? false : true;
            i = DelayKey.Enabled ? i : 0;
        }
        private void DownBlock_Tick(object sender, EventArgs e)
        {
            if (GameState.GameOver)
            {
                DownBlock.Stop();
                MessageBox.Show("LOSER");
                GameState = new GameState();
                DownBlock.Start();
            }
            else
                if (!GameState.MoveDown())
                PlaceBlock.Enabled = true;

            Draw(GameState);
            this.Invalidate();
        }


        private void timer3_Tick(object sender, EventArgs e)
        {
            int row = GameState.PlaceBlock();
            if (row > 0)
            {
                DownBlock.Enabled = false;
                Clear.Enabled = true;
                blink = row >= 4 ? true : false;
            }    
            PlaceBlock.Enabled = false;
        }

        int tick = 0;
        bool blink;
        bool blinkblink;
        private void Clear_Tick(object sender, EventArgs e)
        {
            if (tick >= 5)
            {
                tick = 0;
                GameState.ClearRow();
                Clear.Enabled = false;
                DownBlock.Enabled = true;
                blinkblink = false;
            }
            else
            {
                if (blink)
                    blinkblink = !blinkblink;
                GameState.gameGrid.MarkedFullRow();
                tick++;
            }
            Draw(GameState, blinkblink);
            this.Invalidate();
        }
    }
}
