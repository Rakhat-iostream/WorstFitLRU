using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorstFitLRU;

namespace WebWorstFitLRU.Pages
{
    public class IndexModel : PageModel
    {
        public List<WorstFitModel> WorstFitModels { get; set; }
        public List<LRUModel> LRUModels { get; set; }
       
        public List<string> InputProcesses { get; set; }
        public List<string> InputBlocks { get; set; }
        public IActionResult OnGet()
        {
            WorstFitModels = new List<WorstFitModel>();
            LRUModels = new List<LRUModel>();
            return Page();
        }

        public IActionResult OnPostWorstFit(string blocksStr, string processesStr)
        {
            var blocksArr = blocksStr.Split(' ');
            var processesArr = processesStr.Split(' ');
            InputProcesses = processesArr.ToList();
            InputBlocks = blocksArr.ToList();
            var blocksList = new List<int>(blocksArr.Length);
            foreach (var i in blocksArr)
            {
                blocksList.Add(int.Parse(i));
            }
            var processesList = new List<int>(processesArr.Length);
            foreach (var i in processesArr)
            {
                processesList.Add(int.Parse(i));
            }
            WorstFitModels = Algos.WorstFit(blocksList.ToArray(), blocksList.Count, processesList.ToArray(), processesList.Count);
            LRUModels = new List<LRUModel>();
            return Page();
        }



    }
}
