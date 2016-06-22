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
    {//
        private int k_ElipsePadding = 4;
        public BoardTile()
        {
            InitializeComponent();
            setControlRegion();
        }
        
        private void InitializeComponent()
        { 
            this.BackgroundImage = Properties.Resources.EmptyCell;
            this.BackgroundImageLayout = ImageLayout.Center;
            this.DoubleBuffered = true;
            this.Margin = new Padding(0);
            this.Size = new Size(67, 67);
            this.ResumeLayout(false);

        }

        private void setControlRegion()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new Rectangle(0, 0, this.Width, this.Height));
            path.AddEllipse(k_ElipsePadding, k_ElipsePadding, this.Width - 2 * k_ElipsePadding, this.Height - 2 * k_ElipsePadding);
            Region region = new Region(path);
            this.Region = region;
        }
    }
}
