
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{

    public class GameBoard<T>
    {
        private const ushort k_Cols = 8;
        private readonly ushort r_Rows;
        private const ushort k_SingleGuessLength = 4;
        private T[,] m_GameBoardArray;

        public T[,] GameBoardArray
        {
            get
            {
                return m_GameBoardArray;
            }
        }

        public GameBoard(T[,] i_GameBoard, ushort i_NumOfGuessings)
        {
            r_Rows = i_NumOfGuessings; //num of guessing are the num of rows we need
            m_GameBoardArray = i_GameBoard;
        }

        public static void InitBoardArray<T>(T[,] o_Array, T i_Value)
        {
            for (int i = 0; i < o_Array.GetLength(0); i++)
            {
                for (int j = 0; j < o_Array.GetLength(1); j++)
                {
                    o_Array[i, j] = i_Value;
                }
            }
        }


        public void AddSingleGuessToBoard(T[] i_Guess, T[] i_Score, int i_RowIndex)
        {
            for (int i = 0; i < k_Cols; i++)
            {
                if (i < k_SingleGuessLength)
                {
                    m_GameBoardArray[i_RowIndex, i] = i_Guess[i];
                }
                else
                {
                    m_GameBoardArray[i_RowIndex, i] = i_Score[i - k_SingleGuessLength];
                }
            }
        }

        public void PrintBoard()
        {
            int arrayIndex = 0;
            Console.WriteLine("Current board status:\n");
            Console.WriteLine("|Pins:    |Result:|");
            Console.WriteLine("|=========|=======|");
            Console.WriteLine("| # # # # |       |");

            foreach (T Letter in m_GameBoardArray)
            {
                if (IsStartOfARow(arrayIndex))
                {
                    Console.Write("| ");
                }
                else if (IsStartOfAResult(arrayIndex))
                {
                    Console.Write("|");
                }

                if (IsEndOfARow(arrayIndex + 1))
                {
                    Console.Write("{0}", Letter);
                    Console.WriteLine("|");

                }
                else
                {
                    Console.Write("{0} ", Letter);
                }

                arrayIndex++;
            }
        }

        public bool IsStartOfARow(int i_Index)
        {
            if (i_Index % (k_Cols) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsStartOfAResult(int i_Index)
        {
            if (i_Index % (k_SingleGuessLength) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsEndOfARow(int i_Index)
        {
            if (i_Index % (k_Cols) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AppendSingleGuessToBoard(T[] i_PlayerSingleGuess, ushort i_CurrentRowToFill)
        {
            for (int i = 0 ; i < i_PlayerSingleGuess.Length ; i++)
            { 
                m_GameBoardArray[i_CurrentRowToFill, i] = i_PlayerSingleGuess[i];
            }
        }

        public void AppendSingleGuessResultToBoard(T[] i_PlayerSingleGuessResults, ushort i_CurrentRowToFill)
        {
            for (int i = 0 ; i < i_PlayerSingleGuessResults.Length ; i++)
            {
                m_GameBoardArray[i_CurrentRowToFill, i + k_SingleGuessLength] = i_PlayerSingleGuessResults[i];
            }
        }
    }
}