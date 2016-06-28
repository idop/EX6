using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Ex06_UI
{
    public class FloatingCoin : PictureBox
    {
        private Timer m_TimeToMove;

        public FloatingCoin(int i_Height, int i_Width)
        {
            Height = i_Height;
            Width = i_Width;
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, Width, Height);
            Region region = new Region(path);
            Region = region;
            m_TimeToMove = new Timer();
        }

        public event EventHandler Tick
        {
            add { m_TimeToMove.Tick += value; }
            remove { m_TimeToMove.Tick -= value; }
        }

        public int Interval
        {
            get { return m_TimeToMove.Interval; }
            set { m_TimeToMove.Interval = value; }
        }

        public void Start()
        {
            m_TimeToMove.Start();
        }

        public void Stop()
        {
            m_TimeToMove.Stop();
        }
    }
}