using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Ex06_UI
{
    public class BoardTile : PictureBox
    {
        private int k_ElipsePadding = 4;

        public BoardTile()
        {
            InitializeComponent();
            setControlRegion();
        }

        private void InitializeComponent()
        {
            BackgroundImage = Properties.Resources.EmptyCell;
            BackgroundImageLayout = ImageLayout.Center;
            DoubleBuffered = true;
            Margin = new Padding(0);
            Size = new Size(67, 67);
            Enabled = false;
        }

        private void setControlRegion()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new Rectangle(0, 0, Width, Height));
            path.AddEllipse(
                k_ElipsePadding,
                k_ElipsePadding,
                Width - (2 * k_ElipsePadding),
                Height - (2 * k_ElipsePadding));
            Region region = new Region(path);
            Region = region;
        }
    }
}