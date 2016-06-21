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
    public class FormGame : Form
    {
        private const int k_Margin = 20;
        private const int k_ColumnSelectButtonWidth = 40;
        private const int k_ColumnSelectButtonHeight = 30;
        private const int k_BoardPieceButtonHeight = 40;
        private const int k_LabelPlayerInfoHeight = 30;
        private const int k_LabelPlayerInfoWidth = 120;
        private Button[] buttonsColumnsSelect;
        private Button[,] buttonsBoardPiece;
        private Label[] m_LabelPlayerInfo;
        private PlayerInfo[] m_PlayersInfo;
        private int m_NumberOfRows;
        private int m_NumberOfColumns;
        private int m_TurnNumber = 0;
        private FormGameProperties m_FromSettings;
        private GameUtils.eGameMode m_GameMode;
        private GameManager m_GameManager;

        public FormGame()
        {
            m_FromSettings = new FormGameProperties();
            m_FromSettings.ShowDialog();
            if (m_FromSettings.DialogResult == DialogResult.OK)
            {
                InitializeComponents();
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Abort;
            }
        }

        private void initializeGameSettingsValues()
        {
            m_NumberOfRows = m_FromSettings.Rows;
            m_NumberOfColumns = m_FromSettings.Coloumns;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Width = ((m_NumberOfColumns + 2) * k_Margin) + (k_ColumnSelectButtonWidth * m_NumberOfColumns);
            this.Height = ((m_NumberOfRows + 4) * k_Margin) + k_ColumnSelectButtonHeight + (k_BoardPieceButtonHeight * m_NumberOfRows) + k_LabelPlayerInfoHeight;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.Text = "4 in a Raw !!";
            m_PlayersInfo = new PlayerInfo[2];
            m_PlayersInfo[0] = new PlayerInfo(m_FromSettings.Player1Name);
            m_PlayersInfo[1] = new PlayerInfo(m_FromSettings.Player2Name);
        }

        private void initializePlayerInfoLabels()
        {
            m_LabelPlayerInfo = new Label[2];
            m_LabelPlayerInfo[0] = new Label();
            m_LabelPlayerInfo[0].Text = m_PlayersInfo[0].ToString();
            m_LabelPlayerInfo[0].Height = k_LabelPlayerInfoHeight;
            m_LabelPlayerInfo[0].Top = this.Height - (k_Margin * 2) - k_LabelPlayerInfoHeight;
            m_LabelPlayerInfo[0].TextAlign = ContentAlignment.TopRight;
            m_LabelPlayerInfo[0].Width = k_LabelPlayerInfoWidth;

            m_LabelPlayerInfo[1] = new Label();
            m_LabelPlayerInfo[1].Text = m_PlayersInfo[1].ToString();
            m_LabelPlayerInfo[1].Height = k_LabelPlayerInfoHeight;
            m_LabelPlayerInfo[1].Top = m_LabelPlayerInfo[0].Top;
            m_LabelPlayerInfo[1].TextAlign = ContentAlignment.TopLeft;
            m_LabelPlayerInfo[1].Width = k_LabelPlayerInfoWidth;

            m_LabelPlayerInfo[0].Left = (this.Width / 2) - k_Margin - m_LabelPlayerInfo[0].Width;
            this.Controls.Add(m_LabelPlayerInfo[0]);
            m_LabelPlayerInfo[1].Left = m_LabelPlayerInfo[0].Left + k_Margin + k_LabelPlayerInfoWidth;
            this.Controls.Add(m_LabelPlayerInfo[1]);
        }

        private void InitializeComponents()
        {
            initializeGameSettingsValues();
            initializeButtonsColumnsSelect();
            initializeButtonsBoardPiece();
            initializePlayerInfoLabels();
            initializeGameManager();
        }

        private void initializeGameManager()
        {
            m_GameManager = new GameManager(m_GameMode, m_NumberOfRows, m_NumberOfColumns);
        }

        private void initializeButtonsBoardPiece()
        {
            buttonsBoardPiece = new Button[m_NumberOfRows, m_NumberOfColumns];
            for (int i = m_NumberOfRows - 1; i >= 0; --i)
            {
                for (int j = m_NumberOfColumns - 1; j >= 0; --j)
                {
                    buttonsBoardPiece[i, j] = new Button();
                    buttonsBoardPiece[i, j].Top = (k_ColumnSelectButtonHeight + k_Margin) + (k_Margin * (i + 1)) + (k_BoardPieceButtonHeight * i);
                    buttonsBoardPiece[i, j].Left = (k_Margin * (j + 1)) + (k_ColumnSelectButtonWidth * j);
                    buttonsBoardPiece[i, j].Width = k_ColumnSelectButtonWidth;
                    buttonsBoardPiece[i, j].Height = k_BoardPieceButtonHeight;
                    buttonsBoardPiece[i, j].Cursor = Cursors.No;
                    buttonsBoardPiece[i, j].Font = new Font("Calibri", 14, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (byte) 0);
                    buttonsBoardPiece[i, j].Enabled = false;
                    this.Controls.Add(buttonsBoardPiece[i, j]);
                }
            }
        }

        private void initializeButtonsColumnsSelect()
        {
            buttonsColumnsSelect = new Button[m_NumberOfColumns];
            for (int i = 0; i < m_NumberOfColumns; ++i)
            {
                buttonsColumnsSelect[i] = new Button();
                buttonsColumnsSelect[i].Text = string.Format("{0}", i + 1);
                buttonsColumnsSelect[i].Top = k_Margin;
                buttonsColumnsSelect[i].Left = (k_Margin * (i + 1)) + (k_ColumnSelectButtonWidth * i);
                buttonsColumnsSelect[i].Width = k_ColumnSelectButtonWidth;
                buttonsColumnsSelect[i].Height = k_ColumnSelectButtonHeight;
                buttonsColumnsSelect[i].Click += buttonColumnSelect_Click;
                this.Controls.Add(buttonsColumnsSelect[i]);
            }
        }

        private void buttonColumnSelect_Click(object sender, EventArgs e)
        {
            Button currentSelectedButton = sender as Button;
            PlayerMove playerMove = m_GameManager.PlayHumanTurn(int.Parse(currentSelectedButton.Text));
            updateFormWithUserAction(currentSelectedButton, playerMove);
        }

        private void updateFormWithUserAction(Button i_CurrentSelectedButton, PlayerMove i_PlayerMove)
        {
            buttonsBoardPiece[i_PlayerMove.SelectedRow, i_PlayerMove.SelectedColumn].Text = i_PlayerMove.SquareSymbol.ToString();
            this.Refresh();
            if (m_GameManager.IsColumnFull(i_PlayerMove.SelectedColumn))
            {
                i_CurrentSelectedButton.Enabled = false;
            }

            checkBoardStatus(i_PlayerMove.GameStatus);
            m_TurnNumber++;
        }

        private void checkBoardStatus(GameBoard.eBoardStatus i_gameStatus)
        {
            if (i_gameStatus != GameBoard.eBoardStatus.NextPlayerCanPlay)
            {
                DialogResult playerWantsToPlayAgain;
                if (i_gameStatus == GameBoard.eBoardStatus.PlayerWon)
                {
                    int playerNumber = m_TurnNumber % 2;
                    playerWantsToPlayAgain = declareWinner(m_PlayersInfo[playerNumber].Name);
                    m_PlayersInfo[playerNumber].Score += playerWantsToPlayAgain.Equals(DialogResult.Yes) ? 1 : 0;
                    this.Refresh();
                    m_LabelPlayerInfo[playerNumber].Text = m_PlayersInfo[playerNumber].ToString();
                }
                else
                {
                    playerWantsToPlayAgain = declareDraw();
                }

                if (playerWantsToPlayAgain == DialogResult.No)
                {
                    Application.Exit();
                }
                else 
                {
                    resetGame();
                }
            }
        }

        private void resetGame()
        {
            m_TurnNumber = 0;
            m_GameManager.ResetGame();
            resetButtonsColumnsSelect();
            resetButtonsBoardPiece();
        }

        private void resetButtonsBoardPiece()
        {
            for (int i = m_NumberOfRows - 1; i >= 0; --i)
            {
                for (int j = m_NumberOfColumns - 1; j >= 0; --j)
                {
                    buttonsBoardPiece[i, j].Text = string.Empty;
                }
            }
        }

        private void resetButtonsColumnsSelect()
        {
            for (int i = 0; i < m_NumberOfColumns; i++)
            {
                buttonsColumnsSelect[i].Enabled = true;
            }
        }

        private DialogResult declareDraw()
        {
            string drawMessage = string.Format(
@"Tie!!
Another Round?");
            DialogResult dialogResult = MessageBox.Show(drawMessage, "A Tie!", MessageBoxButtons.YesNo);
            return dialogResult;
        }

        private DialogResult declareWinner(string i_WinnerName)
        {
            string playerWonMessage = string.Format(
 @"{0} Won!!
Another Round?", 
 i_WinnerName);
            DialogResult dialogResult = MessageBox.Show(playerWonMessage, "A Win!", MessageBoxButtons.YesNo);
            return dialogResult;
        }
    }
}