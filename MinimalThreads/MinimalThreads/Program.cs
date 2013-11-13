using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalThreads
{
    public class Program
    {
        private const char emptySymbol = '.';

        public static void Main(string[] args)
        {
            Console.WriteLine("Enter horizontal dimension.");
            int n = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter vertical dimension.");
            int m = int.Parse(Console.ReadLine());

            var processor = new Processor(n, m);

            processor.ReadFields();

            int numberOfThreads = processor.ProcessField();

            Console.WriteLine("The minimal number of threads is {0}", numberOfThreads);
        }        
    }
}
