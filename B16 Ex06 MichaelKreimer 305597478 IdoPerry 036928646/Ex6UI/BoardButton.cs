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
            this.BackColor = System.Drawing.Color.Thistle;
            this.Margin = new Padding(0);
            this.Size = new Size(67, 67);
            this.BackgroundImageLayout = ImageLayout.None;
            this.ForeColor = Color.Thistle;
            this.Enabled = true;
        }
    }
}
