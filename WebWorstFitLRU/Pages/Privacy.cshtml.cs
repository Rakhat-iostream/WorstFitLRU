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
    public class PrivacyModel : PageModel
    {
        public List<WorstFitModel> WorstFitModels { get; set; }
        public List<LRUModel> LRUModels { get; set; }
        public int PageFaults { get; set; }
        public List<string> InputPages { get; set; }
        public int InputCapacity { get; set; }
        public List<string> InputProcesses { get; set; }
        public List<string> InputBlocks { get; set; }

        public IActionResult OnGet()
        {

            LRUModels = new List<LRUModel>();
            return Page();
        }

        public IActionResult OnPostLru(string pagesStr, int capacity)
        {
            var pagesArr = pagesStr.Split(' ');
            var pagesList = new List<int>(pagesArr.Length);
            foreach (var i in pagesArr)
            {
                pagesList.Add(int.Parse(i));
            }
            Algos.PageFaults(pagesList.ToArray(), capacity, out List<LRUModel> list);
            LRUModels = list;
            PageFaults = LRUModel.PageFaults;
            InputPages = pagesArr.ToList();
            InputCapacity = capacity;
            return Page();
        }
    }
}
