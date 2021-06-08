using System;
// Ta głębokość by podstawowy typ mieć
using System.Collections.Generic;

namespace PP_Projekt_1_Tic_Tac_Toe
{
    class Program
    {

        static void Main(string[] args)
        {
            bool play = true;
            int[] wygrane = new int[3];
            Game g = new Game();
            while (play)
            {
                g.Rysuj();
                Console.WriteLine("Gra: " + Game.TypGraczaToString(g.gracz));
                if (g.WykonajRuch())
                {
                    if (g.SprawdzCzyWygrana())
                    {
                        wygrane[(int)g.gracz] += 1;
                        g.Rysuj();
                        Console.WriteLine("Wygrał: " + Game.TypGraczaToString(g.gracz));
                        play = false;
                    }
                    else if (!g.SprawdzCzyWolne())
                    {
                        wygrane[(int)TypGracza.Pusta] += 1;
                        g.Rysuj();
                        Console.WriteLine("Remis");
                        play = false;
                    }
                    g.GraczSwap();
                }

                if (!play)
                {
                    while(true) {
                        Console.Write("Nowa gra? (Y/N) ");
                        char key = Console.ReadKey().KeyChar;
                        if(char.ToLower(key) == 'y') {
                            play = true;
                            g.ResetujPole();
                            Console.WriteLine();
                            break;
                        }
                        if(char.ToLower(key) == 'n') {
                            Console.WriteLine();
                            Console.WriteLine("Podsumowanie:");
                            Console.WriteLine(" Remis: "+wygrane[(int)TypGracza.Pusta]);
                            Console.WriteLine(" Wygrane krzyżyka: "+wygrane[(int)TypGracza.X]);
                            Console.WriteLine(" Wygrane kółka: "+wygrane[(int)TypGracza.O]);
                            break;
                        }
                        Console.WriteLine();
                    };
                    
                }
            }
        }
    }

    class Game
    {
        SekwencjaWygranej[] sekwencjeWygranych = Sekwencje();
        TypGracza[,] poleGry = new TypGracza[3, 3];
        public TypGracza gracz = TypGracza.X;

        public void ResetujPole()
        {
            this.gracz = TypGracza.X;
            this.poleGry = new TypGracza[3, 3];
        }


        public void Rysuj()
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    Console.Write(" " + TypGraczaToString(poleGry[x, y]) + " ");
                    if (x != 2)
                        Console.Write("|");
                }
                Console.WriteLine("");
                if (y != 2)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        Console.Write("---");
                        if (x != 2)
                            Console.Write("+");
                    }
                    Console.WriteLine("");
                }
            }
        }
        public bool WykonajRuch()
        {
            Console.Write("Podaj pole klawiaturą numeryczną: ");
            char key = Console.ReadKey().KeyChar;
            Console.WriteLine("");

            for (int i = 1; i <= 9; i++)
            {
                if (i.ToString() == key.ToString())
                {
                    int x = (i - 1) % 3;
                    int y = (i - 1) / 3;
                    y = 2 - y;

                    if (poleGry[x, y] == TypGracza.Pusta)
                    {
                        poleGry[x, y] = gracz;
                        return true;
                    }
                    else
                        return false;
                }
            }
            return false;
        }
        public bool SprawdzCzyWygrana()
        {
            for (int i = 0; i < sekwencjeWygranych.Length; i++)
            {
                if (sekwencjeWygranych[i].Check(gracz, poleGry))
                    return true;
            }
            return false;
        }

        public bool SprawdzCzyWolne()
        {
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    if (poleGry[x, y] == TypGracza.Pusta)
                        return true;
            return false;
        }

        public static string TypGraczaToString(TypGracza t)
        {
            if (t == TypGracza.Pusta)
                return " ";
            return t.ToString();
        }

        static SekwencjaWygranej[] Sekwencje()
        {
            List<SekwencjaWygranej> all = new List<SekwencjaWygranej> { };
            for (int i = 0; i < 3; i++)
            {
                all.Add(HackSekwencja(i, 0, 0, 1));
                all.Add(HackSekwencja(0, i, 1, 0));
            }
            all.Add(HackSekwencja(0, 0, 1, 1));
            all.Add(HackSekwencja(2, 0, -1, 1));

            return all.ToArray();
        }

        static SekwencjaWygranej HackSekwencja(int startx, int starty, int nextx, int nexty)
        {
            bool[,] map = new bool[3, 3];
            for (int i = 0; i < 3; i++)
            {
                int posx = startx + (nextx * i);
                int posy = starty + (nexty * i);
                map[posx, posy] = true;
            }

            return new SekwencjaWygranej(map);
        }

        public void GraczSwap()
        {
            if (gracz == TypGracza.X)
                this.gracz = TypGracza.O;
            else if (gracz == TypGracza.O)
                this.gracz = TypGracza.X;
        }
    }

    enum TypGracza
    {
        Pusta,
        X,
        O
    }

    class SekwencjaWygranej
    {
        public bool[,] map;

        public SekwencjaWygranej()
        {
            this.map = new bool[3, 3];
        }

        public SekwencjaWygranej(bool[,] map)
        {
            this.map = map;
        }

        public bool Check(TypGracza p, TypGracza[,] map)
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    bool same = map[x, y] == p;
                    if (this.map[x, y] && !same)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
