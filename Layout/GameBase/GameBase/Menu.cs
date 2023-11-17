using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace GameBase
{
    public partial class Menu : Form
    {
        WindowsMediaPlayer player = new WindowsMediaPlayer();
        public Menu()
        {
            InitializeComponent();
            //player.URL = (".\\Assets\\Sounds\\theme.mp3");
            //player.settings.autoStart = true;
            //player.settings.setMode("loop", true);
            //player.controls.play();
        }

        private void start_button_MouseHover(object sender, EventArgs e)
        {
            start_button.Image = Properties.Resources.start_hover;
        }

        private void start_button_MouseLeave(object sender, EventArgs e)
        {
            start_button.Image= Properties.Resources.start;
        }

        private void start_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form1 = new Form1();
            form1.Closed += (s, args) => this.Close();
            form1.Show();
        }
    }
}
