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

        private char[][,] field;

        //private char[,] back;

        private bool[][,] visited;

        //private bool[,] visitedBack;

        private long numberOfThreads;

        private long currentSequence;

        public long counter = 0;

        public List<string> result = new List<string>();


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
            this.field = new char[2][,];
            //this.back = new char[horizontal, vertical];
            this.visited = new bool[2][,];
            //this.visitedBack = new bool[horizontal, vertical];
            this.numberOfThreads = long.MaxValue;
        }

        public void ReadFields()
        {
            this.field = this.ReadSymbols();

            //Console.WriteLine("Enter the back field.");
            //this.back = this.ReadSymbols();

            this.InitializeVisited();
        }

        public long ProcessField()
        {
            for (int i = 0; i < this.Horizontal; i++)
            {
                for (int j = 0; j < this.Vertical; j++)
                {
                    if (!this.visited[0][i, j])
                    {
                        this.currentSequence = 1;
                        this.StartMainDfs(i, j, 0);
                        /*if (this.numberOfThreads > this.currentSequence)
                        {
                            this.numberOfThreads = this.currentSequence;
                        }*/
                    }

                    if (!this.visited[1][i, j])
                    {
                        this.currentSequence = 1;
                        this.StartMainDfs(i, j, 1);
                        /*if (this.numberOfThreads > this.currentSequence)
                        {
                            this.numberOfThreads = this.currentSequence;
                        }*/
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

        private void StartMainDfs(int i, int j, int l)
        {
            //this.currentSequence++;
             switch (this.field[l][i,j])
            {
                case ('\\'):
                    this.visited[l][i, j] = true;
                    //result.Append(this.field[l][i, j]);
                    this.ComponentDfs(i, j, l, CurrentPosition.HighLeft);
                    //this.currentSequence--;
                    //this.visited[l][i, j] = false;
                    this.ComponentDfs(i, j, l, CurrentPosition.LowRight);
                    //this.currentSequence--;
                    this.visited[l][i, j] = false;
                    //result.Length--;
                    break;
                case ('/'):
                    this.visited[l][i, j] = true;
                    //result.Append(this.field[l][i, j]);
                    this.ComponentDfs(i, j, l, CurrentPosition.HighRight);
                    //this.currentSequence--;
                    //this.visited[l][i, j] = false;
                    this.ComponentDfs(i, j, l, CurrentPosition.LowLeft);
                    //this.currentSequence--;
                    this.visited[l][i, j] = false;
                    //result.Length--;
                    break;
                case ('x'):
                    this.field[l][i,j] = '\\';
                    //result.Append(this.field[l][i, j]);
                    //result.Append("as\\");
                    this.ComponentDfs(i, j, l, CurrentPosition.HighRight);
                    //this.currentSequence--;
                    this.ComponentDfs(i, j, l, CurrentPosition.LowLeft);
                    //result.Length = result.Length - 4;
                    //this.currentSequence--;
                    this.field[l][i,j] = '/';
                    //result.Append(this.field[l][i, j]);
                    //result.Append("as/");
                    this.ComponentDfs(i, j, l, CurrentPosition.HighLeft);
                    //this.currentSequence--;
                    this.ComponentDfs(i, j, l, CurrentPosition.LowRight);
                    //result.Length = result.Length - 4;
                    //this.currentSequence--;
                    this.field[l][i,j] = 'x';
                    break;              
            }

            //this.currentSequence--;
        }

        private void ComponentDfs(int i, int j, int l, CurrentPosition pos)
        {
            this.result.Add("" + l + i + j);
            // Switching l to the opposite side.
            if (l == 0)
            {
                l = 1;
            }
            else
            {
                l = 0;
            }

            switch (pos)
            {
                case (CurrentPosition.HighLeft):
                    if (!this.visited[l][i, j])
                    {
                        switch (this.field[l][i, j])
                        {
                            case ('\\'):
                                this.visited[l][i, j] = true;
                                this.ComponentDfs(i, j, l, CurrentPosition.LowRight);
                                this.visited[l][i, j] = false;
                                break;
                            case ('x'):
                                this.field[l][i, j] = '/';
                                this.ComponentDfs(i, j, l, CurrentPosition.LowRight);
                                this.field[l][i, j] = 'x';
                                break;
                        }
                    }

                    if (i - 1 >= 0)
                    {
                        if (!this.visited[l][i - 1, j])
                        {
                            switch (this.field[l][i - 1, j])
                            {
                                case ('/'):
                                    this.visited[l][i - 1, j] = true;
                                    this.ComponentDfs(i - 1, j, l, CurrentPosition.HighRight);
                                    this.visited[l][i - 1, j] = false;
                                    break;
                                case ('x'):
                                    this.field[l][i - 1, j] = '\\';
                                    this.ComponentDfs(i - 1, j, l, CurrentPosition.HighRight);
                                    this.field[l][i - 1, j] = 'x';
                                    break;
                            }
                        }

                        if (j - 1 >= 0)
                        {
                            if (!this.visited[l][i - 1, j - 1])
                            {
                                switch (this.field[l][i - 1, j - 1])
                                {
                                    case ('\\'):
                                        this.visited[l][i - 1, j - 1] = true;
                                        this.ComponentDfs(i - 1, j - 1, l, CurrentPosition.HighLeft);
                                        this.visited[l][i - 1, j - 1] = false;
                                        break;
                                    case ('x'):
                                        this.field[l][i - 1, j - 1] = '/';
                                        this.ComponentDfs(i - 1, j, l, CurrentPosition.HighLeft);
                                        this.field[l][i - 1, j - 1] = 'x';
                                        break;
                                }
                            }
                        }
                    }

                    if (j - 1 >= 0)
                    {
                        if (!this.visited[l][i, j - 1])
                        {
                            switch (this.field[l][i, j - 1])
                            {
                                case ('/'):
                                    this.visited[l][i, j - 1] = true;
                                    this.ComponentDfs(i, j - 1, l, CurrentPosition.LowLeft);
                                    this.visited[l][i, j - 1] = false;
                                    break;
                                case ('x'):
                                    this.field[l][i, j - 1] = '\\';
                                    this.ComponentDfs(i, j - 1, l, CurrentPosition.LowLeft);
                                    this.field[l][i, j - 1] = 'x';
                                    break;
                            }
                        }
                    }
                    break;
                case (CurrentPosition.HighRight):
                    if (!this.visited[l][i, j])
                    {
                        switch (this.field[l][i, j])
                        {
                            case ('/'):
                                this.visited[l][i, j] = true;
                                this.ComponentDfs(i, j, l, CurrentPosition.LowLeft);
                                this.visited[l][i, j] = false;
                                break;
                            case ('x'):
                                this.field[l][i, j] = '\\';
                                this.ComponentDfs(i, j, l, CurrentPosition.LowLeft);
                                this.field[l][i, j] = 'x';
                                break;
                        }
                    }

                    if (i - 1 >= 0)
                    {
                        if (!this.visited[l][i - 1, j])
                        {
                            switch (this.field[l][i - 1, j])
                            {
                                case ('\\'):
                                    this.visited[l][i - 1, j] = true;
                                    this.ComponentDfs(i - 1, j, l, CurrentPosition.HighLeft);
                                    this.visited[l][i - 1, j] = false;
                                    break;
                                case ('x'):
                                    this.field[l][i - 1, j] = '/';
                                    this.ComponentDfs(i - 1, j, l, CurrentPosition.HighLeft);
                                    this.field[l][i - 1, j] = 'x';
                                    break;
                            }
                        }

                        if (j + 1 < this.Vertical)
                        {
                            if (!this.visited[l][i - 1, j + 1])
                            {
                                switch (this.field[l][i - 1, j + 1])
                                {
                                    case ('/'):
                                        this.visited[l][i - 1, j + 1] = true;
                                        this.ComponentDfs(i - 1, j + 1, l, CurrentPosition.HighRight);
                                        this.visited[l][i - 1, j + 1] = false;
                                        break;
                                    case ('x'):
                                        this.field[l][i - 1, j + 1] = '\\';
                                        this.ComponentDfs(i - 1, j + 1, l, CurrentPosition.HighRight);
                                        this.field[l][i - 1, j + 1] = 'x';
                                        break;
                                }
                            }
                        }
                    }

                    if (j + 1 < this.Vertical)
                    {
                        if (!this.visited[l][i, j + 1])
                        {
                            switch (this.field[l][i, j + 1])
                            {
                                case ('\\'):
                                    this.visited[l][i, j + 1] = true;
                                    this.ComponentDfs(i, j + 1, l, CurrentPosition.LowLeft);
                                    this.visited[l][i, j + 1] = false;
                                    break;
                                case ('x'):
                                    this.field[l][i, j + 1] = '/';
                                    this.ComponentDfs(i, j + 1, l, CurrentPosition.LowLeft);
                                    this.field[l][i, j + 1] = 'x';
                                    break;
                            }
                        }
                    }
                    break;
                case (CurrentPosition.LowLeft):
                    if (!this.visited[l][i, j])
                    {
                        switch (this.field[l][i, j])
                        {
                            case ('/'):
                                this.visited[l][i, j] = true;
                                this.ComponentDfs(i, j, l, CurrentPosition.HighRight);
                                this.visited[l][i, j] = false;
                                break;
                            case ('x'):
                                this.field[l][i, j] = '\\';
                                this.ComponentDfs(i, j, l, CurrentPosition.HighRight);
                                this.field[l][i, j] = 'x';
                                break;
                        }
                    }

                    if (i + 1 < this.Horizontal)
                    {
                        if (!this.visited[l][i + 1, j])
                        {
                            switch (this.field[l][i + 1, j])
                            {
                                case ('\\'):
                                    this.visited[l][i + 1, j] = true;
                                    this.ComponentDfs(i + 1, j, l, CurrentPosition.LowRight);
                                    this.visited[l][i + 1, j] = false;
                                    break;
                                case ('x'):
                                    this.field[l][i + 1, j] = '/';
                                    this.ComponentDfs(i + 1, j, l, CurrentPosition.LowRight);
                                    this.field[l][i + 1, j] = 'x';
                                    break;
                            }
                        }

                        if (j - 1 >= 0)
                        {
                            if (!this.visited[l][i + 1, j - 1])
                            {
                                switch (this.field[l][i + 1, j - 1])
                                {
                                    case ('/'):
                                        this.visited[l][i + 1, j - 1] = true;
                                        this.ComponentDfs(i + 1, j - 1, l, CurrentPosition.LowLeft);
                                        this.visited[l][i + 1, j - 1] = false;
                                        break;
                                    case ('x'):
                                        this.field[l][i + 1, j - 1] = '\\';
                                        this.ComponentDfs(i + 1, j - 1, l, CurrentPosition.LowLeft);
                                        this.field[l][i + 1, j - 1] = 'x';
                                        break;
                                }
                            }
                        }
                    }

                    if (j -1 >= 0)
                    {
                        if (!this.visited[l][i, j - 1])
                        {
                            switch (this.field[l][i, j - 1])
                            {
                                case ('\\'):
                                    this.visited[l][i, j - 1] = true;
                                    this.ComponentDfs(i, j - 1, l, CurrentPosition.LowLeft);
                                    this.visited[l][i, j - 1] = false;
                                    break;
                                case ('x'):
                                    this.field[l][i, j - 1] = '/';
                                    this.ComponentDfs(i, j - 1, l, CurrentPosition.LowLeft);
                                    this.field[l][i, j - 1] = 'x';
                                    break;
                            }
                        }
                    }
                    break;
                case (CurrentPosition.LowRight):
                    if (!this.visited[l][i, j])
                    {
                        switch (this.field[l][i, j])
                        {
                            case ('\\'):
                                this.visited[l][i, j] = true;
                                this.ComponentDfs(i, j, l, CurrentPosition.HighLeft);
                                this.visited[l][i, j] = false;
                                break;
                            case ('x'):
                                this.field[l][i, j] = '/';
                                this.ComponentDfs(i, j, l, CurrentPosition.HighLeft);
                                this.field[l][i, j] = 'x';
                                break;
                        }
                    }

                    if (i + 1 < this.Horizontal)
                    {
                        if (!this.visited[l][i + 1, j])
                        {
                            switch (this.field[l][i + 1, j])
                            {
                                case ('/'):
                                    this.visited[l][i + 1, j] = true;
                                    this.ComponentDfs(i + 1, j, l, CurrentPosition.LowLeft);
                                    this.visited[l][i + 1, j] = false;
                                    break;
                                case ('x'):
                                    this.field[l][i + 1, j] = '\\';
                                    this.ComponentDfs(i + 1, j, l, CurrentPosition.LowLeft);
                                    this.field[l][i + 1, j] = 'x';
                                    break;
                            }
                        }

                        if (j + 1 < this.Vertical)
                        {
                            if (!this.visited[l][i + 1, j + 1])
                            {
                                switch (this.field[l][i + 1, j + 1])
                                {
                                    case ('\\'):
                                        this.visited[l][i + 1, j + 1] = true;
                                        this.ComponentDfs(i + 1, j + 1, l, CurrentPosition.LowRight);
                                        this.visited[l][i + 1, j + 1] = false;
                                        break;
                                    case ('x'):
                                        this.field[l][i + 1, j + 1] = '/';
                                        this.ComponentDfs(i + 1, j + 1, l, CurrentPosition.LowRight);
                                        this.field[l][i + 1, j + 1] = 'x';
                                        break;
                                }
                            }
                        }
                    }

                    if (j + 1 < this.Vertical)
                    {
                        if (!this.visited[l][i, j + 1])
                        {
                            switch (this.field[l][i, j + 1])
                            {
                                case ('/'):
                                    this.visited[l][i, j + 1] = true;
                                    this.ComponentDfs(i, j + 1, l, CurrentPosition.HighRight);
                                    this.visited[l][i, j + 1] = false;
                                    break;
                                case ('x'):
                                    this.field[l][i, j + 1] = '\\';
                                    this.ComponentDfs(i, j + 1, l, CurrentPosition.HighRight);
                                    this.field[l][i, j + 1] = 'x';
                                    break;
                            }
                        }
                    }
                    break;
            }

            for (int k = 0; k < this.Horizontal; k++)
            {
                for (int m = 0; m < this.Vertical; m++)
                {
                    if (!this.visited[0][k, m])
                    {
                        this.currentSequence++;
                        this.StartMainDfs(k, m, 0);
                        this.currentSequence--;
                    }

                    if (!this.visited[1][k, m])
                    {
                        this.currentSequence++;
                        this.StartMainDfs(k, m, 1);
                        this.currentSequence--;
                    }
                }
            }

            if (this.AllCleared())
            {
                counter++;
                //Console.WriteLine(this.result.Print());
                if (this.currentSequence < this.numberOfThreads)
                {
                    this.numberOfThreads = this.currentSequence;
                }
            }

            this.result.RemoveAt(this.result.Count - 1);


            //this.currentSequence--;

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

        private bool AllCleared()
        {
            for (int l = 0; l < 2; l++)
            {
                for (int i = 0; i < this.Horizontal; i++)
                {
                    for (int j = 0; j < this.Vertical; j++)
                    {
                        if (!this.visited[l][i, j])
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private char[][,] ReadSymbols()
        {
            char[][,] arr = new char[2][,];

            /*arr[0] = new char[1, 1]{
                { '/'}
            };

            arr[1] = new char[1, 1]{
                { 'x'}
            };*/ 


            /*arr[0] = new char[4, 5] {
                { '.', '.', '.', '/', '.'},
                { '.', '.', '/', '.', '.'},
                { '.', '.', '.', '.', '.'},
                { '.', '.', '.', '.', '.'}
            };


            arr[1] = new char[4, 5] {
                { '.', '.', '.', '/', '.'},
                { '.', '.', '/', '.', '.'},
                { '.', '.', '.', '.', '.'},
                { '.', '.', '.', '.', '.'}
            };*/

            /*arr[0] = new char[4, 5] {
                { '.', '.', '.', '.', '.'},
                { '.', '\\', '.', '.', '.'},
                { '.', '.', '\\', '.', '.'},
                { '.', '.', '.', '.', '.'}
            };


            arr[1] = new char[4, 5] {
                { '.', '.', '.', '.', '.'},
                { '.', '.', '.', '.', '\\'},
                { '.', '\\', 'x', '.', '.'},
                { '.', '.', '.', '.', '.'}
            };*/

            arr[0] = new char[10, 10];
            arr[1] = new char[10, 10];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    arr[0][i, j] = '.';
                    arr[1][i, j] = '.';
                }   
            }

            arr[0][2, 2] = '/';
            arr[1][2, 2] = '/';

            arr[0][4, 4] = '/';
            arr[1][4, 4] = '/';

            arr[0][7, 7] = '/';
            arr[1][7, 7] = '/';


            /*for (int l = 0; l < 2; l++)
            {
                arr[l] = new char[this.Horizontal, this.Vertical];
                Console.WriteLine("Enter side {0}", l);
                for (int i = 0; i < this.Horizontal; i++)
                {
                    Console.WriteLine("Enter row:");
                    string symbols = Console.ReadLine();

                    for (int j = 0; j < this.Vertical; j++)
                    {
                        arr[l][i, j] = symbols[j];
                    }
                }
            }*/

            return arr;
        }

        private void InitializeVisited()
        {
            this.visited[0] = new bool[this.Horizontal, this.Vertical];
            this.visited[1] = new bool[this.Horizontal, this.Vertical];

            for (int i = 0; i < this.Horizontal; i++)
            {
                for (int j = 0; j < this.Vertical; j++)
                {
                    if (this.field[0][i, j] == emptySymbol)
                    {
                        this.visited[0][i, j] = true;
                    }

                    if (this.field[1][i, j] == emptySymbol)
                    {
                        this.visited[1][i, j] = true;
                    }
                }
            }
        }
    }
}