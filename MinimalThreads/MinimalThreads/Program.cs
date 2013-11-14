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
            //int n = int.Parse(Console.ReadLine());
            int n = 10;

            Console.WriteLine("Enter vertical dimension.");
            //int m = int.Parse(Console.ReadLine());
            int m = 10;

            var processor = new Processor(n, m);

            processor.ReadFields();

            long numberOfThreads = processor.ProcessField();

            Console.WriteLine("The minimal number of threads is {0}", numberOfThreads);
            Console.WriteLine(processor.counter);
        }        
    }
}
