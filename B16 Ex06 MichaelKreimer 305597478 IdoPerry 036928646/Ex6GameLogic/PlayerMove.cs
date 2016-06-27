using System;
using System.Text;

namespace Ex06_GameLogic
{
    public class PlayerMove
    {
        private readonly int r_SelectedRow;
        private readonly int r_SelectedColumn;
        private readonly GameBoard.eBoardStatus r_GameStatus;

        public PlayerMove(int i_SelectedRow, int i_SelectedColumn, GameBoard.eBoardStatus i_GameStatus)
        {
            r_SelectedRow = i_SelectedRow;
            r_SelectedColumn = i_SelectedColumn;
            r_GameStatus = i_GameStatus;
        }

        public GameBoard.eBoardStatus GameStatus
        {
            get { return r_GameStatus; }
        }

        public int SelectedRow
        {
            get { return r_SelectedRow; }
        }

        public int SelectedColumn
        {
            get { return r_SelectedColumn; }
        }
    }
}