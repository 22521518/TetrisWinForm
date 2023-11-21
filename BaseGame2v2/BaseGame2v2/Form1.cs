using SurvivalPvP.Object;
using SurvivalPvP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System.Diagnostics.Tracing;

namespace BaseGame2v2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GameState = new GameState();
            ch = GameState.Player;
            SetUp();
        }
        void SetUp()
        {
            HoldBox.SizeMode = PictureBoxSizeMode.StretchImage;
            HoldBox.Image = null;

            //Location of HoldBox will turn block
            this.HoldBox.Location = new System.Drawing.Point(GridX + CellSize * 15, GridY + CellSize * 10);
        }
        void DrawChar(Character ch)
        {
            int Row = 50 + CellSize * ch.Body[0].Row;
            int Column = CellSize * ch.Body[0].Column;
            PlayerY = (Column - CellSize / 2) + ch.JumpSteps * CellSize / 4;
            PlayerX = Row + ch.Steps * CellSize / (4);
        }
        void ResetForm()
        {
            tick = 0;
            Jump = 0;
            MoveLeft = MoveRight = false;
            BlockLeft = BlockRight = false;
            GameState = new GameState();
            ch = GameState.Player;
        }
        void Draw(GameState game)
        {
            this.HoldBox.Image = game.Player.Hold ? TileImage[1] : null;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //------ Character ------//
            if (GameState.Player.isLanded)
            {
                if (e.KeyCode == Keys.Up)
                {
                    Jump = 4;
                }
            }
            if (e.KeyCode == Keys.Left)
                MoveLeft = true;
            if (e.KeyCode == Keys.Right)
                MoveRight = true;
            if (e.KeyCode == Keys.Down)
            {
                if (GameState.Player.Hold)
                    GameState.PutBlock();
                else
                    GameState.TakeBlock();
            }
            //------ BLOCK ------//
            if (e.KeyCode == Keys.W)
            {
                if (GameState.BlockRotate90())
                    this.DelayPlaceBlock.Enabled = false;
            }
            if (e.KeyCode == Keys.A)
            {
                BlockLeft = true;
            }
            if (e.KeyCode == Keys.D)
            {
                BlockRight = true;
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                MoveLeft = false;
            if (e.KeyCode == Keys.Right)
                MoveRight = false;
            if (e.KeyCode == Keys.A)
                BlockLeft = false;
            if (e.KeyCode == Keys.D)
                BlockRight = false;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Grid
            Graphics graphics = e.Graphics;
            for (int r = 0; r < 22; r++)
                for (int c = 0; c < 10; c++)
                {
                    if (r < 2)
                        continue;
                    //Chỉnh vị trí Grid = GridX, GridY biến ở dưới cùng
                    graphics.DrawImage(TileImage[GameState.GameGrid[r, c] > 0 ? 1 : 0], GridX + CellSize * (c), GridY + CellSize * (r), CellSize, CellSize);
                }
            //Block
            foreach (Position p in GameState.CurrentBlock.PositionInTiles())
            {
                if (p.Row < 2)
                    continue;
                int r = GridY + CellSize * p.Row;
                int c = GridX + CellSize * p.Column;
                graphics.DrawImage(TileImage[1], c, r, CellSize, CellSize);
            }
            //Character
            int Row = CellSize * (ch.Body[0].Row);
            int Column = CellSize * ch.Body[0].Column;
            PlayerY = GridY + (Row - CellSize / 2) + ch.JumpSteps * CellSize / 4;
            PlayerX = GridX + Column + ch.Steps * CellSize / (4);
            graphics.DrawImage(Characters[0], PlayerX, PlayerY, CellSize, CellSize + CellSize / 2);
        }
        private void MoveTimer_Tick(object sender, EventArgs e)
        {
            if (GameState.isGameOver())
            {
                MoveTimer.Stop();
                Draw(GameState);
                this.Invalidate();
                if (GameState.PlayerLose)
                    MessageBox.Show("You Lose");
                else
                    MessageBox.Show("You Win");
                ResetForm();
                MoveTimer.Start();
                return;
            }
            if (tickDelay % 3 == 0)
            {
                if (BlockLeft)
                    if (GameState.MoveBlockLeft())
                        DelayPlaceBlock.Enabled = false;
                if (BlockRight)
                    if (GameState.MoveBlockRight())
                        DelayPlaceBlock.Enabled = false;
            }
            if (tick % 12 == 0)
                if (!GameState.MoveDown())
                    DelayPlaceBlock.Enabled = true;
            if (MoveLeft)
                GameState.MoveLeft();
            if (MoveRight)
                GameState.MoveRight();
            if (Jump > 0)
            {
                for (int i = 0; i < Jump; i++)
                {
                    if (!GameState.Jump(1))
                    {
                        Jump = 0;
                    }
                }
                Jump--;
            }
            else
            {
                GameState.Fall(1);
            }
            Draw(GameState);
            Invalidate();
            tick = (tick + 1) % 13;
            tickDelay = (tickDelay + 1) % 6;
        }
        private void DelayPlaceBlock_Tick(object sender, EventArgs e)
        {
            MoveTimer.Stop();
            GameState.PlaceBlock();
            DelayPlaceBlock.Enabled = false;
            MoveTimer.Start();
        }
        int tick;
        int tickDelay;
        int Jump;
        int CellSize = 32;
        bool MoveLeft, MoveRight;
        bool BlockLeft, BlockRight;
        GameState GameState;
        Character ch;
        int PlayerX, PlayerY;
        int GridX = 40, GridY = 50;
        Image[] TileImage = new Image[]
        {
            Image.FromFile(@".\assets\blocks\EmptyBlock.png"),
            Image.FromFile(@".\assets\blocks\Block.png")
        };
        Image[] Characters = new Image[]
        {
            Image.FromFile(@".\assets\characters\player.png"),

        };
        Bitmap[] TileImageBlur = new Bitmap[]
        {
            new Bitmap(@".\assets\blocks\EmptyBlock.png"),
            new Bitmap(@".\assets\blocks\Block.png")
        };
    }
}
