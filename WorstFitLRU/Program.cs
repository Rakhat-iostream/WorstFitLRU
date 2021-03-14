using System;
using System.Collections.Generic;

namespace WorstFitLRU
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] blockSize = { 100, 500, 200, 300, 600 };
            int[] processSize = { 212, 417, 112, 426 };
            int m = blockSize.Length;
            int n = processSize.Length;

            Algos.WorstFit(blockSize, m, processSize, n);


            int[] pages = {7, 0, 1, 2, 0, 3,
                       0, 4, 2, 3, 0, 3, 2};

            List<LRUModel> list = new List<LRUModel>(4);
            Console.WriteLine(Algos.PageFaults(pages,
                              pages.Length, out list));
        }
    }
}
