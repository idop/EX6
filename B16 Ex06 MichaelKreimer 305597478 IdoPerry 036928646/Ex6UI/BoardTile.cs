using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Ex6UI
{
    public partial class BoardTile : UserControl
    {
        public BoardTile()
        {
            InitializeComponent();
            cutCircleFromTheMiddle();
        }

        private void cutCircleFromTheMiddle()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, this.Width, this.Height);
            Region region = new Region(path);
            Region = region;
        }
    }
}
