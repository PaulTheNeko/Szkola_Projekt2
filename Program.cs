using System;
// Ta głębokość by podstawowy typ mieć
using System.Collections.Generic;

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
        int[] score = new int[] {0, 0};
        int ties = 0;
        
        public int getScore(bool player)
        {
            if(player)
                return score[0];
            else
                return score[1];
        }

        public int won(bool Player)
        {
            
        }
    }

    class Board
    {

    }

    class UI
    {

        public bool playAgain()
        {
            while(true) {
                        Console.Write("Nowa gra? (Y/N) ");
                        char key = Console.ReadKey().KeyChar;
                        Console.WriteLine();

                        if(char.ToLower(key) == 'y') {
                            return true;
                        }
                        if(char.ToLower(key) == 'n') {
                            return false;
                        }
            };
        }
    }
}
