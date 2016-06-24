using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using Ex06_GameLogic;
using Ex06_GameUtils;
using Ex06_UI.Properties;


namespace Ex06_UI
{
    public partial class FormGame : Form
    {
        private const int k_Margin = 20;
        private readonly Size r_GamePieceSize;
        private FormGameProperties m_GameProperties;
        private PlayerInfo[] m_PlayersInfo;
        private int m_NumberOfRows;
        private int m_NumberOfColumns;
        private int m_TurnNumber = 0;
        private BoardTile[,] m_UIGameBoard;
        private BoardButton[] m_UIGameBoardButtons;
        private FloatingCoin m_MouseFollower;
        private GameManager m_GameManager;
        private FormHowToPlay m_FormHowToPlay;
        private FormAbout m_FormAbout;
        private PlayerMove m_LastPlayerMove;
        private FloatingCoin m_fallingCoin;
        private BoardButton m_LastCurrentSelectedButton;

        public FormGame()
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
                m_FormAbout = new FormAbout();
                initFormHowToPlay();
                initGame();
            }
        }

        private void initFormHowToPlay()
        {
            m_FormHowToPlay = new FormHowToPlay();
            m_FormHowToPlay.Lines = File.ReadAllLines(GameUtils.k_HowToPlayFileLocation);
        }

        private void initGame()
        {
            initializeStatusStrip();
            m_TurnNumber = 0;
            panelGameBoard.Width = r_GamePieceSize.Width * m_NumberOfRows;
            panelGameBoard.Height = r_GamePieceSize.Height * (m_NumberOfColumns + 1);
            this.Width = panelGameBoard.Width + (k_Margin * 2);
            this.Height = panelGameBoard.Height + (k_Margin * 4) + menuStrip1.Height + statusStrip1.Height;
            m_GameManager = new GameManager(m_NumberOfRows, m_NumberOfColumns);
            initGameBoardButtons();
            initGameBoard();
            initMouseFollower();
        }

        private void initMouseFollower()
        {
            if (m_MouseFollower == null)
            {
                m_MouseFollower = new FloatingCoin(getCurrentPlayerImage());
                m_MouseFollower.Location = Cursor.Position;
                this.Controls.Add(m_MouseFollower);
                m_MouseFollower.BringToFront();
            }
        }

        private void initGameBoard()
        {
            m_UIGameBoard = new BoardTile[m_NumberOfRows, m_NumberOfColumns];
            for (int i = m_NumberOfRows - 1; i >= 0; --i)
            {
                for (int j = m_NumberOfColumns - 1; j >= 0; --j)
                {
                    m_UIGameBoard[i, j] = new BoardTile();
                    m_UIGameBoard[i, j].Top = panelGameBoard.Top + (m_UIGameBoard[i, j].Height * (i + 1));
                    m_UIGameBoard[i, j].Left = panelGameBoard.Left + (m_UIGameBoard[i, j].Width * j);
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
                m_UIGameBoardButtons[i].Left = panelGameBoard.Left + (i * m_UIGameBoardButtons[i].Width);
                m_UIGameBoardButtons[i].Text = (i + 1).ToString();
                m_UIGameBoardButtons[i].Click += new EventHandler(this.BoardButton_Click);
                this.Controls.Add(m_UIGameBoardButtons[i]);
                m_UIGameBoardButtons[i].BringToFront();
            }
        }

        private void initializeStatusStrip()
        {
            setCurrentPlayerStatusStripText(m_PlayersInfo[m_TurnNumber % GameUtils.k_NumberOfPlayers].Name);
            updateStatusStripScore();
        }

        private void updateStatusStripScore()
        {
            toolStripStatusLabelScore.Text = string.Format(
"{0}: {1}, {2}: {3}",
m_PlayersInfo[GameUtils.k_FirstPlayerIndex].Name, 
m_PlayersInfo[GameUtils.k_FirstPlayerIndex].Score, 
m_PlayersInfo[GameUtils.k_SecondPlayerIndex].Name, 
m_PlayersInfo[GameUtils.k_SecondPlayerIndex].Score);
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
                m_PlayersInfo[GameUtils.k_FirstPlayerIndex].Name = m_GameProperties.Player1Name;
                m_PlayersInfo[GameUtils.k_SecondPlayerIndex].Name = m_GameProperties.Player2Name;
            }
        }

        private void initNewGamePlayers()
        {
            m_PlayersInfo = new PlayerInfo[GameUtils.k_NumberOfPlayers];
            m_PlayersInfo[GameUtils.k_FirstPlayerIndex] = new PlayerInfo(m_GameProperties.Player1Name);
            m_PlayersInfo[GameUtils.k_SecondPlayerIndex] = new PlayerInfo(m_GameProperties.Player2Name);
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
            m_LastCurrentSelectedButton = sender as BoardButton;
            m_LastPlayerMove = m_GameManager.PlayHumanTurn(int.Parse(m_LastCurrentSelectedButton.Text));
            m_LastCurrentSelectedButton.Enabled = false;
            dropCoin();
        }

        private void updateFormWithUserAction()
        {
            m_UIGameBoard[m_LastPlayerMove.SelectedRow, m_LastPlayerMove.SelectedColumn].Image = getCurrentPlayerTileImage();
            m_UIGameBoard[m_LastPlayerMove.SelectedRow, m_LastPlayerMove.SelectedColumn].Region = new Region();
            m_UIGameBoard[m_LastPlayerMove.SelectedRow, m_LastPlayerMove.SelectedColumn].BringToFront();
            this.Refresh();
            if (m_GameManager.IsColumnFull(m_LastPlayerMove.SelectedColumn))
            {
                m_LastCurrentSelectedButton.Enabled = false;
            }

            m_TurnNumber++;
            checkBoardStatus(m_LastPlayerMove.GameStatus);
            initMouseFollower();
            setCurrentPlayerStatusStripText(m_PlayersInfo[m_TurnNumber % GameUtils.k_NumberOfPlayers].Name);
            m_LastCurrentSelectedButton.Enabled = true;
        }

        private void dropCoin()
        {
            m_fallingCoin = new FloatingCoin(getCurrentPlayerImage());
            m_fallingCoin.Location = m_LastCurrentSelectedButton.Location;
            disposeMouseFollower();
            this.Controls.Add(m_fallingCoin);
            m_fallingCoin.BringToFront();
            timerFall.Start();       
        }

        private void disposeMouseFollower()
        {
            if (m_MouseFollower != null)
            {
                this.Controls.Remove(m_MouseFollower);
                m_MouseFollower.Dispose();
                m_MouseFollower = null;
            }
        }

        private void checkBoardStatus(GameBoard.eBoardStatus i_gameStatus)
        {
            if (i_gameStatus != GameBoard.eBoardStatus.NextPlayerCanPlay)
            {
                DialogResult playerWantsToPlayAgain;
                if (i_gameStatus == GameBoard.eBoardStatus.PlayerWon)
                {
                    int playerNumber = (m_TurnNumber - 1) % GameUtils.k_NumberOfPlayers;
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
            return m_TurnNumber % GameUtils.k_NumberOfPlayers == 0 ? Resources.FullCellRed : Resources.FullCellYellow;
        }

        private Image getCurrentPlayerImage()
        {
            return m_TurnNumber % GameUtils.k_NumberOfPlayers == 0 ? Resources.CoinRed : Resources.CoinYellow;
        }

        private void FormGame_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_MouseFollower != null)
            {
                m_MouseFollower.Location = e.Location;
                m_MouseFollower.BringToFront();
            }
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
            m_PlayersInfo[GameUtils.k_FirstPlayerIndex].Score = 0;
            m_PlayersInfo[GameUtils.k_SecondPlayerIndex].Score = 0;
        }

        private void howToPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int heightFactor = k_Margin * GameUtils.k_HowToplayFormHeightFactorMultiplayer;
            int widthFactor = k_Margin;
            setControlSize(m_FormHowToPlay, widthFactor, heightFactor);
            m_FormHowToPlay.ShowDialog();
        }

        private void setControlSize(Form i_Form ,int i_WidthFactor ,int i_HeightFactor)
        {
            i_Form.Width = this.Width - i_WidthFactor;
            i_Form.Height = this.Height - i_HeightFactor;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int heightFactor = k_Margin * GameUtils.k_AboutFormHeightFactorMultiplayer;
            int widthFactor = (k_Margin * GameUtils.k_AboutFormWidthFactorMultiplayer) - GameUtils.k_AboutFormWidthFactorAdjustment;
            setControlSize(m_FormAbout, widthFactor, heightFactor);
            m_FormAbout.ShowDialog();
        }

        private void timerFall_Tick(object sender, EventArgs e)
        {
            if(m_fallingCoin.Bottom < m_UIGameBoard[m_LastPlayerMove.SelectedRow, m_LastPlayerMove.SelectedColumn].Top)
            {
                m_fallingCoin.Top += 50;
                this.Refresh();
            }
            else
            {
                this.Controls.Remove(m_fallingCoin);
                m_fallingCoin.Dispose();
                timerFall.Stop();
                updateFormWithUserAction();
            }
        }
    }
}
