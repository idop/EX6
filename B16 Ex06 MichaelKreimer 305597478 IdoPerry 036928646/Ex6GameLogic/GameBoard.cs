﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace Ex06_GameLogic
{
    public class GameBoard
    {
        private const int k_NumberOfSquaresInARowNeededForVictory = 4;
        private List<Point> m_FourInARowPath;

        public enum eBoardSquare : byte
        {
            EmptySquare,
            Player1Square,
            Player2Square
        }

        public enum eBoardStatus : byte
        {
            NextPlayerCanPlay,
            Draw,
            PlayerWon
        }

        private eBoardStatus m_BoardStatus;
        private eBoardSquare[,] m_GameBoard;
        private int m_NumberOfEmptySquares;
        private int[] m_CurrentEmptyRowInColumn;

        public GameBoard(int i, int j)
        {
            m_GameBoard = new eBoardSquare[i, j];
            m_CurrentEmptyRowInColumn = new int[j];
            ClearBoard();
        }

        public int Rows
        {
            get { return m_GameBoard.GetLength(0); }
        }

        public int Columns
        {
            get { return m_GameBoard.GetLength(1); }
        }

        public IList<Point> FourInARowWiningPath
        {
            get { return m_FourInARowPath.AsReadOnly(); }
        }

        public eBoardStatus BoardStatus
        {
            get { return m_BoardStatus; }
        }

        public void ClearBoard()
        {
            m_NumberOfEmptySquares = Rows * Columns;

            for (int i = 0; i < Columns; ++i)
            {
                m_CurrentEmptyRowInColumn[i] = Rows;
                for (int j = 0; j < Rows; ++j)
                {
                    m_GameBoard[j, i] = eBoardSquare.EmptySquare;
                }
            }

            m_BoardStatus = eBoardStatus.NextPlayerCanPlay;
        }

        public int SetColumnSquare(int i_ColumnIndex, eBoardSquare i_PlayerSquare)
        {
            int currentRow = m_CurrentEmptyRowInColumn[i_ColumnIndex] - 1;
            m_GameBoard[currentRow, i_ColumnIndex] = i_PlayerSquare;
            --m_NumberOfEmptySquares;
            setNewBoardStatus(i_ColumnIndex);
            --m_CurrentEmptyRowInColumn[i_ColumnIndex];
            return currentRow;
        }

        private void setNewBoardStatus(int i_LastInsertedColumn)
        {
            bool playerWon = checkIfPlayerWon(i_LastInsertedColumn);
            if (playerWon)
            {
                m_BoardStatus = eBoardStatus.PlayerWon;
            }
            else if (m_NumberOfEmptySquares == 0)
            {
                m_BoardStatus = eBoardStatus.Draw;
            }
        }

        public bool IsColumnFull(int i_ColumnIndex)
        {
            return m_CurrentEmptyRowInColumn[i_ColumnIndex] == 0;
        }

        private bool checkIfPlayerWon(int i_LastInsertedColumn)
        {
            int lastInstertedRow = m_CurrentEmptyRowInColumn[i_LastInsertedColumn] - 1;
            eBoardSquare currentPlayerSquare = m_GameBoard[lastInstertedRow, i_LastInsertedColumn];
            bool playerWon = checkCurrentColumn(lastInstertedRow, i_LastInsertedColumn, currentPlayerSquare);
            if (!playerWon)
            {
                playerWon = checkCurrentRow(lastInstertedRow, i_LastInsertedColumn, currentPlayerSquare);
                if (!playerWon)
                {
                    playerWon = checkCurrentDiagonals(lastInstertedRow, i_LastInsertedColumn, currentPlayerSquare);
                }
            }

            return playerWon;
        }

        private bool checkCurrentColumn(
            int i_LastInstertedRow,
            int i_LastInsertedColumn,
            eBoardSquare currentPlayerSquare)
        {
            bool playerWon = false;
            int maxNumberofSquaresInARow = 1;
            initFourInARowPath(i_LastInstertedRow, i_LastInsertedColumn);
            for (int i = i_LastInstertedRow + 1; i < Rows && !playerWon; ++i)
            {
                if (m_GameBoard[i, i_LastInsertedColumn] == currentPlayerSquare)
                {
                    ++maxNumberofSquaresInARow;
                    m_FourInARowPath.Add(new Point(i, i_LastInsertedColumn));
                }
                else
                {
                    m_FourInARowPath = null;
                    break;
                }

                if (maxNumberofSquaresInARow == k_NumberOfSquaresInARowNeededForVictory)
                {
                    playerWon = true;
                }
            }

            return playerWon;
        }

        private bool checkCurrentRow(int i_LastInstertedRow, int i_LastInsertedColumn, eBoardSquare currentPlayerSquare)
        {
            bool playerWon = false;
            int maxNumberofSquaresInARow = 0;
            initFourInARowPath(i_LastInstertedRow, i_LastInsertedColumn);
            for (int i = 0; i < Columns && !playerWon; ++i)
            {
                if (m_GameBoard[i_LastInstertedRow, i] == currentPlayerSquare)
                {
                    m_FourInARowPath.Add(new Point(i_LastInstertedRow, i));
                    ++maxNumberofSquaresInARow;
                }
                else
                {
                    m_FourInARowPath = null;
                    break;
                }

                if (maxNumberofSquaresInARow == k_NumberOfSquaresInARowNeededForVictory)
                {
                    playerWon = true;
                }
            }

            return playerWon;
        }

        private void initFourInARowPath(int i_LastInstertedRow, int i_LastInsertedColumn)
        {
            m_FourInARowPath = new List<Point>();
            m_FourInARowPath.Add(new Point(i_LastInstertedRow, i_LastInsertedColumn));
        }

        private bool checkCurrentDiagonals(
            int i_LastInstertedRow,
            int i_LastInsertedColumn,
            eBoardSquare currentPlayerSquare)
        {
            bool playerWon = checkLeftDiagonal(i_LastInstertedRow, i_LastInsertedColumn, currentPlayerSquare);
            if (!playerWon)
            {
                playerWon = checkRightDiagonal(i_LastInstertedRow, i_LastInsertedColumn, currentPlayerSquare);
            }

            return playerWon;
        }

        private bool checkRightDiagonal(
            int i_LastInstertedRow,
            int i_LastInsertedColumn,
            eBoardSquare currentPlayerSquare)
        {
            bool playerWon = false;
            bool isSameSquare = true;
            int maxNumberofSquaresInARow = 1;
            int diagonalRowIndex = i_LastInstertedRow + 1;
            int diagonalColumnIndex = i_LastInsertedColumn - 1;
            initFourInARowPath(i_LastInstertedRow, i_LastInsertedColumn);
            while (diagonalRowIndex < Rows && diagonalColumnIndex >= 0 && !playerWon && isSameSquare)
            {
                if (m_GameBoard[diagonalRowIndex, diagonalColumnIndex] == currentPlayerSquare)
                {
                    ++maxNumberofSquaresInARow;
                    m_FourInARowPath.Add(new Point(diagonalRowIndex, diagonalColumnIndex));
                }
                else
                {
                    isSameSquare = false;
                }

                if (maxNumberofSquaresInARow == k_NumberOfSquaresInARowNeededForVictory)
                {
                    playerWon = true;
                }
                --diagonalColumnIndex;
                ++diagonalRowIndex;
            }

            isSameSquare = true;
            diagonalRowIndex = i_LastInstertedRow - 1;
            diagonalColumnIndex = i_LastInsertedColumn + 1;

            while (diagonalRowIndex >= 0 && diagonalColumnIndex < Columns && !playerWon && isSameSquare)
            {
                if (m_GameBoard[diagonalRowIndex, diagonalColumnIndex] == currentPlayerSquare)
                {
                    ++maxNumberofSquaresInARow;
                    m_FourInARowPath.Add(new Point(diagonalRowIndex, diagonalColumnIndex));
                }
                else
                {
                    isSameSquare = false;
                    m_FourInARowPath = null;
                }

                if (maxNumberofSquaresInARow == k_NumberOfSquaresInARowNeededForVictory)
                {
                    playerWon = true;
                }
                ++diagonalColumnIndex;
                --diagonalRowIndex;
            }

            return playerWon;
        }

        private bool checkLeftDiagonal(
            int i_LastInstertedRow,
            int i_LastInsertedColumn,
            eBoardSquare currentPlayerSquare)
        {
            bool playerWon = false;
            bool isSameSquare = true;
            int maxNumberofSquaresInARow = 1;
            int diagonalRowIndex = i_LastInstertedRow + 1;
            int diagonalColumnIndex = i_LastInsertedColumn + 1;
            initFourInARowPath(i_LastInstertedRow, i_LastInsertedColumn);
            while (diagonalRowIndex < Rows && diagonalColumnIndex < Columns && !playerWon && isSameSquare)
            {
                if (m_GameBoard[diagonalRowIndex, diagonalColumnIndex] == currentPlayerSquare)
                {
                    ++maxNumberofSquaresInARow;
                    m_FourInARowPath.Add(new Point(diagonalRowIndex, diagonalColumnIndex));
                }
                else
                {
                    isSameSquare = false;
                }

                if (maxNumberofSquaresInARow == k_NumberOfSquaresInARowNeededForVictory)
                {
                    playerWon = true;
                }
                ++diagonalColumnIndex;
                ++diagonalRowIndex;
            }

            isSameSquare = true;
            diagonalRowIndex = i_LastInstertedRow - 1;
            diagonalColumnIndex = i_LastInsertedColumn - 1;

            while (diagonalRowIndex >= 0 && diagonalColumnIndex >= 0 && !playerWon && isSameSquare)
            {
                if (m_GameBoard[diagonalRowIndex, diagonalColumnIndex] == currentPlayerSquare)
                {
                    ++maxNumberofSquaresInARow;
                    m_FourInARowPath.Add(new Point(diagonalRowIndex, diagonalColumnIndex));
                }
                else
                {
                    isSameSquare = false;
                    m_FourInARowPath = null;
                }

                if (maxNumberofSquaresInARow == k_NumberOfSquaresInARowNeededForVictory)
                {
                    playerWon = true;
                }
                --diagonalColumnIndex;
                --diagonalRowIndex;
            }

            return playerWon;
        }
    }
}