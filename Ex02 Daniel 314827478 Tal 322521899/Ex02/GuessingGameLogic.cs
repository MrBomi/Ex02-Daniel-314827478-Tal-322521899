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

        public bool PlayerSingleTurn(T[] i_PlayerSingleGuess, T i_CorrectSpotSign, T i_IncorrectSpotSign)
        {
            T[] singleGuessResutls;

            singleGuessResutls = calculateGuessResults(i_PlayerSingleGuess, i_CorrectSpotSign, i_IncorrectSpotSign);
            m_GameBoard.AppendSingleGuessToBoard(i_PlayerSingleGuess, m_CurrentRowToFill);
            m_GameBoard.AppendSingleGuessResultToBoard(singleGuessResutls, m_CurrentRowToFill);
            m_CurrentRowToFill++;

            return IsPlayerGuessCorrect(singleGuessResutls, i_CorrectSpotSign);
        }

        private T[] calculateGuessResults(T[] i_PlayerSingleGuess, T i_CorrectSpotSign, T i_IncorrectSpotSign)
        {
            LinkedList<T> guessResults = new LinkedList<T>();

            for (int i = 0; i < i_PlayerSingleGuess.Length; i++)
            {
                if (i_PlayerSingleGuess[i].Equals(r_computerSecretCode[i]))
                {
                    guessResults.AddFirst(i_CorrectSpotSign);
                    continue;
                }

                for (int j = 0; j < r_computerSecretCode.Length; j++)
                {
                    if((i != j) && (i_PlayerSingleGuess[i].Equals(r_computerSecretCode[j])))
                    {
                        guessResults.AddLast(i_IncorrectSpotSign);
                        break;
                    }
                }
            }

            return guessResults.ToArray();
        }

        public bool IsPlayerGuessCorrect(T[] i_PlayerSingleGuessResutls, T i_CorrectSpotSign)
        {
            bool isCorrectGuess = true;

            if (i_PlayerSingleGuessResutls.Length < k_SecretCodeLength)
            {
                isCorrectGuess = false;
            }
            else
            {
                for (int i = 0 ; i < i_PlayerSingleGuessResutls.Length ; i++)
                {
                    if (!i_PlayerSingleGuessResutls[i].Equals(i_CorrectSpotSign))
                    {
                        isCorrectGuess = false;
                        break;
                    }
                }
            }

            return isCorrectGuess;
        }


    }
}
