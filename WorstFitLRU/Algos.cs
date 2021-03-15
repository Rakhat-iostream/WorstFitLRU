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

            
            for (int i = 0; i < processNum; i++)
            {
                 
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
                    
                    allocation[i] = wstIdx;

                     
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
           
            HashSet<int> set = new HashSet<int>(capacity);
            list = new List<LRUModel>(pages.Length);
            
            Dictionary<int, int> indexes = new Dictionary<int, int>();

            
            int page_faults = 0;
            for (int i = 0; i < pages.Length; i++)
            {
                
                if (set.Count < capacity)
                {
                    // Insert it into set if not present 
                    // already which represents page fault 
                    if (!set.Contains(pages[i]))
                    {
                        set.Add(pages[i]);

                       
                        page_faults++;
                    }
                    if (indexes.ContainsKey(pages[i]))
                        indexes[pages[i]] = i;
                    else
                        indexes.Add(pages[i], i);
                }

               
                else
                {
                    
                    if (!set.Contains(pages[i]))
                    {
                        
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

                        
                        set.Remove(val);

                        
                        indexes.Remove(val);

                        
                        set.Add(pages[i]);

                        
                        page_faults++;
                    }

                   
                    if (indexes.ContainsKey(pages[i]))
                        indexes[pages[i]] = i;
                    else
                        indexes.Add(pages[i], i);

                }
                    list.Add(new LRUModel { AllocatedPages = set.ToList() });
            }
            LRUModel.PageFaults = page_faults;
            return LRUModel.PageFaults;
        }
    }
}
