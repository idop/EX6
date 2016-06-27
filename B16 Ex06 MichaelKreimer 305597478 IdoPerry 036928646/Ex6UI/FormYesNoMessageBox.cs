using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ex06_UI
{
    public partial class FormYesNoMessageBox : Form
    {
        public FormYesNoMessageBox()
        {
            InitializeComponent();
        }

        public string Message
        {
            get { return labelMessage.Text; }

            set { labelMessage.Text = value; }
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }
    }
}