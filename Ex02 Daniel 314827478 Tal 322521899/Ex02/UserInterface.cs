using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    public class UserInterface
    {
        private const ushort k_SecretCodeLength = 4;
        private const ushort k_ColsInGameBoard = 8;
        private GuessingGameLogic<char> m_GameLogic = null;
        private char[] m_SecretCode = new char[k_SecretCodeLength];


        public void StartGuessingGame()
        {
            bool isGameRunning = true;

            initGuessingGame();
            while(isGameRunning)
            {
                PrintBoard(true);
            }



        }

        public void PrintBoard(bool i_IsAnswerRevealed)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            int arrayIndex = 0;
            GameBoard<char> board = m_GameLogic.GetBoard(); 
            char[,] boardArray = board.GetBoard();
            Console.WriteLine("Current board status:\n");
            Console.WriteLine("|Pins:    |Result:|");
            Console.WriteLine("|=========|=======|");


            if (!i_IsAnswerRevealed)
            {
                Console.WriteLine("| # # # # |       |");
            }
            else
            {
                Console.WriteLine("| {0} {1} {2} {3} |       |", m_SecretCode[0], m_SecretCode[1], m_SecretCode[2], m_SecretCode[3]);
            }

                foreach (char Letter in boardArray)
                {
                    if (board.IsStartOfARow(arrayIndex))
                    {
                        Console.Write("| ");
                    }
                    else if (board.IsStartOfAResult(arrayIndex))
                    {
                        Console.Write("|");
                    }

                    if (board.IsEndOfARow(arrayIndex + 1))
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
        private static ushort getNumberOfGuessingsFromUser()
        {
            ushort numOfGuessings = 0; //Was needed in case of failing in parse
            bool isCorrectInput = false;

            Console.WriteLine("Please type positive number (between 4-10) of guessing for your game");

            while(!isCorrectInput)
            {
                isCorrectInput = ushort.TryParse(Console.ReadLine(), out numOfGuessings);

                if(!isCorrectInput)
                {
                    Console.WriteLine("Wrong input type, Please type positive number between 4-10)");
                }
                else if(numOfGuessings < 4 || numOfGuessings > 10)
                {
                    Console.WriteLine("The number youve entered should be positive between 4-10, please type again");
                    isCorrectInput = false;
                }
                
            }

            return numOfGuessings;
        }

        private static string getSingleGuessFromUser()
        {
            string singleGuess;

            Console.WriteLine("Please type your next guess <A B C D> or 'Q' to quit");
            singleGuess = Console.ReadLine();

            while(!isCorrectSingleGuessInput(singleGuess))
            {
                Console.WriteLine("Please type your next guess <A B C D> or 'Q' to quit");
                singleGuess = Console.ReadLine();
            }

            return singleGuess;
        }

        private static bool isCorrectSingleGuessInput(string i_InputToCheck)
        {
            bool isCorrectInput = true;

            if (i_InputToCheck.Length != 4)
            {
                isCorrectInput = false;
                Console.WriteLine("The length of the input should be 4 chars");
            }
            else if (!hasDuplicateLetters(i_InputToCheck))
            {
                Console.WriteLine("The input you entered contains duplications - not allowed!");
            }
            else
            {
                foreach (char letterToCheck in i_InputToCheck)
                {
                    if (letterToCheck < 'A' || letterToCheck > 'H')
                    {
                        Console.WriteLine("The string should be letters between 'A' to 'H'");
                        isCorrectInput = false;
                        break;
                    }
                }
            }

            return isCorrectInput;
        }

        private static bool hasDuplicateLetters(string i_InputToCheck)
        {
            bool isCorrectInput = true;

            for (int i = 0; i < i_InputToCheck.Length; i++)
            {
                for (int j = i + 1; j < i_InputToCheck.Length; j++)
                {
                    if (i_InputToCheck[i] == i_InputToCheck[j])
                    {
                        isCorrectInput = false;
                        break;
                    }
                }
            }

            return isCorrectInput;
        }

        private void GenerateSecretCode()
        {
            Random rand = new Random();

            for (int i = 0; i < k_SecretCodeLength; i++)
            {
                m_SecretCode[i] = (char)('A' + rand.Next(26)); // אות רנדומלית בין A ל-Z
            }
        }

        private void initGuessingGame()
        {
            ushort numberOfGuessings;
            char[,] boardCharArray;
            GameBoard<char> gameBoard;

            GenerateSecretCode();
            Console.WriteLine("Welcome to Guessing Game");
            numberOfGuessings = getNumberOfGuessingsFromUser();
            boardCharArray = new char[numberOfGuessings, k_ColsInGameBoard];
            GameBoard<char>.InitFillArray(boardCharArray, ' ');
            gameBoard = new GameBoard<char>(boardCharArray, numberOfGuessings);

            this.m_GameLogic = new GuessingGameLogic<char>(gameBoard, numberOfGuessings);
        }

        private GameBoard<char> initGameBoard(ushort i_NumOfGuessings)
        {
            char[,] charArrayOfGameBoard = new char[i_NumOfGuessings, k_ColsInGameBoard];
            GameBoard<char> gameBoard = new GameBoard<char>(charArrayOfGameBoard, i_NumOfGuessings);

            return gameBoard; 
        }
    }
}
