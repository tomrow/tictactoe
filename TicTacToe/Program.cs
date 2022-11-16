using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class Program
    {
        //List<String> stringlist = new List<String>(4);
        public static String[] boardpieces = { " ", "x", "o", "Nobody" };

        public static int[,] board = new int[3, 3];
        public static bool PlayerTurn = true;
        List<Int16> list = new List<Int16>();

        static void Main(string[] args)
        {
            //bool GameStarted = true;
            Console.WriteLine(boardpieces[1]);
            int GameWonBy = 0;
            while(GameWonBy==0)
            {
                DrawBoard();
                PlaceSymbolOnBoard(PlayerTurn ? 1 : 2);
                PlayerTurn = !PlayerTurn;
                GameWonBy = CheckForWinner();
            }
            Console.WriteLine(boardpieces[GameWonBy]+" Won");
            DrawBoard();
            Console.ReadLine();
            
        }

        private static int CheckForWinner()
        {
            //Console.WriteLine("Checking for a winner");
            int WinnerDecision = 0;
            int EmptySpaceCount = 0;
            for (int i = 0; i < 9; i++)
            {
                EmptySpaceCount += (board[i / 3, i % 3] == 0) ? 1 : 0; //increment counter if tile is equal to 0
            }
            WinnerDecision = (EmptySpaceCount == 0) ? 3 : 0;
            for (int i=0; i<3; i++)
            {
                //Console.WriteLine((board[i, 0],board[i, 1],board[i, 2]));
                if ((board[i, 0] == board[i, 1]) && (board[i, 0] == board[i, 2]))
                {
                    if (board[i, 0] > 0) { WinnerDecision = board[i, 0]; }
                }
                if ((board[0,i] == board[1,i]) && (board[0,i] == board[2,i]))
                {
                    if (board[0,i] > 0) { WinnerDecision = board[0, i]; }
                }
            }
            if ((board[0, 0] == board[1, 1]) && (board[0, 0] == board[2, 2]))
            {
                if (board[1,1] > 0) { WinnerDecision = board[1, 1]; }
                
            }
            if ((board[2, 0] == board[1, 1]) && (board[2, 0] == board[0, 2]))
            {
                if (board[1, 1] > 0) { WinnerDecision = board[1, 1]; }
            }
            return WinnerDecision;
        }

        private static void PlaceSymbolOnBoard(int v)
        {
            Console.WriteLine("Player " + boardpieces[v] + "'s turn.  ");
            int x = -1;
            int y = -1;

            bool success = false;
            bool empty = false;
            while (!empty)
            {
                Console.WriteLine("Input column");
                success = Int32.TryParse(Console.ReadLine(), out x);
                while (!success || (x < 1) || (x > 3))
                {
                    Console.WriteLine("Invalid value, try again");
                    success = Int32.TryParse(Console.ReadLine(), out x);
                }
                Console.WriteLine("Input Row");
                success = Int32.TryParse(Console.ReadLine(), out y);
                while (!success || (y < 1) || (y > 3))
                {
                    Console.WriteLine("Invalid value, try again");
                    success = Int32.TryParse(Console.ReadLine(), out y);
                }
                empty = board[y - 1, x - 1] == 0;
                if (!empty) { Console.WriteLine("This tile is not empty, try again"); }
            }
            board[y - 1, x - 1] = v;
            Console.WriteLine("You have placed a " + boardpieces[v] + " piece on the board.");
            //DrawBoard();

        }

        private static void DrawBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(i == 0 ? "╭─┬─┬─╮" : "├─┼─┼─┤");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write("│"+boardpieces[board[i, j]]);
                }
                Console.Write("│\n");
            }
            Console.WriteLine("╰─┴─┴─╯");
        }
    }
}
