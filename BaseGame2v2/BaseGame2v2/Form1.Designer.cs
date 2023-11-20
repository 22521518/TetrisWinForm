namespace BaseGame2v2
{
    partial class Form1
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
            this.MoveTimer = new System.Windows.Forms.Timer(this.components);
            this.DelayPlaceBlock = new System.Windows.Forms.Timer(this.components);
            this.HoldBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.HoldBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MoveTimer
            // 
            this.MoveTimer.Enabled = true;
            this.MoveTimer.Interval = 50;
            this.MoveTimer.Tick += new System.EventHandler(this.MoveTimer_Tick);
            // 
            // DelayPlaceBlock
            // 
            this.DelayPlaceBlock.Interval = 25;
            this.DelayPlaceBlock.Tick += new System.EventHandler(this.DelayPlaceBlock_Tick);
            // 
            // HoldBox
            // 
            this.HoldBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HoldBox.Location = new System.Drawing.Point(585, 198);
            this.HoldBox.Name = "HoldBox";
            this.HoldBox.Size = new System.Drawing.Size(32, 32);
            this.HoldBox.TabIndex = 29;
            this.HoldBox.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 530);
            this.Controls.Add(this.HoldBox);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.HoldBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer MoveTimer;
        private System.Windows.Forms.Timer DelayPlaceBlock;
        private System.Windows.Forms.PictureBox HoldBox;
    }
}

