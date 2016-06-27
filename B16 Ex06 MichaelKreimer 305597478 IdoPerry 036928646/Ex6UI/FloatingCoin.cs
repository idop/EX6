using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Ex06_UI
{
    public class FloatingCoin : PictureBox
    {
        public FloatingCoin(Image i_Image)
        {
            Height = i_Image.Height;
            Width = i_Image.Width;
            Image = i_Image;
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, Width, Height);
            Region region = new Region(path);
            Region = region;
        }
    }
}