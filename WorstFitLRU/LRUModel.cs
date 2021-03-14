using System;
using System.Collections.Generic;
using System.Text;

namespace WorstFitLRU
{
    public class LRUModel
    {
        public List<int> AllocatedPages { get; set; }
        public static int PageFaults { get; set; }
    }
}
