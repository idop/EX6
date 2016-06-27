using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ex06_UI
{
    public class BoardButton : UserControl
    {
        public BoardButton()
        {
            BackColor = Color.Thistle;
            Margin = new Padding(0);
            Size = new Size(67, 67);
            BackgroundImageLayout = ImageLayout.None;
            ForeColor = Color.Thistle;
            Enabled = true;
        }
    }
}