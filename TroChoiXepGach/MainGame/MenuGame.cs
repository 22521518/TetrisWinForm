using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using ComponentsGame.ImageDrawer;
using MainGame.DialogForm;

namespace MainGame
{
    public partial class MenuGame : BasicForm
    {
        //Background
        List<string> BGDirectory = new List<string>();
        List<ImageDrawer> BGLayers = new List<ImageDrawer>();
        private float SizeBG_Height = 405f,
                      SizeBG_Width = 720f;
        private int IndexLayer = 8;
        private float[] BGSteps;

        //Sound Effect
        private List<Image> SoundIcon = new List<Image>();
        public System.Media.SoundPlayer MenuBGSound = new System.Media.SoundPlayer();
        private int IsSound = 1;

        public MenuGame()
        {
            InitializeComponent();
            InitializeImageFolder();
            InitializSound();
            this.DoubleBuffered = true;

            //MAKE SOUND FOR BACKGROUND
            MenuBGSound.PlayLooping();
        }
        private void InitializeImageFolder()
        {
            BGDirectory = Directory.GetFiles(".\\assets\\img\\background").ToList();
            foreach (string dir in BGDirectory)
                BGLayers.Add(new ImageDrawer(Image.FromFile(dir), 0, 0));

            //TO MAKE BACKGROUND MOVE BASED ON WINDOW
            float h1 = ClientSize.Height; //WHOLE SCREEN
            float h2 = ClientSize.Height * 2 / 3; // MEDIUM
            float h3 = ClientSize.Height / 3; // LOW
            BGSteps = new float[9] { h1, h1, h1, h2, h3, 3, h2, h2, h2 };
        }

        private void InitializSound()
        {
            SoundIcon.Add(Image.FromFile(".\\assets\\img\\icon\\icons8-no-audio-50.png"));
            SoundIcon.Add(Image.FromFile(".\\assets\\img\\icon\\icons8-speaker-50.png"));
            MenuBGSound.SoundLocation = "./assets/sound/comic5.wav";
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
            labelStartingGame.ForeColor = System.Drawing.Color.FromArgb(curentColor, curentColor, curentColor);
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
            if (BGLayers[IndexLayer].PosY > BGSteps[IndexLayer])
            {
                IndexLayer--;
            }
            if (IndexLayer < 2)
            {
                IndexLayer = 8;
                this.HideBackGroundTimer.Enabled = false;
                this.ShowModeTimer.Enabled = true;
                return;
            }
            BGLayers[IndexLayer].PosY += BGSteps[IndexLayer] / 3;
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

        private void Settings_Click(object sender, EventArgs e)
        {
            if (IsSound == 1)
                MenuBGSound.Stop();
            else
                MenuBGSound.PlayLooping();

            IsSound = 1 - IsSound;
            this.VolumeIcon.Image = SoundIcon[IsSound];
        }
        private void SettingsIcon_Click(object sender, EventArgs e)
        {
            ScoreDialog Setting = new ScoreDialog(this);
            Setting.StartPosition = FormStartPosition.CenterScreen;
            Setting.Show();

        }
        private void PaintBackGroundFormEvent(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            for (int i = 0; i <= IndexLayer; i++)
                canvas.DrawImage(BGLayers[i].Img, BGLayers[i].PosX, BGLayers[i].PosY, SizeBG_Width, SizeBG_Height);
        }
    }
}
