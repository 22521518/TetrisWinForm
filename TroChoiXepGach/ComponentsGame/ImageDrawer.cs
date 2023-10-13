using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ComponentsGame.ImageDrawer
{
    public class ImageDrawer
    {
        public Image Img { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public ImageDrawer(Image i1, float i2, float i3)
        {
            Img = i1;
            PosX = i2;
            PosY = i3;
        }
    }
}
