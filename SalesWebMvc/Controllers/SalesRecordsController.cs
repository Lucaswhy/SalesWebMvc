using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService) {
            _salesRecordService = salesRecordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? initial, DateTime? final) {
            if (!initial.HasValue) initial = new DateTime(DateTime.Now.Year, 1, 1);
            if (!final.HasValue) final = new DateTime(DateTime.Now.Year, 1, 1);

            ViewData["minDate"] = initial.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = final.Value.ToString("yyyy-MM-dd");

            var result = await _salesRecordService.FindByDateAsync(initial, final);
            return View(result);
        }

        public IActionResult GroupingSearch() {
            return View();
        }
    }
}