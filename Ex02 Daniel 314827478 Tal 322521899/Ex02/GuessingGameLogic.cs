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
        private const ushort k_SecretCodeLength = 4;
        private readonly ushort r_NumOfGuessings;
        private readonly T[] r_computerSecretCode = null;
        private ushort m_CurrentRowToFill = 0;

        public ushort NumOfGuessings
        {
            get
            {
                return r_NumOfGuessings;
            }
        }
        public T[] ComputerSecretCode
        {
            get
            {
                return r_computerSecretCode;
            }
        }

        public GameBoard<T> GameBoard
        {
            get
            {
                return m_GameBoard;
            }

            private set
            {
                m_GameBoard = value;
            }
        }

        public GuessingGameLogic(GameBoard<T> i_GameBoard, ushort i_NumOfGuessings, T[] i_ComputerSecretCode)
        {
            m_GameBoard = i_GameBoard;
            r_NumOfGuessings = i_NumOfGuessings;
            r_computerSecretCode = i_ComputerSecretCode;
        }

        public bool PlayerSingleTurn(T[] i_PlayerSingleGuess, T i_CorrectSign, T i_IncorrectSign)
        {
            T[] singleGuessResutls;

            singleGuessResutls = calculateGuessResults(i_PlayerSingleGuess, i_CorrectSign, i_IncorrectSign);

            m_GameBoard.AppendSingleGuessToBoard(i_PlayerSingleGuess, m_CurrentRowToFill);
            m_GameBoard.AppendSingleGuessResultToBoard(singleGuessResutls, m_CurrentRowToFill);
            m_CurrentRowToFill++;

            return false; // need to change
        }

        private T[] calculateGuessResults(T[] i_PlayerSingleGuess, T i_CorrectSign, T i_IncorrectSign)
        {
            return null;
        }
    }
}
