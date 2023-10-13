using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentsGame.ImageDrawer;

namespace MainGame
{
    public partial class MenuGame : BasicForm
    {

        List<string> bgLayer_dirs = new List<string>();
        List<ImageDrawer> bgLayers = new List<ImageDrawer>();
        private float sizeBG_Height = 405f,
                      sizeBG_Width = 720f;
        private int index_layer = 8;
        private float[] stepsBG;
        public MenuGame()
        {
            InitializeComponent();
            InitializeBG();
            this.DoubleBuffered = true;

            //TO MAKE BACKGROUND MOVE BASED ON WINDOW
            float h1 = ClientSize.Height; //WHOLE SCREEN
            float h2 = ClientSize.Height * 2 / 3; // MEDIUM
            float h3 = ClientSize.Height / 3; // LOW
            stepsBG = new float[9] { h1, h1, h1, h2, h3, 3, h2, h2, h2};
        }
        private void InitializeBG()
        {
            bgLayer_dirs = Directory.GetFiles(".\\assets\\img\\background").ToList();
            foreach (string dir in bgLayer_dirs)
                bgLayers.Add(new ImageDrawer(Image.FromFile(dir), 0, 0));
        }

        private int colorTransitionRate = 17; // ff - cc = 17 ( dai mau tong xam: 00 11 22 33 44 ... ff)
        private int curentColor = 255; //White Word 
        // make word sparkle blink blink
        private void WordEffectEvent(object sender, EventArgs e)
        {
            if (curentColor == 0)
                colorTransitionRate = 17;
            if (curentColor == 255)
                colorTransitionRate = -17;
            curentColor += colorTransitionRate;
            labelStartingGame.ForeColor = Color.FromArgb(curentColor, curentColor, curentColor);
        }
        bool ModeIsOpen = false;
        private void ClickToPlayEvent(object sender, EventArgs e)
        {
            if (!ModeIsOpen)
            {
                this.HideBackGroundTimer.Enabled = true;
                this.Tetris.Visible = false;
                this.labelStartingGame.Visible = false;
            }
        }

        private void PressKeyToPlay(object sender, KeyEventArgs e)
        {
            if(!ModeIsOpen)
            {
                this.HideBackGroundTimer.Enabled = true;
                this.Tetris.Visible = false;
                this.labelStartingGame.Visible = false;
            }
        }

        private void HideBackGroundEvent(object sender, EventArgs e)
        {
            if (bgLayers[index_layer].PosY > stepsBG[index_layer])
            {
                index_layer--;
            }
            if (index_layer < 2)
            {
                index_layer = 8;
                this.HideBackGroundTimer.Enabled = false;
                this.ShowModeTimer.Enabled = true;
                return;
            }
            bgLayers[index_layer].PosY += stepsBG[index_layer] / 3;
            this.Invalidate();
        }

        private void ShowModeEvent(object sender, EventArgs e)
        {
            if (this.Mode2.Location.Y >= 80)
            {
                this.labelMode.Visible = true;
                this.ShowModeTimer.Enabled = false;
                return;
            }

            this.Mode1.Location = new System.Drawing.Point(this.Mode1.Location.X, this.Mode1.Location.Y + 29);
            this.Mode2.Location = new System.Drawing.Point(this.Mode2.Location.X, this.Mode2.Location.Y + 29);
            this.Mode3.Location = new System.Drawing.Point(this.Mode3.Location.X, this.Mode3.Location.Y + 29);
        }

        private void PaintBackGroundFormEvent(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            for (int i = 0; i <= index_layer; i++)
                canvas.DrawImage(bgLayers[i].Img, bgLayers[i].PosX, bgLayers[i].PosY, sizeBG_Width, sizeBG_Height);
        }
    }
}
