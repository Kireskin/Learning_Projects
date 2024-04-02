using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Console
{
    internal class TicTacToe
    {
        private char[,] _board;
        private bool _player;
        private int _turns;
        public char[,] Board
        {
            get { return _board; }
            set { _board = value; }
        }

        public TicTacToe()
        {
            _board = new char[3, 3] { { '1', '2', '3' },
                                        { '4', '5', '6' },
                                        { '7', '8', '9' }};
        }

        public void Start()
        {
            while (!Checker() && _turns++ < 9)
            {
                Display();
                Play();
                Console.Clear();
            }

            Display();
            Finalyze();
        }

        private void setBoard()
        {
            Board = new char[3, 3] { { '1', '2', '3' },
                                        { '4', '5', '6' },
                                        { '7', '8', '9' }};
        }

        public void Display()
        {
            Console.WriteLine("   |   |   ");
            Console.WriteLine(" {0} | {1} | {2} ", Board[0, 0], Board[0, 1], Board[0, 2]);
            Console.WriteLine("___|___|___");
            Console.WriteLine("   |   |   ");
            Console.WriteLine(" {0} | {1} | {2} ", Board[1, 0], Board[1, 1], Board[1, 2]);
            Console.WriteLine("___|___|___");
            Console.WriteLine("   |   |   ");
            Console.WriteLine(" {0} | {1} | {2} ", Board[2, 0], Board[2, 1], Board[2, 2]);
            Console.WriteLine("   |   |   ");
        }

        public void Play()
        {
            if (!_player)
            {
                int inputInt;
                Console.Write("Player 1: Choose your field! ");
                while (!Int32.TryParse(Console.ReadLine(), out inputInt) ||
                       (inputInt < 1 || inputInt > 9) ||
                       (Board[(inputInt - 1) / 3, (inputInt - 1) % 3] == 'X' ||
                        Board[(inputInt - 1) / 3, (inputInt - 1) % 3] == 'O'))
                {
                    Console.WriteLine("Please enter a number!\n");
                    Console.WriteLine(" Incorrect input! Please use another field!\n");
                    Console.Write("Player 1: Choose your field! ");
                }
                Board[(inputInt - 1) / 3, (inputInt - 1) % 3] = 'X';
            }
            else
            {
                int inputInt;
                Console.Write("Player 2: Choose your field! ");
                while (!Int32.TryParse(Console.ReadLine(), out inputInt) ||
                       (inputInt < 1 || inputInt > 9) ||
                       (Board[(inputInt - 1) / 3, (inputInt - 1) % 3] == 'X' ||
                        Board[(inputInt - 1) / 3, (inputInt - 1) % 3] == 'O'))
                {
                    Console.WriteLine("Please enter a number!\n");
                    Console.WriteLine(" Incorrect input! Please use another field!\n");
                    Console.Write("Player 2: Choose your field! ");
                }
                Board[(inputInt - 1) / 3, (inputInt - 1) % 3] = 'O';
            }
            _player = !_player;
        }

        public bool Checker()
        {
            for (int i = 0; i < 3; i++)
            {
                if (Board[i, 0] == Board[i, 1] && Board[i, 1] == Board[i, 2]) { return true; }
                if (Board[0, i] == Board[1, i] && Board[1, i] == Board[2, i]) { return true; }
            }
            if (Board[0, 0] == Board[1, 1] && Board[1, 1] == Board[2, 2]) { return true; }
            if (Board[0, 2] == Board[1, 1] && Board[1, 1] == Board[2, 0]) { return true; }
            return false;
        }

        public void Finalyze()
        {
            if (_turns == 10)
            {
                Console.WriteLine("Draw!");
            }
            else if (_player)
            {
                Console.WriteLine("Player 1 wins!\n");
            }
            else
            {
                Console.WriteLine("Player 2 wins!\n");
            }

            Console.WriteLine(" To restart press R!");
            Console.WriteLine(" To quit press Q!");

            char key;
            while ((key = (char)Console.Read()) != 'R' && key != 'Q') { }
            if (key == 'R')
            {
                setBoard();
                _player = false;
                _turns = 0;
                Console.Clear();
                Start();
            }
        }
    }
}
