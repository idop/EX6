using Ex6GameUtils;
using Ex6GameLogic;

namespace Ex6UI
{
    public class GameManager
    {
        private GameBoard m_GameBoard;
        private GameUtils.eGameMode m_GameMode;
        private int m_TurnNumber = 0;

        public GameManager(GameUtils.eGameMode i_GameMode, int i_rows, int i_Columns)
        {
            m_GameMode = i_GameMode;
            m_GameBoard = new GameBoard(i_rows, i_Columns);
            m_TurnNumber = 0; 
        }

        public void ResetGameBoard()
        {
            m_GameBoard.ClearBoard();
        }
     
        public PlayerMove PlayHumanTurn(int i_NextMove)
        {
            GameBoard.eBoardSquare playerSquare = m_TurnNumber % 2 == 0 ? GameBoard.eBoardSquare.Player1Square : GameBoard.eBoardSquare.Player2Square;
            --i_NextMove;
            m_TurnNumber++;
            return new PlayerMove(m_GameBoard.SetColumnSquare(i_NextMove, playerSquare), i_NextMove, playerSquare, m_GameBoard.BoardStatus);
        }

        public bool IsColumnFull(int i_selectedColumn)
        {
            return m_GameBoard.IsColumnFull(i_selectedColumn);
        }

        public void ResetGame()
        {
            m_TurnNumber = 0;
            m_GameBoard.ClearBoard();
        }
    }
}