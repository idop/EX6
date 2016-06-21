using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ex06_GameLogic;
using Ex06_GameUtils;

namespace Ex06_UI
{
    public partial class FormTest : Form
    {
        private readonly Size r_GamePieceSize;
        private const int k_Margin = 20;
        private FormGameProperties m_GameProperties;
        private PlayerInfo[] m_PlayersInfo;
        private int m_NumberOfRows;
        private int m_NumberOfColumns;
        private int m_TurnNumber = 0;
        private BoardTile[,] m_UIGameBoard;
        private BoardButton[] m_UIGameBoardButtons;

        public FormTest()
        {
            r_GamePieceSize = new BoardTile().Size;
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
                saveGameProperties();
                initializeDynamicComponents();
            }
        }

        private void initializeDynamicComponents()
        {
            initializeStatusStrip();
            initGame();
        }

        private void initGame()
        {
            panelGameBoard.Width = r_GamePieceSize.Width * (m_NumberOfRows) ;
            panelGameBoard.Height = r_GamePieceSize.Height * (m_NumberOfColumns + 1);
            this.Width = panelGameBoard.Width + k_Margin * 3;
            this.Height = panelGameBoard.Height + k_Margin * 4 + menuStrip1.Height + statusStrip1.Height;
            initGameBoardButtons();
            initGameBoard();
        }

        private void initGameBoard()
        {
            m_UIGameBoard = new BoardTile[m_NumberOfRows,m_NumberOfColumns];
            for (int i = m_NumberOfRows - 1; i >= 0; --i)
            {
                for (int j = m_NumberOfColumns - 1; j >= 0; --j)
                {
                    m_UIGameBoard[i, j] = new BoardTile();
                    m_UIGameBoard[i, j].Top = panelGameBoard.Top + (m_UIGameBoard[i, j].Height * (i+1));
                    m_UIGameBoard[i, j].Left = panelGameBoard.Left + (m_UIGameBoard[i, j].Width *j);
                    this.Controls.Add(m_UIGameBoard[i, j]);
                    m_UIGameBoard[i, j].BringToFront();
                }
            }
        }

        private void initGameBoardButtons()
        {
            m_UIGameBoardButtons = new BoardButton[m_NumberOfRows];
            for (int i = 0; i < m_NumberOfRows; i++)
            {
                m_UIGameBoardButtons[i] = new BoardButton();
                m_UIGameBoardButtons[i].Top = panelGameBoard.Top;
                m_UIGameBoardButtons[i].Left = panelGameBoard.Left + i * m_UIGameBoardButtons[i].Width;
                m_UIGameBoardButtons[i].MouseLeave += new EventHandler(this.BoardButton_MouseLeave);
                m_UIGameBoardButtons[i].MouseHover += new EventHandler(this.BoardButton_MouseHover);
                m_UIGameBoardButtons[i].Click += new EventHandler(this.BoardButton_Click);
                this.Controls.Add(m_UIGameBoardButtons[i]);
                m_UIGameBoardButtons[i].BringToFront();
            }
        }

        private void initializeStatusStrip()
        {
            setCurrentPlayerStatusStripText(m_PlayersInfo[m_TurnNumber % 2].Name);
            toolStripStatusLabelScore.Text = string.Format("{0}: {1}, {2}: {3}",
                m_PlayersInfo[0].Name, m_PlayersInfo[0].Score, m_PlayersInfo[1].Name, m_PlayersInfo[1].Score);
        }

        private void setCurrentPlayerStatusStripText(string i_PlayerName)
        {
            toolStripStatusLabelCurrentPlayer.Text = string.Format("{0} {1}", GameTexts.k_CurrentPlayer, i_PlayerName);
        }

        private void saveGameProperties()
        {
            m_NumberOfRows = m_GameProperties.Rows;
            m_NumberOfColumns = m_GameProperties.Coloumns;

            if (m_PlayersInfo == null)
            {
                initNewGamePlayers();
            }
            else
            {
                m_PlayersInfo[0].Name = m_GameProperties.Player1Name;
                m_PlayersInfo[1].Name = m_GameProperties.Player2Name;
            }

        }

        private void initNewGamePlayers()
        {
            m_PlayersInfo = new PlayerInfo[2];
            m_PlayersInfo[0] = new PlayerInfo(m_GameProperties.Player1Name);
            m_PlayersInfo[1] = new PlayerInfo(m_GameProperties.Player2Name);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_GameProperties.ShowDialog();
            //TODO
        }

        private void BoardButton_MouseHover(object sender, EventArgs e)
        {
            ((BoardButton)sender).ForeColor = Color.Plum;
        }

        private void BoardButton_MouseLeave(object sender, EventArgs e)
        {
            ((BoardButton)sender).ForeColor = Color.Thistle;
        }

        private void BoardButton_Click(object sender, EventArgs e)
        {
            //TODO
        }
    }
}
