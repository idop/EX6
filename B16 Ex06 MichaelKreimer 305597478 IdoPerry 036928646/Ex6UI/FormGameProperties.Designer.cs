namespace Ex6UI
{
    public partial class FormGameProperties
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGameProperties));
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TextBoxPlayer1 = new System.Windows.Forms.TextBox();
            this.TextBoxPlayer2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.nUDRows = new System.Windows.Forms.NumericUpDown();
            this.nUDCols = new System.Windows.Forms.NumericUpDown();
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nUDRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDCols)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // TextBoxPlayer1
            // 
            resources.ApplyResources(this.TextBoxPlayer1, "TextBoxPlayer1");
            this.TextBoxPlayer1.Name = "TextBoxPlayer1";
            // 
            // TextBoxPlayer2
            // 
            resources.ApplyResources(this.TextBoxPlayer2, "TextBoxPlayer2");
            this.TextBoxPlayer2.Name = "TextBoxPlayer2";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // ButtonOk
            // 
            resources.ApplyResources(this.ButtonOk, "ButtonOk");
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.UseVisualStyleBackColor = true;
            this.ButtonOk.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // nUDRows
            // 
            resources.ApplyResources(this.nUDRows, "nUDRows");
            this.nUDRows.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nUDRows.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nUDRows.Name = "nUDRows";
            this.nUDRows.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // nUDCols
            // 
            resources.ApplyResources(this.nUDCols, "nUDCols");
            this.nUDCols.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nUDCols.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nUDCols.Name = "nUDCols";
            this.nUDCols.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormGameProperties
            // 
            this.AcceptButton = this.ButtonOk;
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.nUDCols);
            this.Controls.Add(this.nUDRows);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TextBoxPlayer2);
            this.Controls.Add(this.TextBoxPlayer1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGameProperties";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            ((System.ComponentModel.ISupportInitialize)(this.nUDRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDCols)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TextBoxPlayer1;
        private System.Windows.Forms.TextBox TextBoxPlayer2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.NumericUpDown nUDRows;
        private System.Windows.Forms.NumericUpDown nUDCols;
        private System.Windows.Forms.Button buttonCancel;
    }
}