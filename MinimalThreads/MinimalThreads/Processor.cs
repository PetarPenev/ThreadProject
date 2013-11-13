using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalThreads
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

        public long ProcessField()
        {
            for (int i = 0; i < this.Horizontal; i++)
            {
                for (int j = 0; j < this.Vertical; j++)
                {
                    if (!this.visitedFace[i, j])
                    {

                    }

                    if (!this.visitedBack[i, j])
                    {

                    }
                }
            }

            /*this.currentSequence = 0;
            var freePosition = this.GetNextFreePosition();
            if (freePosition == null)
            {
                return 0;
            }

            char symbol;

            switch (freePosition.Item3)
            {
                case (SidePosition.Face):
                    symbol = this.face[freePosition.Item1, freePosition.Item2];
                    break;
                case (SidePosition.Back):
                    symbol = this.back[freePosition.Item1, freePosition.Item2];
                    break;
            }

            this.currentSequence++;

            switch (symbol)
            {
                case (leftSlash):
                    this.VisitPosition(freePosition);
                    this.DFS(freePosition.Item1, freePosition.Item2, (freePosition.Item3 + 1), CurrentPosition.LowRight);
                    //this.UnvisitPosition(freePosition);
                    break;
                case (rightSlash):
                    this.VisitPosition(freePosition);
                    this.DFS(freePosition.Item1, freePosition.Item2, (freePosition.Item3 + 1), CurrentPosition.LowLeft);
                    //this.UnvisitPosition(freePosition);
                    break;
                case (doubleSlash):
                    this.DFS(freePosition.Item1, freePosition.Item2, (freePosition.Item3 + 1), CurrentPosition.LowRight);
                    this.DFS(freePosition.Item1, freePosition.Item2, (freePosition.Item3 + 1), CurrentPosition.LowLeft);
                    break;
            }



            var freePosition = this.GetNextFreePosition();
            if (freePosition == null)
            {
                return 0;
            }

            this.currentSequence = 1;

            Tuple<int, int, SidePosition, CurrentPosition> positionToProcess = null;

            switch (freePosition.Item3)
            {
                case (SidePosition.Face):

            }

            this.DFS(freePosition);*/

            if (this.numberOfThreads == long.MaxValue)
            {
                this.numberOfThreads = 0;
            }

            return this.numberOfThreads;
        }

        private void DFS(int horizontal, int vertical, SidePosition side, CurrentPosition current)
        {
            /*this.VisitPosition();

            if (this.AllVisited())
            {
                if (this.currentSequence < this.numberOfThreads)
                {
                    this.numberOfThreads = this.currentSequence;
                }

                this.currentSequence--;
                return;
            }*/
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

        private Tuple<int, int, SidePosition> GetNextFreePosition()
        {
            for (int i = 0; i < this.Horizontal; i++)
            {
                for (int j = 0; j < this.Vertical; j++)
                {
                    if (!visitedFace[i, j])
                    {
                        return new Tuple<int, int, SidePosition>(i, j, SidePosition.Face);
                    }
                    else if (!visitedBack[i, j])
                    {
                        return new Tuple<int, int, SidePosition>(i, j, SidePosition.Back);
                    }
                }
            }

            return null;
        }
    }
}
