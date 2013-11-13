using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondSolution
{
    public class Processor
    {
        private const char emptySymbol = '.';

        private const char leftSlash = '\\';

        private const char rightSlash = '/';

        private const char doubleSlash = 'x';

        private int horizontal;

        private int vertical;

        private char[,] face;

        private char[,] back;

        private bool[,] visitedFace;

        private bool[,] visitedBack;

        private long numberOfThreads;

        private long currentSequence;

        public int Horizontal
        {
            get
            {
                return this.horizontal;
            }
        }

        public int Vertical
        {
            get
            {
                return this.vertical;
            }
        }

        public Processor(int horizontal, int vertical)
        {
            this.horizontal = horizontal;
            this.vertical = vertical;
            this.face = new char[horizontal, vertical];
            this.back = new char[horizontal, vertical];
            this.visitedFace = new bool[horizontal, vertical];
            this.visitedBack = new bool[horizontal, vertical];
            this.numberOfThreads = long.MaxValue;
        }

        public void ReadFields()
        {
            Console.WriteLine("Enter the front field.");
            this.face = this.ReadSymbols();

            Console.WriteLine("Enter the back field.");
            this.back = this.ReadSymbols();

            this.InitializeVisited();
        }

        private char[,] ReadSymbols()
        {
            char[,] arr = new char[this.Horizontal, this.Vertical];

            for (int i = 0; i < this.Horizontal; i++)
            {
                Console.WriteLine("Enter row:");
                string symbols = Console.ReadLine();

                for (int j = 0; j < this.Vertical; j++)
                {
                    arr[i, j] = symbols[j];
                }
            }

            return arr;
        }

        private void InitializeVisited()
        {
            for (int i = 0; i < this.Horizontal; i++)
            {
                for (int j = 0; j < this.Vertical; j++)
                {
                    if (this.face[i, j] == emptySymbol)
                    {
                        this.visitedFace[i, j] = true;
                    }

                    if (this.back[i, j] == emptySymbol)
                    {
                        this.visitedBack[i, j] = true;
                    }
                }
            }
        }
    }
}
