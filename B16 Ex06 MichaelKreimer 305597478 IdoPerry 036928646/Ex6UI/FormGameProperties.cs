using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ex06_UI
{
    public partial class FormGameProperties : Form
    {
        public FormGameProperties()
        {
            InitializeComponent();
        }

        public string Player1Name
        {
            get { return TextBoxPlayer1.Text; }
        }

        public string Player2Name
        {
            get { return TextBoxPlayer2.Text; }
        }

        public int Rows
        {
            get { return (int) nUDRows.Value; }
        }

        public int Coloumns
        {
            get { return (int) nUDCols.Value; }
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}