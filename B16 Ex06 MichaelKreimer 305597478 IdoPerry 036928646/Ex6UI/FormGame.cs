using System;
using System.Drawing;
using System.IO;
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
        private int m_TurnNumber;
        private int m_NumberOfHighlighTicks;
        private BoardTile[,] m_UiGameBoard;
        private BoardButton[] m_UiGameBoardButtons;
        private FloatingCoin m_MouseFollower;
        private GameManager m_GameManager;
        private FormHowToPlay m_FormHowToPlay;
        private FormAbout m_FormAbout;
        private PlayerMove m_LastPlayerMove;
        private FloatingCoin m_FallingCoin;
        private BoardButton m_LastCurrentSelectedButton;
        private FormYesNoMessageBox m_FormYesNoMessageBox;

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
                Close();
            }
            else
            {
                m_FormAbout = new FormAbout();
                m_FormYesNoMessageBox = new FormYesNoMessageBox();
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
            saveGameProperties();
            initializeStatusStrip();
            m_TurnNumber = 0;
            m_NumberOfHighlighTicks = 0;
            Controls.Remove(panelGameBoard);
            panelGameBoard.Height = r_GamePieceSize.Height * (m_NumberOfRows + 1);
            panelGameBoard.Width = r_GamePieceSize.Width * m_NumberOfColumns;
            Width = panelGameBoard.Width + (k_Margin * 2);
            Height = panelGameBoard.Height + (k_Margin * 3) + menuStrip1.Height + statusStrip1.Height;
            Controls.Add(panelGameBoard);
            panelGameBoard.SendToBack();
            m_GameManager = new GameManager(m_NumberOfRows, m_NumberOfColumns);
            initGameBoardButtons();
            initGameBoard();
            initMouseFollower();
            initFallingCoin();
        }

        private void initFallingCoin()
        {
            m_FallingCoin = new FloatingCoin(Resources.CoinRed.Height, Resources.CoinRed.Width);
            m_FallingCoin.Tick += timerFall_Tick;
            m_FallingCoin.Interval = 100;
        }

        private void initMouseFollower()
        {
            if (m_MouseFollower == null)
            {
                m_MouseFollower = new FloatingCoin(Resources.CoinRed.Height, Resources.CoinRed.Width);
            }

            m_MouseFollower.Image = getCurrentPlayerImage();
            m_MouseFollower.Tick += timerMouseFollow_Tick;
            m_MouseFollower.Interval = 10;
            m_MouseFollower.Location = Cursor.Position;
            Controls.Add(m_MouseFollower);
            m_MouseFollower.BringToFront();
            m_MouseFollower.Start();
        }

        private void initGameBoard()
        {
            m_UiGameBoard = new BoardTile[m_NumberOfRows, m_NumberOfColumns];
            for (int i = m_NumberOfRows - 1; i >= 0; --i)
            {
                for (int j = m_NumberOfColumns - 1; j >= 0; --j)
                {
                    m_UiGameBoard[i, j] = new BoardTile();
                    m_UiGameBoard[i, j].Top = panelGameBoard.Top + (m_UiGameBoard[i, j].Height * (i + 1));
                    m_UiGameBoard[i, j].Left = panelGameBoard.Left + (m_UiGameBoard[i, j].Width * j);
                    m_UiGameBoard[i, j].Enabled = false;
                    Controls.Add(m_UiGameBoard[i, j]);
                    m_UiGameBoard[i, j].BringToFront();
                }
            }
        }

        private void initGameBoardButtons()
        {
            m_UiGameBoardButtons = new BoardButton[m_NumberOfRows];
            for (int i = 0; i < m_NumberOfRows; i++)
            {
                m_UiGameBoardButtons[i] = new BoardButton();
                m_UiGameBoardButtons[i].Top = panelGameBoard.Top;
                m_UiGameBoardButtons[i].Left = panelGameBoard.Left + (i * m_UiGameBoardButtons[i].Width);
                m_UiGameBoardButtons[i].Text = (i + 1).ToString();
                m_UiGameBoardButtons[i].Click += BoardButton_Click;
                Controls.Add(m_UiGameBoardButtons[i]);
                m_UiGameBoardButtons[i].BringToFront();
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
            Close();
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_GameProperties.ShowDialog();
            if (m_GameProperties.DialogResult == DialogResult.OK)
            {
                m_FormYesNoMessageBox.Message = GameTexts.k_MessageStartNewGame;
                m_FormYesNoMessageBox.ShowDialog();
                if (m_FormYesNoMessageBox.DialogResult == DialogResult.Yes)
                {
                    resetGame();
                }
                else
                {
                    MessageBox.Show(
                        GameTexts.k_MessageSChangesWillEffectNextGame,
                        GameTexts.k_MessageBoxTitle,
                        MessageBoxButtons.OK);
                }
            }
        }

        private void BoardButton_Click(object sender, EventArgs e)
        {
            m_LastCurrentSelectedButton = sender as BoardButton;
            if (m_LastCurrentSelectedButton != null)
            {
                m_LastPlayerMove = m_GameManager.PlayTurn(int.Parse(m_LastCurrentSelectedButton.Text));
            }

            disableAllBoardButtons();
            dropCoin();
        }

        private void disableAllBoardButtons()
        {
            foreach (BoardButton boardButton in m_UiGameBoardButtons)
            {
                boardButton.Enabled = false;
            }
        }

        private void updateFormWithUserAction()
        {
            m_UiGameBoard[m_LastPlayerMove.SelectedRow, m_LastPlayerMove.SelectedColumn].Image =
                getCurrentPlayerTileImage();
            m_UiGameBoard[m_LastPlayerMove.SelectedRow, m_LastPlayerMove.SelectedColumn].Region = new Region();
            m_UiGameBoard[m_LastPlayerMove.SelectedRow, m_LastPlayerMove.SelectedColumn].BringToFront();
            enableAllBoardButtons();
            Refresh();
            if (m_GameManager.IsColumnFull(m_LastPlayerMove.SelectedColumn))
            {
                m_LastCurrentSelectedButton.Enabled = false;
            }

            m_TurnNumber++;
            checkBoardStatus(m_LastPlayerMove.GameStatus);
            initMouseFollower();
            setCurrentPlayerStatusStripText(m_PlayersInfo[m_TurnNumber % GameUtils.k_NumberOfPlayers].Name);
        }

        private void enableAllBoardButtons()
        {
            foreach (BoardButton boardButton in m_UiGameBoardButtons)
            {
                boardButton.Enabled = true;
            }
        }

        private void dropCoin()
        {
            m_FallingCoin.Image = getCurrentPlayerImage();
            m_FallingCoin.Location = m_LastCurrentSelectedButton.Location;
            disposeMouseFollower();
            Controls.Add(m_FallingCoin);
            m_FallingCoin.BringToFront();
            m_FallingCoin.Interval = 100;
            m_FallingCoin.Start();
            Cursor = Cursors.WaitCursor;
        }

        private void disposeMouseFollower()
        {
            m_MouseFollower.Stop();
            if (m_MouseFollower != null)
            {
                Controls.Remove(m_MouseFollower);
            }
        }

        private void checkBoardStatus(GameBoard.eBoardStatus i_GameStatus)
        {
            if (i_GameStatus != GameBoard.eBoardStatus.NextPlayerCanPlay)
            {
                if (i_GameStatus == GameBoard.eBoardStatus.PlayerWon)
                {
                    int playerNumber = (m_TurnNumber - 1) % GameUtils.k_NumberOfPlayers;
                    string playerWonMessage = string.Format(GameTexts.k_MessageWin, m_PlayersInfo[playerNumber].Name);
                    timerWiningPath.Start();
                    m_FormYesNoMessageBox.Message = playerWonMessage;
                    m_FormYesNoMessageBox.ShowDialog();
                    timerWiningPath.Stop();
                    m_PlayersInfo[playerNumber].Score += m_FormYesNoMessageBox.DialogResult == DialogResult.Yes ? 1 : 0;
                    updateStatusStripScore();
                    Refresh();
                }
                else
                {
                    m_FormYesNoMessageBox.Message = GameTexts.k_MessageTie;
                    m_FormYesNoMessageBox.ShowDialog();
                }

                if (m_FormYesNoMessageBox.DialogResult == DialogResult.No)
                {
                    Close();
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
                m_UiGameBoardButtons[i].Click -= BoardButton_Click;
                Controls.Remove(m_UiGameBoardButtons[i]);
                m_UiGameBoardButtons[i].Dispose();
            }
        }

        private void disposeGameBoard()
        {
            for (int i = m_NumberOfRows - 1; i >= 0; --i)
            {
                for (int j = m_NumberOfColumns - 1; j >= 0; --j)
                {
                    Controls.Remove(m_UiGameBoard[i, j]);
                    m_UiGameBoard[i, j].Dispose();
                }
            }
        }

        private Image getCurrentPlayerTileImage()
        {
            return m_TurnNumber % GameUtils.k_NumberOfPlayers == 0 ? Resources.FullCellRed : Resources.FullCellYellow;
        }

        private Image getCurrentPlayerImage()
        {
            return m_TurnNumber % GameUtils.k_NumberOfPlayers == 0 ? Resources.CoinRed : Resources.CoinYellow;
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

        private void setControlSize(Form i_Form, int i_WidthFactor, int i_HeightFactor)
        {
            i_Form.Width = Width - i_WidthFactor;
            i_Form.Height = Height - i_HeightFactor;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int heightFactor = k_Margin * GameUtils.k_AboutFormHeightFactorMultiplayer;
            int widthFactor = (k_Margin * GameUtils.k_AboutFormWidthFactorMultiplayer) -
                              GameUtils.k_AboutFormWidthFactorAdjustment;
            setControlSize(m_FormAbout, widthFactor, heightFactor);
            m_FormAbout.ShowDialog();
        }

        private void timerFall_Tick(object sender, EventArgs e)
        {
            if (m_FallingCoin.Bottom < m_UiGameBoard[m_LastPlayerMove.SelectedRow, m_LastPlayerMove.SelectedColumn].Top)
            {
                m_FallingCoin.Top += 50;
                Refresh();
            }
            else
            {
                Controls.Remove(m_FallingCoin);
                m_FallingCoin.Stop();
                updateFormWithUserAction();
                Cursor = Cursors.Default;
            }
        }

        private void timerWiningPath_Tick(object sender, EventArgs e)
        {
            foreach (Point point in m_GameManager.FourInARowWiningPath)
            {
                m_UiGameBoard[point.X, point.Y].Image = getBoardTileAlternativeImage();
            }
            ++m_NumberOfHighlighTicks;
            Refresh();
        }

        private Image getBoardTileAlternativeImage()
        {
            Image result;
            if (m_TurnNumber % GameUtils.k_NumberOfPlayers == 0)
            {
                result = m_NumberOfHighlighTicks % 2 == 0 ? Resources.FullCellYellowHighlight : Resources.FullCellYellow;
            }
            else
            {
                result = m_NumberOfHighlighTicks % 2 == 0 ? Resources.FullCellRedHighLight : Resources.FullCellRed;
            }

            return result;
        }

        private void timerMouseFollow_Tick(object sender, EventArgs e)
        {
            if (m_MouseFollower != null)
            {
                Point newMouseFollowerPosition = PointToClient(Cursor.Position);
                newMouseFollowerPosition.X += GameUtils.k_MouseFollowerXOffset;
                newMouseFollowerPosition.Y += GameUtils.k_MouseFollowerYOffset;
                m_MouseFollower.Location = newMouseFollowerPosition;
                m_MouseFollower.BringToFront();
            }
        }
    }
}