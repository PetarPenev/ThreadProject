using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalThreads
{
    public class HelperMethods
    {

        public static bool AllVisited(bool[,] visited)
        {
            for (int i = 0; i < visited.GetLength(0); i++)
            {
                for (int j = 0; j < visited.GetLength(1); j++)
                {
                    if (visited[i, j] == false)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static Tuple<int,int> GetFirstFreePosition(bool[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (!arr[i, j])
                    {
                        return new Tuple<int, int>(i, j);
                    }
                }
            }

            return null;
        }
    }
}
