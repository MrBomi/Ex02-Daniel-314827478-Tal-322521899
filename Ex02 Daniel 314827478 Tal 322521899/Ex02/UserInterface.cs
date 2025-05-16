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
        private const ushort k_ColsInGameBoard = 8;
        private GuessingGameLogic<char> m_GameLogic = null;
        private const char m_CorrectSpotSign = 'V';
        private const char m_InCorrectSpotSign2 = 'X';

        public void StartGuessingGame()
        {
            bool isSecretCodeGuessed = false;
            bool isNewGame = false;
            char[] singleInputFromUser;
            bool isGameActive = true;
            bool isPlayerWon = false;

            while (isGameActive)
            {
                initGuessingGame();
                for (int i = 0; i < m_GameLogic.NumOfGuessings; i++)
                {
                    PrintBoard(false);
                    singleInputFromUser = getSingleGuessFromUser();
                    if (new string(singleInputFromUser) == "Q")
                    {
                        Console.WriteLine("Bye Bye...");
                        isGameActive = false;
                        return;
                    }
                    else
                    {
                        isSecretCodeGuessed = m_GameLogic.PlayerSingleTurn(singleInputFromUser, m_CorrectSpotSign, m_InCorrectSpotSign2);
                    }

                    if (isSecretCodeGuessed)
                    {
                        winningPlayerAnnouncement();
                        isPlayerWon = true;
                        break;
                    }
                }
                
                if(!isPlayerWon)
                {
                    loosingPlayerAnnouncement();
                }

                isNewGame = askForNewGameFromUser();
                if(isNewGame)
                {
                    isPlayerWon = false;
                    isSecretCodeGuessed = false;
                }
                else
                {
                    Console.Write("Bye Bye...");
                    isGameActive = false;
                }
            }
        }

        public void PrintBoard(bool i_IsSecretCodeRevealed)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            int arrayIndex = 0;
            GameBoard<char> boardToPrint = m_GameLogic.GameBoard;
            char[,] boardArray = boardToPrint.GameBoardArray;
            char[] computerSecretCode = m_GameLogic.ComputerSecretCode;

            Console.WriteLine("Current board status:\n");
            Console.WriteLine("|Pins:    |Result:|");
            Console.WriteLine("|=========|=======|");

            if (!i_IsSecretCodeRevealed)
            {
                Console.WriteLine("| # # # # |       |");
            }
            else
            {
                Console.WriteLine("| {0} {1} {2} {3} |       |", computerSecretCode[0], computerSecretCode[1], computerSecretCode[2], computerSecretCode[3]);
            }

            Console.WriteLine("|=========|=======|");
            foreach (char Letter in boardArray)
            {
                if (boardToPrint.IsStartOfARow(arrayIndex))
                {
                    Console.Write("| ");
                }
                else if (boardToPrint.IsStartOfAResult(arrayIndex))
                {
                    Console.Write("|");
                }

                if (boardToPrint.IsEndOfARow(arrayIndex + 1))
                {
                    Console.Write("{0}", Letter);
                    Console.WriteLine("|");
                    Console.WriteLine("|=========|=======|");
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

            while (!isCorrectInput)
            {
                isCorrectInput = ushort.TryParse(Console.ReadLine(), out numOfGuessings);

                if (!isCorrectInput)
                {
                    Console.WriteLine("Wrong input type, Please type positive number between 4-10)");
                }
                else if (numOfGuessings < 4 || numOfGuessings > 10)
                {
                    Console.WriteLine("The number youve entered should be positive between 4-10, please type again");
                    isCorrectInput = false;
                }

            }

            return numOfGuessings;
        }

        private static char[] getSingleGuessFromUser()
        {
            string singleGuess;

            Console.WriteLine("Please type your next guess <A B C D> or 'Q' to quit");
            singleGuess = Console.ReadLine();

            while (!isCorrectSingleGuessInput(singleGuess))
            {
                Console.WriteLine("Please type your next guess <A B C D> or 'Q' to quit");
                singleGuess = Console.ReadLine();
            }

            return singleGuess.ToCharArray();
        }

        private static bool isCorrectSingleGuessInput(string i_InputToCheck)
        {
            bool isCorrectInput = true;

            if (i_InputToCheck.Length != 4)
            {
                if (i_InputToCheck != "Q")
                {
                    isCorrectInput = false;
                    Console.WriteLine("The length of the input should be 4 chars");
                }
            }
            else if (hasDuplicateLetters(i_InputToCheck))
            {
                Console.WriteLine("The input you entered contains duplications - not allowed!");
                isCorrectInput = false;
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
            bool isInputContainDuplications = false;

            for (int i = 0; i < i_InputToCheck.Length; i++)
            {
                for (int j = i + 1; j < i_InputToCheck.Length; j++)
                {
                    if (i_InputToCheck[i] == i_InputToCheck[j])
                    {
                        isInputContainDuplications = true;
                        break;
                    }
                }
            }

            return isInputContainDuplications;
        }

        private char[] GenerateSecretCode()
        {
            Random rand = new Random();
            int secretCodeLength = 4;
            char[] secretCode = new char[secretCodeLength];
            bool isValidSecretCode = false;

            while (!isValidSecretCode)
            {
                for (int i = 0; i < secretCodeLength; i++)
                {
                    secretCode[i] = (char)('A' + rand.Next(8));
                }

                if (!hasDuplicateLetters(new string(secretCode)))
                {
                    isValidSecretCode = true;
                }
            }

            return secretCode;
        }

        private void initGuessingGame()
        {
            ushort numberOfGuessings;
            char[,] boardCharArray;
            char[] computerSecretCode;
            GameBoard<char> gameBoard;

            Console.WriteLine("Welcome to Guessing Game");
            computerSecretCode = GenerateSecretCode();
            numberOfGuessings = getNumberOfGuessingsFromUser();
            boardCharArray = new char[numberOfGuessings, k_ColsInGameBoard];
            GameBoard<char>.InitBoardArray(boardCharArray, ' ');
            gameBoard = new GameBoard<char>(boardCharArray, numberOfGuessings);

            this.m_GameLogic = new GuessingGameLogic<char>(gameBoard, numberOfGuessings, computerSecretCode);
        }

        private GameBoard<char> initGameBoard(ushort i_NumOfGuessings)
        {
            char[,] charArrayOfGameBoard = new char[i_NumOfGuessings, k_ColsInGameBoard];
            GameBoard<char> gameBoard = new GameBoard<char>(charArrayOfGameBoard, i_NumOfGuessings);

            return gameBoard;
        }

        private void winningPlayerAnnouncement()
        {
            PrintBoard(true);
            Console.WriteLine("You guessed after {0} steps!", m_GameLogic.NumOfGuessings);
        }
        private bool askForNewGameFromUser()
        {
            string input;

            Console.WriteLine("Would you like to start a new game? (Y/N)");
            input = Console.ReadLine();
            while (input != "Y" && input != "N" )
            {
                Console.WriteLine("Invalid input. Please enter Y or N:");
                input = Console.ReadLine();
            }

            return input == "Y";
        }

        private void loosingPlayerAnnouncement()
        {
            PrintBoard(true);
            Console.WriteLine("No more guessing allowed. You lost!");
        }
    }
}
