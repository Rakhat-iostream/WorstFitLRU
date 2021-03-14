using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorstFitLRU
{
    public class Algos
    {
        public static List<WorstFitModel> WorstFit(int[] blockSize, int blockNum, int[] processSize, int processNum)
        {
            // Stores block id of the block allocated to a  
            // process  
            int[] allocation = new int[processNum];

            // Initially no block is assigned to any process  
            for (int i = 0; i < allocation.Length; i++)
                allocation[i] = -1;

            // pick each process and find suitable blocks  
            // according to its size ad assign to it  
            for (int i = 0; i < processNum; i++)
            {
                // Find the best fit block for current process  
                int wstIdx = -1;
                for (int j = 0; j < blockNum; j++)
                {
                    if (blockSize[j] >= processSize[i])
                    {
                        if (wstIdx == -1)
                            wstIdx = j;
                        else if (blockSize[wstIdx] < blockSize[j])
                            wstIdx = j;
                    }
                }

                // If we could find a block for current process  
                if (wstIdx != -1)
                {
                    // allocate block j to p[i] process  
                    allocation[i] = wstIdx;

                    // Reduce available memory in this block.  
                    blockSize[wstIdx] -= processSize[i];
                }
            }

            var model = new List<WorstFitModel>(processNum);
            Console.WriteLine("\nProcess No.\tProcess Size\tBlock no.");
            for (int i = 0; i < processNum; i++)
            {
                Console.Write(" " + (i + 1) + "\t\t\t" + processSize[i] + "\t\t\t");
                if (allocation[i] != -1)
                    Console.Write(allocation[i] + 1);
                else
                    Console.Write("Not Allocated");
                Console.WriteLine();

                model.Add(new WorstFitModel
                {
                    ProcessNo = i + 1,
                    ProcessSize = processSize[i],
                    BlockNo = (allocation[i] != -1) ? allocation[i] + 1 : -1
                });
            }

            return model;
        }


        public static int PageFaults(int[] pages, int capacity, out List<LRUModel> list)
        {
            // To represent set of current pages.  
            // We use an unordered_set so that  
            // we quickly check if a page is  
            // present in set or not 
            HashSet<int> set = new HashSet<int>(capacity);
            list = new List<LRUModel>(pages.Length);
            // To store least recently used indexes 
            // of pages. 
            Dictionary<int, int> indexes = new Dictionary<int, int>();

            // Start from initial page 
            int page_faults = 0;
            for (int i = 0; i < pages.Length; i++)
            {
                // Check if the set can hold more pages 
                if (set.Count < capacity)
                {
                    // Insert it into set if not present 
                    // already which represents page fault 
                    if (!set.Contains(pages[i]))
                    {
                        set.Add(pages[i]);

                        // increment page fault 
                        page_faults++;
                    }

                    // Store the recently used index of 
                    // each page 
                    if (indexes.ContainsKey(pages[i]))
                        indexes[pages[i]] = i;
                    else
                        indexes.Add(pages[i], i);
                }

                // If the set is full then need to  
                // perform lru i.e. remove the least  
                // recently used page and insert 
                // the current page 
                else
                {
                    // Check if current page is not  
                    // already present in the set 
                    if (!set.Contains(pages[i]))
                    {
                        // Find the least recently used pages 
                        // that is present in the set 
                        int lru = int.MaxValue, val = int.MinValue;

                        foreach (int itr in set)
                        {
                            int temp = itr;
                            if (indexes[temp] < lru)
                            {
                                lru = indexes[temp];
                                val = temp;
                            }
                        }

                        // Remove the indexes page 
                        set.Remove(val);

                        //remove lru from hashmap 
                        indexes.Remove(val);

                        // insert the current page 
                        set.Add(pages[i]);

                        // Increment page faults 
                        page_faults++;
                    }

                    // Update the current page index 
                    if (indexes.ContainsKey(pages[i]))
                        indexes[pages[i]] = i;
                    else
                        indexes.Add(pages[i], i);

                    list.Add(new LRUModel { AllocatedPages = set.ToList() });
                }
            }
            LRUModel.PageFaults = page_faults;
            return LRUModel.PageFaults;
        }
    }
}
