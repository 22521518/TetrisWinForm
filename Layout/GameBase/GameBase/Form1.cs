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
            Image.FromFile(".\\Assets\\emptytile.png"),
            Image.FromFile(".\\Assets\\i.png"),
            Image.FromFile(".\\Assets\\j.png"),
            Image.FromFile(".\\Assets\\l.png"),
            Image.FromFile(".\\Assets\\o.png"),
            Image.FromFile(".\\Assets\\s.png"),
            Image.FromFile(".\\Assets\\t.png"),
            Image.FromFile(".\\Assets\\z.png"),
            Image.FromFile(".\\Assets\\wall.png"),
        };

        //Gach duoi day
        Bitmap[] TileImageBlur = new Bitmap[]
        {
            new Bitmap(".\\Assets\\emptytile.png"),
            new Bitmap(".\\Assets\\i.png"),
            new Bitmap(".\\Assets\\j.png"),
            new Bitmap(".\\Assets\\l.png"),
            new Bitmap(".\\Assets\\o.png"),
            new Bitmap(".\\Assets\\s.png"),
            new Bitmap(".\\Assets\\t.png"),
            new Bitmap(".\\Assets\\z.png"),

        };

        Image[] FullBlock = new Image[]
        {
            Image.FromFile(".\\Assets\\blockempty.png"),
            Image.FromFile(".\\Assets\\iblock.png"),
            Image.FromFile(".\\Assets\\jblock.png"),
            Image.FromFile(".\\Assets\\lblock.png"),
            Image.FromFile(".\\Assets\\oblock.png"),
            Image.FromFile(".\\Assets\\sblock.png"),
            Image.FromFile(".\\Assets\\tblock.png"),
            Image.FromFile(".\\Assets\\zblock.png"),
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
        int cellSize = 25;
        GameState GameState;
        public Form1()
        {
            InitializeComponent();
            SetUpCanvas(22, 12);
            canvas[3, 4].Image = TileImage[4];
            GameState = new GameState();
            Draw(GameState);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(".\\Assets\\font.ttf");
            foreach (Control c in this.Controls)
            {
                c.Font = new Font(pfc.Families[0], 15, FontStyle.Regular);
            }
        }

        void SetUpCanvas(int Row, int Column) //GameGrid grid
        {
            canvas = new System.Windows.Forms.PictureBox[Row, Column];
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Column; j++)
                {
                    canvas[i, j] = new System.Windows.Forms.PictureBox();
                    canvas[i, j].Location = new System.Drawing.Point(25 + cellSize * j, cellSize * (i - 2));
                    canvas[i, j].Size = new System.Drawing.Size(cellSize, cellSize);
                    canvas[i, j].BackColor = Color.Transparent;
                    canvas[i, j].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;


                    if (i > 1)
                        this.Controls.Add(canvas[i, j]);
                }
            }
            MakeBlur();
   

        }
        void DrawGrid(GameGrid grid)
        {
            for (int i = 0; i < grid.Row; i++)
                for (int j = 0; j < grid.Column; j++)
                {
                    try
                    {
                        int id = grid[i, j];
                        canvas[i, j].Image = TileImage[id];
                    }
                    catch { MessageBox.Show(grid[i, j].ToString()); }

                }
        }
        void DrawBlock(Block.Block block)
        {
            foreach (Position p in block.PositionInTiles())
                canvas[p.Row, p.Column].Image = TileImage[block.Id];
        }
        void DrawGhostBlock(Block.Block block)
        {
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
        void Draw(GameState gamestate)
        {
            DrawGrid(gamestate.gameGrid);
            DrawGhostBlock(gamestate.CurrentBlock);
            DrawBlock(gamestate.CurrentBlock);
            DrawNextBlock(gamestate.queue);
            label1.Text = gamestate.Score.ToString();
            label1.TextAlign = ContentAlignment.MiddleRight;

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
                if (this.timer1.Enabled)
                    return;
                switch (e.KeyCode)
                {
                    case Keys.Left:
                    case Keys.A:
                        if (GameState.MoveLeft())
                            this.timer3.Enabled = false;
                        break;
                    case Keys.Right:
                    case Keys.D:
                        if (GameState.MoveRight())
                            this.timer3.Enabled = false;
                        break;
                    case Keys.Down:
                    case Keys.S:
                        GameState.MoveDown();
                        break;
                    case Keys.Up:
                    case Keys.W:
                        if (GameState.BlockRotate90())
                            this.timer3.Enabled = false;
                        break;
                    case Keys.Space:
                        GameState.Drop();
                        this.timer3.Enabled = false;
                        break;
                    default:
                        return;
                }
                this.timer1.Enabled = true;
            }

            Draw(GameState);
            this.Invalidate();
        }
        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = ++i > 1 ? false : true;
            i = timer1.Enabled ? i : 0;
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
                timer3.Enabled = true;

            Draw(GameState);
            this.Invalidate();

        }


        private void timer3_Tick(object sender, EventArgs e)
        {
            GameState.PlaceBlock();
            timer3.Enabled = false;
        }

    }
}
