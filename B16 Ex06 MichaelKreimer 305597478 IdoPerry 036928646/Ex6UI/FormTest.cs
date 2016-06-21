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
        private FormGameProperties m_GameProperties;
        private PlayerInfo[] m_PlayersInfo;
        private int m_NumberOfRows;
        private int m_NumberOfColumns;
        private int m_TurnNumber = 0;

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
            saveGameProperties();
            initializeStatusStrip();
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
    }
}
