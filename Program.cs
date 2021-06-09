using System;
// Ta głębokość by podstawowy typ mieć
// using System.Collections.Generic;
namespace PP_Projekt_1_Tic_Tac_Toe
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Run();
        }
    }

    class Game
    {
        Players players = new Players();
        UI ui = new UI();

        public void Run()
        {
            while (true)
            {
                ui.Display();
                if (HandleMove_Win())
                if (ui.playAgain())
                    ui.Reset();
                else
                    break;
            }
            ui.Stats(players);
        }

        bool HandleMove_Win()
        {
            ui.ReadMove();
            Space winner = ui.Victory();
            if (winner != Space.Empty || ui.Tie())
            {
                players.Won(winner);
                return true;
            }
            return false;
        }
    }

    class Players
    {
        int[] score = new int[] { 0, 0, 0 };

        public int GetScore(Space player)
        {
            return score[(int)player];
        }

        public void Won(Space p)
        {
            int i = (int)p;
            score[i] += 1;
        }
    }

    class Board
    {
        Space[,] board;
        Space current;

        public Board() { Reset(); }
        public void Reset()
        {
            this.board = new Space[3, 3];
            this.current = Space.O;
        }

        public bool Move(int x, int y)
        {
            if (board[x, y] != Space.Empty) return false;

            board[x, y] = current;
            current = (current == Space.O) ? Space.X : Space.O;
            return true;
        }

        // Must be called after every Move
        public Space Victory()
        {
            if (CheckWin(1, 1) || CheckWin(1, 0) || CheckWin(0, 1) || CheckWin(-1, -1))
                return current == Space.O ? Space.X : Space.O;
            return Space.Empty;
        }

        bool CheckWin(int x, int y)
        {
            int width = 1 - Math.Abs(x);
            int height = 1 - Math.Abs(y);
            for (int i = 1 - width; i <= width + 1; i++)
                for (int j = 1 - height; j <= height + 1; j++)
                {
                    if (
                        board[i, j] == board[i - x, j - y] &&
                        board[i, j] == board[i + x, j + y] &&
                        board[i, j] != Space.Empty
                    )   return true;
                }

            return false;
        }

        public Space View(int x, int y)
        {
            return board[x, y];
        }

        public bool Tie()
        {
            for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == Space.Empty)
                    return false;
            }

            return true;
        }

        public Space currentPlayer()
        {
            return current;
        }
    }

    class UI : Board
    {
        public bool playAgain()
        {
            while (true)
            {
                Console.Write("Nowa gra? (Y/N) ");
                char key = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if (char.ToLower(key) == 'y')
                    return true;
                if (char.ToLower(key) == 'n')
                    return false;
            }
        }

        public void Display()
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    Console.Write(" " + RenderSpace(View(x, y)) + " ");
                    if (x != 2)
                        Console.Write("|");
                }
                Console.WriteLine("");
                if (y != 2) Console.WriteLine("---+---+---");
            }
        }

        void MovingNextAnnounce()
        {
            Console.WriteLine("Teraz rusza się: " + RenderSpace(currentPlayer()));
        }

        int ReadMoveKey()
        {
            while (true)
            {
                Console.Write("Podaj pole klawiaturą numeryczną: ");
                char key = Console.ReadKey().KeyChar;
                Console.WriteLine("");
                if (int.TryParse(key.ToString(), out int num)) return num;
                Console.WriteLine("Podany klawisz jest niepoprawny");
            }
        }

        int[] NumToPos(int num)
        {
            int x = (num - 1) % 3;
            int y = (9 - num) / 3;
            return new int[] {x,y};
        }

        public void ReadMove()
        {
            MovingNextAnnounce();
            while (true)
            {
                int num = ReadMoveKey();
                int[] m = NumToPos(num);
                if (Move(m[0], m[1]))
                    break;
                else
                    Console.WriteLine("Podane pole jest już zajęte");
            }
        }

        public void Stats(Players p)
        {
            Console.WriteLine("\n"
                + "Podsumowanie:" + "\n"
                + " Remis: " + p.GetScore(Space.Empty) + "\n"
                + " Wygrane kółka: " + p.GetScore(Space.O) + "\n"
                + " Wygrane krzyżyka: " + p.GetScore(Space.X));
        }

        string RenderSpace(Space space)
        {
            switch (space)
            {
                case Space.O:
                    return "O";
                case Space.X:
                    return "X";
                default:
                    return " ";
            }
        }
    }


    enum Space
    {
        Empty = 0,
        O = 1,
        X = 2
    }
}
