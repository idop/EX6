﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ex06_GameLogic
{
    public class PlayerMove
    {
        private int m_SelectedRow;
        private int m_SelectedColumn;
        private GameBoard.eBoardStatus m_GameStatus;

        public PlayerMove(int i_SelectedRow, int i_SelectedColumn, GameBoard.eBoardStatus i_GameStatus)
        {
            m_SelectedRow = i_SelectedRow;
            m_SelectedColumn = i_SelectedColumn;
            m_GameStatus = i_GameStatus;
        }

        public GameBoard.eBoardStatus GameStatus
        {
            get
            {
                return m_GameStatus;
            }
        }

        public int SelectedRow
        {
            get
            {
                return m_SelectedRow;
            }
        }

        public int SelectedColumn
        {
            get
            {
                return m_SelectedColumn;
            }
        }
    }
}
