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
        static String[] boardpieces = { " ", "x", "o", "Nobody" };
        static int[,] board = new int[3, 3];
        static bool PlayerTurn = true;
        List<Int16> list = new List<Int16>();

        static void Main(string[] args)
        {
            Console.ResetColor();
            if (args.Length < 1)
                RMain();
            else
                TMain();
        }
        static void TMain()
        {
            //bool GameStarted = true;
            //Console.WriteLine(boardpieces[1]);
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
            for (int i = 0; i < 3; i++) //column
            {
                Console.WriteLine(i == 0 ? "┌─┬─┬─┐" : "├─┼─┼─┤");
                for (int j = 0; j < 3; j++)  //row
                {
                    Console.Write("│"+boardpieces[board[i, j]]);
                }
                Console.Write("│\n");
            }
            Console.WriteLine("└─┴─┴─┘");
        }

        static char[] rGridArray = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static int rPlayerNumber = 1;
        static int rPlayerLocation;
        static int rFlag;

        private static void RMain()
        {
            do
            {
                Console.WriteLine("TicTacToe");
                Console.WriteLine("Player 1 = X, Player 2 = 0. \nType the index of a grid square and press Return to place a piece on the board.");
                RDrawBoard();
                bool rPlayer2Turn = (rPlayerNumber % 2 == 0);
                Console.WriteLine(rPlayer2Turn ? "Player 2's Turn" : "Player 1's Turn");
                rPlayerLocation = 0;
                bool rSuccess = Int32.TryParse(Console.ReadLine(), out rPlayerLocation);
                while (!rSuccess || (rPlayerLocation < 1) || (rPlayerLocation > 9
                    ))
                {
                    Console.WriteLine("Type a valid index on the board to place your piece");
                    rSuccess = Int32.TryParse(Console.ReadLine(), out rPlayerLocation);
                }
                if ((rGridArray[rPlayerLocation] != 'x') && (rGridArray[rPlayerLocation] != 'o'))
                {
                    rGridArray[rPlayerLocation] = rPlayer2Turn ? 'o' : 'x';
                    rPlayerNumber = ((rPlayerNumber + 1) % 2);
                }
                else
                {
                    Console.WriteLine($"The square at {rPlayerLocation} is occupied, try another.");
                    Thread.Sleep(2000);
                }
                rFlag = RCheckForWinner();
            } while (rFlag == 0);
            Console.Clear();
            RDrawBoard();
            if (rFlag == 1)
            //{ Console.WriteLine(rPlayerNumber.ToString()+" lost")}
            {
                //Console.WriteLine(RCheckWhichWinner() == 'x' ? "X Won" : RCheckWhichWinner() == 'o' ? "O Won" : "Nobody Wins");
                Console.WriteLine($"Congrats, Player { (rPlayerNumber % 2)+1 } Wins");

            }
            else 
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine("You Lousy!");
                Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&WWMM#*oooooooooZf]<>>>~[jmoo*MMMW&&&&&");
                Console.WriteLine("&&&&&&&&&&&&&&&&&&&&WMMM*ooooooooom)>>+?]]+>>_|Xdoo#MMMW&&&");
                Console.WriteLine("&&&&&&&&&&&&&&&&&&&&WM*ooooooooobv}+>>+?]]]]][{|jUkoo#MW&&&");
                Console.WriteLine("&&&&&&&&&&&&&&&&&&WMMMMM*oooooaX{_>>>>+?]]{))))))vk#MMMMWW&");
                Console.WriteLine("&&&&&&&&MqCXLo&&&&WMMMMM*oooooaX{_>>>>+?})?>>>>+{vk#MMMMMW&");
                Console.WriteLine("&&&&&&&&MqUjtu0#WMMMMMMMMMMMMM#Lrrrrrr|[})?>>>>-/Uk#MMMMMW&");
                Console.WriteLine("&&&&&&&&MqUj))fuOMMMMMMMMMM#oooooooobOQQQQzj|))|jJoMMMMMMW&");
                Console.WriteLine("&&&&&&&&MqCXx|))fvwMMMMMM#*oooooooooooooooqQQQQmoMMMMMMMMW&");
                Console.WriteLine("&&&&&&&&MqQQCzr|fvwMMMMMMMMMMMM#ooooooooooo*#MMMMMMMMMMMMW&");
                Console.WriteLine("&&&&&&&&#0zCQQz|))vLcxuuxrrL#MMMMMMMMMMMMM#oo*#MMMMMMMMMMW&");
                Console.WriteLine("&&&&&&&&#0vvXCQQUvUCr(xux(+>>{rXQQZaMMMMMMMMMMMMMMMMMMMW&&&");
                Console.WriteLine("&&&&&&&&&&oCXCQQQQQCr(xux(+>>c#LnUCzvvYLpMMMMMMMMMMMMZcUOa&");
                Console.WriteLine("&&&&&&&&&&oCx|zobQQCr))j0C/]<{xLwXuJQQQQXr}~)cdMMMMwcvcUOa&");
                Console.WriteLine("&&&&&&&&&&oCx|rCpMpLcj||jXwpppqYnYQQQQYx0MQnQppb*kOUcUQQOa&");
                Console.WriteLine("&&&&&&&&&&oCvvr|UMM#qJj))))))uQQQQJccCQQUvQppqOmokOQQQ0h&&&");
                Console.WriteLine("&&&&&&&&&&oCXCz|nQb#qCXn/))xLYuYCznnnnzLZppwutXmokOQQbMYfq&");
                Console.WriteLine("&&&&&&&&W#*#aZz|))QMM*wQQJzYLQQQCznnnnzLv))))tXmokOQQznw&&&");
                Console.WriteLine("&&&Moooooo*#MMk0UvU0kMMMMMMMMM#qCznnnnzLv))|zZaaZQQpMW&&&&&");
                Console.WriteLine("oooooooooooo*MMMwcvzqowJurrcLQQQQQJccCQQQQQ0kMMMMMMW&&&&&&&");
                Console.WriteLine("oooooooooooo*MMMQ(jzqMMaZUnt))))|zQQQQq#MMMMMMMMMW&&&&&&&&&");
                Console.WriteLine("ooooo#MMMMMMMMMMQ(jvncpMMMMbQXrrrrrrrrCoMMMMMMMWW&&&&&&&&&&");
                Console.WriteLine("MMMMMMMMMMMMMMMMwcnj[_(XkMMMMMMMMMMMMMMMdQQ0kM*oooo#&&&&&&&");
                Console.WriteLine("MMMMMMMMMMMMMMMMbQYut(fXkkOQQQQbMMMMMMk0QQQ0kMM#*ooooooM&&&");
                Console.WriteLine("&&&&&&&&&WMMMMMMMMdLct??rLobQQQQOkMMwf/jncwMMMMMMMMMM#ooooo");
                Console.WriteLine("&&&&&&&WMMMMMMMMMMdLYx]>>[x0#MMMMMkXrrcLQQbMMMMMMMMMMMMMM*o");
                Console.WriteLine("&&&&&&&WMMMMMM#*#MM*0ut))[~>>?)trr/-?tcLd&&&&&&WMMMMMMMMMMM");
                Console.WriteLine("&&&&&J]]1OMMMM#*#MM#qCXn/))))?>>~[(fvCpM&&&&&&&&&&&WMMMMMMM");
                Console.WriteLine("&&&&&wv|}jUkMM#*#MM#qQQCzx|)))))|xzCw#&&&&&&&&&&&&&&&WMMMMM");
                Console.WriteLine("&&&&&wvvvvUkMM#ooo#*0vvvvvvvvUQQQQZaWW&&&&&&&&&&&&&&&&&WMMM");
                Console.WriteLine("&&&&&&&WMMMMMMMM#o#*0vvn/))fvUQQOhMMW&&&&&&&&&&&&&&&&&&&WWM");
                Console.WriteLine("&&&&&&&WMMMMMMMMMMM*0ut))))fvvcUOhMMW&&&&&&&&&&&&&&&&&&&&&&");
                Console.WriteLine("&&&&&WMMMMMMMMMMMMM*0ut))))fvU0bMMMMW&&&&&&&&&&&&&&&&&&&&&&");
            }
            Console.ReadLine();
        }
        private static int RCheckForWinner()
        {
            int rEmptyCounter = 0;
            for (int s = 1; s < 10; s++)
            {
                if (rGridArray[s] == s + 48) //if a space is empty
                { rEmptyCounter++; } //increment this counter
            }

            #region Horizontal Win
            if ((rGridArray[1] == rGridArray[2]) && (rGridArray[1] == rGridArray[3]))
            { return 1; }
            else if ((rGridArray[4] == rGridArray[5]) && (rGridArray[4] == rGridArray[6]))
            { return 1; }
            else if ((rGridArray[7] == rGridArray[8]) && (rGridArray[7] == rGridArray[9]))
            { return 1; }
            #endregion
            #region Vertical Win
            else if ((rGridArray[1] == rGridArray[4]) && (rGridArray[1] == rGridArray[7]))
            { return 1; }
            else if ((rGridArray[2] == rGridArray[5]) && (rGridArray[2] == rGridArray[8]))
            { return 1; }
            else if ((rGridArray[3] == rGridArray[6]) && (rGridArray[3] == rGridArray[9]))
            { return 1; }
            #endregion
            #region Diagonal Win
            else if ((rGridArray[1] == rGridArray[5]) && (rGridArray[1] == rGridArray[9]))
            { return 1; }
            else if ((rGridArray[3] == rGridArray[5]) && (rGridArray[3] == rGridArray[7]))
            { return 1; }
            #endregion
            else if (rEmptyCounter == 0)
            { return -1; }
            else
            { return 0; }
        }
        private static int RCheckWhichWinner()
        {
            int rEmptyCounter = 0;
            for (int s = 1; s < 10; s++)
            {
                if (rGridArray[s] == s + 48) //if a space is empty
                { rEmptyCounter++; } //increment this counter
            }

            #region Horizontal Win
            if ((rGridArray[1] == rGridArray[2]) && (rGridArray[1] == rGridArray[3]))
            { return rGridArray[1]; }
            else if ((rGridArray[4] == rGridArray[5]) && (rGridArray[4] == rGridArray[6]))
            { return rGridArray[4]; }
            else if ((rGridArray[7] == rGridArray[8]) && (rGridArray[7] == rGridArray[9]))
            { return rGridArray[7]; }
            #endregion
            #region Vertical Win
            else if ((rGridArray[1] == rGridArray[4]) && (rGridArray[1] == rGridArray[7]))
            { return rGridArray[1]; }
            else if ((rGridArray[2] == rGridArray[5]) && (rGridArray[2] == rGridArray[8]))
            { return rGridArray[2]; }
            else if ((rGridArray[3] == rGridArray[6]) && (rGridArray[3] == rGridArray[9]))
            { return rGridArray[3]; }
            #endregion
            #region Diagonal Win
            else if ((rGridArray[1] == rGridArray[5]) && (rGridArray[1] == rGridArray[9]))
            { return rGridArray[5]; }
            else if ((rGridArray[3] == rGridArray[5]) && (rGridArray[3] == rGridArray[7]))
            { return rGridArray[5]; }
            #endregion
            else if (rEmptyCounter == 0)
            { return -1; }
            else
            { return 0; }
        }
        private static void RDrawBoard()
        {
            for (int i = 0; i < 3; i++)
            {             //row
                Console.WriteLine(i == 0 ? "┌─┬─┬─┐" : "├─┼─┼─┤");
                for (int j = 1; j < 4; j++)
                {         //column
                    Console.Write("│" + rGridArray[(i * 3) + j]);
                }
                Console.Write("│\n");
            }
            Console.WriteLine("└─┴─┴─┘");
            Console.WriteLine("Push Return to continue");
            Console.ReadLine();
        }



    }
}
