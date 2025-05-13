using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    public class Board
    {
        private const ushort k_Cols = 8;
        private int[,] m_GameBoard;
     
        public Board(int i_Rows) 
        {
            m_GameBoard = new int[i_Rows, k_Cols];
        }   
    }
}
