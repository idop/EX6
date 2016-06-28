namespace Ex06_UI
{
    public partial class FormGame
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startANewGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startANewTournirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.howToPlayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelCurrentPlayer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelScore = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelGameBoard = new System.Windows.Forms.Panel();
            this.timerFall = new System.Windows.Forms.Timer(this.components);
            this.timerWiningPath = new System.Windows.Forms.Timer(this.components);
            this.timerMouseFollow = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(852, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startANewGameToolStripMenuItem,
            this.startANewTournirToolStripMenuItem,
            this.propertiesToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(60, 24);
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // startANewGameToolStripMenuItem
            // 
            this.startANewGameToolStripMenuItem.Name = "startANewGameToolStripMenuItem";
            this.startANewGameToolStripMenuItem.Size = new System.Drawing.Size(211, 26);
            this.startANewGameToolStripMenuItem.Text = "Start a New Game";
            this.startANewGameToolStripMenuItem.Click += new System.EventHandler(this.startANewGameToolStripMenuItem_Click);
            // 
            // startANewTournirToolStripMenuItem
            // 
            this.startANewTournirToolStripMenuItem.Name = "startANewTournirToolStripMenuItem";
            this.startANewTournirToolStripMenuItem.Size = new System.Drawing.Size(211, 26);
            this.startANewTournirToolStripMenuItem.Text = "Start a New Tournir";
            this.startANewTournirToolStripMenuItem.Click += new System.EventHandler(this.startANewTournirToolStripMenuItem_Click);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(211, 26);
            this.propertiesToolStripMenuItem.Text = "Properties...";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(208, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(211, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.howToPlayToolStripMenuItem,
            this.toolStripSeparator2,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // howToPlayToolStripMenuItem
            // 
            this.howToPlayToolStripMenuItem.Name = "howToPlayToolStripMenuItem";
            this.howToPlayToolStripMenuItem.Size = new System.Drawing.Size(172, 26);
            this.howToPlayToolStripMenuItem.Text = "How to play?";
            this.howToPlayToolStripMenuItem.Click += new System.EventHandler(this.howToPlayToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(169, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(172, 26);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.GripMargin = new System.Windows.Forms.Padding(4);
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelCurrentPlayer,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabelScore});
            this.statusStrip1.Location = new System.Drawing.Point(0, 647);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(852, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelCurrentPlayer
            // 
            this.toolStripStatusLabelCurrentPlayer.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStripStatusLabelCurrentPlayer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStripStatusLabelCurrentPlayer.BorderStyle = System.Windows.Forms.Border3DStyle.Bump;
            this.toolStripStatusLabelCurrentPlayer.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabelCurrentPlayer.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.toolStripStatusLabelCurrentPlayer.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.toolStripStatusLabelCurrentPlayer.Name = "toolStripStatusLabelCurrentPlayer";
            this.toolStripStatusLabelCurrentPlayer.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(36, 17);
            this.toolStripStatusLabel1.Text = "       ";
            // 
            // toolStripStatusLabelScore
            // 
            this.toolStripStatusLabelScore.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.toolStripStatusLabelScore.Name = "toolStripStatusLabelScore";
            this.toolStripStatusLabelScore.Size = new System.Drawing.Size(0, 17);
            // 
            // panelGameBoard
            // 
            this.panelGameBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelGameBoard.BackColor = System.Drawing.Color.Thistle;
            this.panelGameBoard.ForeColor = System.Drawing.Color.Thistle;
            this.panelGameBoard.Location = new System.Drawing.Point(20, 48);
            this.panelGameBoard.Margin = new System.Windows.Forms.Padding(0);
            this.panelGameBoard.Name = "panelGameBoard";
            this.panelGameBoard.Size = new System.Drawing.Size(810, 580);
            this.panelGameBoard.TabIndex = 2;
            // 
            // timerFall
            // 
            this.timerFall.Tick += new System.EventHandler(this.timerFall_Tick);
            // 
            // timerWiningPath
            // 
            this.timerWiningPath.Tick += new System.EventHandler(this.timerWiningPath_Tick);
            // 
            // timerMouseFollow
            // 
            this.timerMouseFollow.Enabled = true;
            this.timerMouseFollow.Interval = 10;
            this.timerMouseFollow.Tick += new System.EventHandler(this.timerMouseFollow_Tick);
            // 
            // FormGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(852, 669);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panelGameBoard);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormTest";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startANewGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startANewTournirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem howToPlayToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCurrentPlayer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelScore;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel panelGameBoard;
        private System.Windows.Forms.Timer timerFall;
        private System.Windows.Forms.Timer timerWiningPath;
        private System.Windows.Forms.Timer timerMouseFollow;
    }
}