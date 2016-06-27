using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ex06_UI
{
    public partial class FormHowToPlay : Form
    {
        public FormHowToPlay()
        {
            InitializeComponent();
        }

        public string[] Lines
        {
            get { return textBoxHowToPlay.Lines; }

            set { textBoxHowToPlay.Lines = value; }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}