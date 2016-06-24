using System.Collections.Generic;
using System.Drawing;
using Ex06_GameLogic;

namespace Ex06_UI
{
    public class GameManager
    {
        private GameBoard m_GameBoard;
        private int m_TurnNumber = 0;

        public GameManager(int i_rows, int i_Columns)
        {
            m_GameBoard = new GameBoard(i_rows, i_Columns);
            m_TurnNumber = 0; 
        }
     
        public PlayerMove PlayTurn(int i_NextMove)
        {
            GameBoard.eBoardSquare playerSquare = m_TurnNumber % 2 == 0 ? GameBoard.eBoardSquare.Player1Square : GameBoard.eBoardSquare.Player2Square;
            --i_NextMove;
            m_TurnNumber++;
            return new PlayerMove(m_GameBoard.SetColumnSquare(i_NextMove, playerSquare), i_NextMove, m_GameBoard.BoardStatus);
        }

        public bool IsColumnFull(int i_selectedColumn)
        {
            return m_GameBoard.IsColumnFull(i_selectedColumn);
        }

        public IList<Point> FourInARowWiningPath
        {
            get
            {
                return m_GameBoard.FourInARowWiningPath;
            }
        }
    }
}