using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{ 
    public class GuessingGameLogic<T>
    {
        private GameBoard<T> m_GameBoard;
        private readonly ushort r_NumOfGuessings;

        public GuessingGameLogic(GameBoard<T> i_GameBoard, ushort i_NumOfGuessings)
        {
            m_GameBoard = i_GameBoard;
            r_NumOfGuessings = i_NumOfGuessings;
        }
    }
}
