using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Ex06_UI
{
    public partial class BoardTile : UserControl
    {
        private int k_ElipsePadding = 4;
        public BoardTile()
        {
            InitializeComponent();
            setControlRegion();
        }

        private void setControlRegion()
        {
            //TODO
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new Rectangle(0, 0, this.Width, this.Height));
            path.AddEllipse(k_ElipsePadding, k_ElipsePadding, this.Width - 2 * k_ElipsePadding, this.Height - 2 * k_ElipsePadding);
            Region region = new Region(path);
            this.Region = region;
        }
    }
}
