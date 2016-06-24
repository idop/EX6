using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace Ex06_UI
{
    public class FloatingCoin : PictureBox
    {
        public FloatingCoin(Image i_Image)
        {
            this.Height = i_Image.Height;
            this.Width = i_Image.Width;
            this.Image = i_Image;
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, this.Width, this.Height);
            Region region = new Region(path);
            this.Region = region;
        }
    }
}
