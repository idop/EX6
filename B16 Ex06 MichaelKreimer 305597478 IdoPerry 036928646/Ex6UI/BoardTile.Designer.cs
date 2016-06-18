namespace Ex6UI
{
    partial class BoardTile
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxTile = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTile)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxTile
            // 
            this.pictureBoxTile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBoxTile.Image = global::Ex6UI.Properties.Resources.EmptyCell;
            this.pictureBoxTile.InitialImage = global::Ex6UI.Properties.Resources.EmptyCell;
            this.pictureBoxTile.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxTile.Name = "pictureBoxTile";
            this.pictureBoxTile.Size = new System.Drawing.Size(67, 67);
            this.pictureBoxTile.TabIndex = 0;
            this.pictureBoxTile.TabStop = false;
            // 
            // BoardTile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBoxTile);
            this.Name = "BoardTile";
            this.Size = new System.Drawing.Size(67, 67);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTile)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxTile;
    }
}
