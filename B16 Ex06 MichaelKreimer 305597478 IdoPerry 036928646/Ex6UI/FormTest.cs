using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ex06_GameLogic;
using Ex06_GameUtils;
using Ex06_UI.Properties;

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
        private MouseFollower m_MouseFollower;
        private GameManager m_GameManager;

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
                initGame();
            }
        }

        private void initGame()
        {
            initializeStatusStrip();
            m_TurnNumber = 0;
            panelGameBoard.Width = r_GamePieceSize.Width * (m_NumberOfRows);
            panelGameBoard.Height = r_GamePieceSize.Height * (m_NumberOfColumns + 1);
            this.Width = panelGameBoard.Width + k_Margin * 2;
            this.Height = panelGameBoard.Height + k_Margin * 4 + menuStrip1.Height + statusStrip1.Height;
            m_GameManager = new GameManager(m_NumberOfRows, m_NumberOfColumns);
            initGameBoardButtons();
            initGameBoard();
            initMouseFollower();
        }


        private void initMouseFollower()
        {
            m_MouseFollower = new MouseFollower(getCurrentPlayerImage());
            m_MouseFollower.Location = Cursor.Position;
            this.Controls.Add(m_MouseFollower);
            m_MouseFollower.BringToFront();
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
                m_UIGameBoardButtons[i].Text = (i+1).ToString();
                m_UIGameBoardButtons[i].Click += new EventHandler(this.BoardButton_Click);
                this.Controls.Add(m_UIGameBoardButtons[i]);
                m_UIGameBoardButtons[i].BringToFront();
            }
        }

        private void initializeStatusStrip()
        {
            setCurrentPlayerStatusStripText(m_PlayersInfo[m_TurnNumber % 2].Name);
            updateStatusStripScore();
        }

        private void updateStatusStripScore()
        {
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

        private void BoardButton_Click(object sender, EventArgs e)
        {
            BoardButton currentSelectedButton = sender as BoardButton;
            PlayerMove playerMove = m_GameManager.PlayHumanTurn(int.Parse(currentSelectedButton.Text));
            updateFormWithUserAction(currentSelectedButton, playerMove);
        }

        private void updateFormWithUserAction(BoardButton i_CurrentSelectedButton, PlayerMove i_PlayerMove)
        {
            m_UIGameBoard[i_PlayerMove.SelectedRow, i_PlayerMove.SelectedColumn].Image = getCurrentPlayerTileImage();
            m_UIGameBoard[i_PlayerMove.SelectedRow, i_PlayerMove.SelectedColumn].Region = new Region();
            m_UIGameBoard[i_PlayerMove.SelectedRow, i_PlayerMove.SelectedColumn].BringToFront();
            this.Refresh();
            if (m_GameManager.IsColumnFull(i_PlayerMove.SelectedColumn))
            {
                i_CurrentSelectedButton.Enabled = false;
            }

            checkBoardStatus(i_PlayerMove.GameStatus);
            m_TurnNumber++;
            disposeMouseFollower();
            setCurrentPlayerStatusStripText(m_PlayersInfo[m_TurnNumber % 2].Name);
        }

        private void disposeMouseFollower()
        {
            this.Controls.Remove(m_MouseFollower);
            m_MouseFollower.Dispose();
            initMouseFollower();
        }

        private void checkBoardStatus(GameBoard.eBoardStatus i_gameStatus)
        {
            if (i_gameStatus != GameBoard.eBoardStatus.NextPlayerCanPlay)
            {
                DialogResult playerWantsToPlayAgain;
                if (i_gameStatus == GameBoard.eBoardStatus.PlayerWon)
                {
                    int playerNumber = m_TurnNumber % 2;
                    string playerWonMessage = string.Format(GameTexts.k_MessageWin, m_PlayersInfo[playerNumber].Name);
                    playerWantsToPlayAgain = openMessageBox(playerWonMessage, GameTexts.k_MessageBoxTitle);
                    m_PlayersInfo[playerNumber].Score += playerWantsToPlayAgain.Equals(DialogResult.Yes) ? 1 : 0;
                    updateStatusStripScore();
                    this.Refresh();
                    
                }
                else
                {
                    playerWantsToPlayAgain = openMessageBox(GameTexts.k_MessageTie, GameTexts.k_ATie);
                }

                if (playerWantsToPlayAgain == DialogResult.No)
                {
                    this.Close();
                }
                else
                {
                    resetGame();
                }
            }
        }

        private void resetGame()
        {
            disposeCurrentGame();
            initGame();
        }

        private void disposeCurrentGame()
        {
            disposeGameBoard();
            disposeGameBoardButtons();
            disposeMouseFollower(); 
        }

        private void disposeGameBoardButtons()
        {
            for (int i = 0; i < m_NumberOfRows; i++)
            {
                m_UIGameBoardButtons[i].Click -= new EventHandler(this.BoardButton_Click);
                this.Controls.Remove(m_UIGameBoardButtons[i]);
                m_UIGameBoardButtons[i].Dispose();
            }
        }

        private void disposeGameBoard()
        {
            for (int i = m_NumberOfRows - 1; i >= 0; --i)
            {
                for (int j = m_NumberOfColumns - 1; j >= 0; --j)
                {
                    this.Controls.Remove(m_UIGameBoard[i, j]);
                    m_UIGameBoard[i, j].Dispose();
                }
            }
        }

        private DialogResult openMessageBox(string i_Message, string i_MessageTitle)
        {
            DialogResult dialogResult = MessageBox.Show(i_Message, i_MessageTitle, MessageBoxButtons.YesNo);
            return dialogResult;
        }

        private Image getCurrentPlayerTileImage()
        {
            return m_TurnNumber % 2 == 0 ? Resources.FullCellRed : Resources.FullCellYellow;
        }

        private Image getCurrentPlayerImage()
        {
            return m_TurnNumber % 2 == 0 ? Resources.CoinRed : Resources.CoinYellow;
        }

        private void FormTest_MouseMove(object sender, MouseEventArgs e)
        {
            m_MouseFollower.Location = new Point(e.X, e.Y);
            m_MouseFollower.BringToFront();
        }

        private void startANewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resetGame();
        }

        private void startANewTournirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resetScore();
            resetGame();
        }

        private void resetScore()
        {
            m_PlayersInfo[0].Score = 0;
            m_PlayersInfo[1].Score = 0;
        }
    }
}
