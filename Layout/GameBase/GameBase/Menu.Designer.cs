namespace GameBase
{
    partial class Menu
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
            this.start_button = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.start_button)).BeginInit();
            this.SuspendLayout();
            // 
            // start_button
            // 
            this.start_button.Image = global::GameBase.Properties.Resources.start;
            this.start_button.Location = new System.Drawing.Point(220, 674);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(392, 62);
            this.start_button.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.start_button.TabIndex = 0;
            this.start_button.TabStop = false;
            this.start_button.Click += new System.EventHandler(this.start_button_Click);
            this.start_button.MouseLeave += new System.EventHandler(this.start_button_MouseLeave);
            this.start_button.MouseHover += new System.EventHandler(this.start_button_MouseHover);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::GameBase.Properties.Resources.blankmenu;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1000, 800);
            this.Controls.Add(this.start_button);
            this.DoubleBuffered = true;
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu";
            ((System.ComponentModel.ISupportInitialize)(this.start_button)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox start_button;
    }
}