using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainGame
{
    public partial class MainMenu : BasicForm
    {
        public MainMenu()
        {
            InitializeComponent();
            this.BackgroundImage = Image.FromFile("./assets/img/background/postapocalypse2.png");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.DoubleBuffered = true;
        }
    }
}
