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
            game.run();
        }
    }

    class Game
    {
        Board board;
        UI ui;

        public void run()
        {
        }
    }

    class Players
    {
        int[] score = new int[] { 0, 0, 0 };

        public int getScore(Space player)
        {
            return score[(int) player];
        }

        /* public int won(bool Player)
        {
            
        } */
        Space boolToSpace(bool player)
        {
            if (player)
                return Space.O;
            else
                return Space.X;
        }
    }

    class Board
    {
        Space[,] board = new Space[3, 3];
        Space current = Space.O;


        // bool move()
    }

    class UI
    {
        public bool playAgain()
        {
            while (true)
            {
                Console.Write("Nowa gra? (Y/N) ");
                char key = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if (char.ToLower(key) == 'y')
                {
                    return true;
                }
                if (char.ToLower(key) == 'n')
                {
                    return false;
                }
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
