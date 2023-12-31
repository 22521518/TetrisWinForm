﻿using System.Drawing;

namespace MainGame
{
    partial class MenuGame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Tetris = new System.Windows.Forms.Label();
            this.labelStartingGame = new System.Windows.Forms.Label();
            this.WordEffectTimer = new System.Windows.Forms.Timer(this.components);
            this.HideBackGroundTimer = new System.Windows.Forms.Timer(this.components);
            this.ShowModeTimer = new System.Windows.Forms.Timer(this.components);
            this.labelMode = new System.Windows.Forms.Label();
            this.SettingsIcon = new System.Windows.Forms.PictureBox();
            this.VolumeIcon = new System.Windows.Forms.PictureBox();
            this.Mode3 = new System.Windows.Forms.PictureBox();
            this.Mode2 = new System.Windows.Forms.PictureBox();
            this.Mode1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.SettingsIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VolumeIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Mode3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Mode2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Mode1)).BeginInit();
            this.SuspendLayout();
            // 
            // Tetris
            // 
            this.Tetris.AllowDrop = true;
            this.Tetris.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tetris.AutoSize = true;
            this.Tetris.BackColor = System.Drawing.Color.Transparent;
            this.Tetris.Font = new System.Drawing.Font("MV Boli", 80F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.Tetris.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(255)))), ((int)(((byte)(232)))));
            this.Tetris.Location = new System.Drawing.Point(181, 80);
            this.Tetris.Name = "Tetris";
            this.Tetris.Size = new System.Drawing.Size(542, 176);
            this.Tetris.TabIndex = 1;
            this.Tetris.Text = "TETRIS";
            this.Tetris.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStartingGame
            // 
            this.labelStartingGame.BackColor = System.Drawing.Color.Transparent;
            this.labelStartingGame.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelStartingGame.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.labelStartingGame.Font = new System.Drawing.Font("Yu Gothic UI", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStartingGame.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelStartingGame.Location = new System.Drawing.Point(253, 444);
            this.labelStartingGame.Name = "labelStartingGame";
            this.labelStartingGame.Size = new System.Drawing.Size(400, 32);
            this.labelStartingGame.TabIndex = 5;
            this.labelStartingGame.Text = "Press any keyboard or click here to play";
            this.labelStartingGame.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.labelStartingGame.Click += new System.EventHandler(this.ClickToPlayEvent);
            // 
            // WordEffectTimer
            // 
            this.WordEffectTimer.Enabled = true;
            this.WordEffectTimer.Interval = 45;
            this.WordEffectTimer.Tick += new System.EventHandler(this.WordEffectEvent);
            // 
            // HideBackGroundTimer
            // 
            this.HideBackGroundTimer.Interval = 20;
            this.HideBackGroundTimer.Tick += new System.EventHandler(this.HideBackGroundEvent);
            // 
            // ShowModeTimer
            // 
            this.ShowModeTimer.Interval = 40;
            this.ShowModeTimer.Tick += new System.EventHandler(this.ShowModeEvent);
            // 
            // labelMode
            // 
            this.labelMode.AutoSize = true;
            this.labelMode.BackColor = System.Drawing.Color.Transparent;
            this.labelMode.Font = new System.Drawing.Font("MV Boli", 30F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(255)))), ((int)(((byte)(232)))));
            this.labelMode.Location = new System.Drawing.Point(385, 28);
            this.labelMode.Name = "labelMode";
            this.labelMode.Size = new System.Drawing.Size(179, 65);
            this.labelMode.TabIndex = 6;
            this.labelMode.Text = "MODE";
            this.labelMode.Visible = false;
            // 
            // SettingsIcon
            // 
            this.SettingsIcon.BackColor = System.Drawing.Color.Transparent;
            this.SettingsIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SettingsIcon.Location = new System.Drawing.Point(97, 28);
            this.SettingsIcon.Name = "SettingsIcon";
            this.SettingsIcon.Size = new System.Drawing.Size(54, 52);
            this.SettingsIcon.Image = Image.FromFile("./assets/img/icon/icons8-speaker-50.png");
            this.SettingsIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.SettingsIcon.TabIndex = 8;
            this.SettingsIcon.TabStop = false;
            this.SettingsIcon.Click += new System.EventHandler(this.SettingsIcon_Click);
            // 
            // VolumeIcon
            // 
            this.VolumeIcon.BackColor = System.Drawing.Color.Transparent;
            this.VolumeIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.VolumeIcon.Location = new System.Drawing.Point(831, 28);
            this.VolumeIcon.Name = "VolumeIcon";
            this.VolumeIcon.Size = new System.Drawing.Size(32, 32);
            this.VolumeIcon.Image = Image.FromFile("./assets/img/icon/icons8-speaker-50.png");
            this.VolumeIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.VolumeIcon.TabIndex = 7;
            this.VolumeIcon.TabStop = false;
            this.VolumeIcon.Click += new System.EventHandler(this.Settings_Click);
            // 
            // Mode3
            // 
            this.Mode3.Location = new System.Drawing.Point(609, -378);
            this.Mode3.Name = "Mode3";
            this.Mode3.Size = new System.Drawing.Size(160, 263);
            this.Mode3.TabIndex = 4;
            this.Mode3.TabStop = false;
            // 
            // Mode2
            // 
            this.Mode2.Location = new System.Drawing.Point(396, -378);
            this.Mode2.Name = "Mode2";
            this.Mode2.Size = new System.Drawing.Size(160, 263);
            this.Mode2.TabIndex = 3;
            this.Mode2.TabStop = false;
            // 
            // Mode1
            // 
            this.Mode1.Location = new System.Drawing.Point(176, -378);
            this.Mode1.Name = "Mode1";
            this.Mode1.Size = new System.Drawing.Size(160, 263);
            this.Mode1.TabIndex = 2;
            this.Mode1.TabStop = false;
            // 
            // MenuGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 498);
            this.Controls.Add(this.SettingsIcon);
            this.Controls.Add(this.VolumeIcon);
            this.Controls.Add(this.labelMode);
            this.Controls.Add(this.labelStartingGame);
            this.Controls.Add(this.Mode3);
            this.Controls.Add(this.Mode2);
            this.Controls.Add(this.Mode1);
            this.Controls.Add(this.Tetris);
            this.Name = "MenuGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TETRIS";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintBackGroundFormEvent);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PressKeyToPlay);
            ((System.ComponentModel.ISupportInitialize)(this.SettingsIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VolumeIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Mode3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Mode2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Mode1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Tetris;
        private System.Windows.Forms.PictureBox Mode1;
        private System.Windows.Forms.PictureBox Mode2;
        private System.Windows.Forms.PictureBox Mode3;
        private System.Windows.Forms.Label labelStartingGame;
        private System.Windows.Forms.Timer WordEffectTimer;
        private System.Windows.Forms.Timer HideBackGroundTimer;
        private System.Windows.Forms.Timer ShowModeTimer;
        private System.Windows.Forms.Label labelMode;
        private System.Windows.Forms.PictureBox VolumeIcon;
        private System.Windows.Forms.PictureBox SettingsIcon;
    }
}