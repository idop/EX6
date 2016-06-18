using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ex6UI
{
    public partial class FormTest : Form
    {
        private FormGameProperties m_GameProperties;

        public FormTest()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            m_GameProperties = new FormGameProperties();
            m_GameProperties.ShowDialog();
            if (m_GameProperties.DialogResult == DialogResult.Cancel)
            {
                this.Close();
            }
            else
            {
                initializeDynamicComponents();
            }
        }

        private void initializeDynamicComponents()
        {
            throw new NotImplementedException();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
