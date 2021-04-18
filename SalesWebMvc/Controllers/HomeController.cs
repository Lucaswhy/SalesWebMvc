using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using SalesWebMvc.Helpers;

namespace SalesWebMvc.Controllers {
    public class HomeController : Controller {

        private readonly SalesWebMvcContext _context;
        private readonly IConfiguration Configuration;

        public PaginatedList<Product> Products { get; set; }
        public string NameSort { get; set; }
        public string PriceSort { get; set; }
        public string CurrentSort { get; set; }


        public HomeController(SalesWebMvcContext context, IConfiguration configuration) {
            _context = context;
            Configuration = configuration;
        }

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageIndex) {

            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            PriceSort = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null) {
                pageIndex = 1;
            }
            else {
                searchString = currentFilter;
            }

            var lProd = _context.Product.Where(pr => pr.Stock == 10);

            IQueryable<Product> productIQ = from p in _context.Product select p;

            switch (sortOrder) {
                case "name_desc":
                    productIQ = productIQ.OrderByDescending(p => p.Name);
                    break;
                case "Price":
                    productIQ = productIQ.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    productIQ = productIQ.OrderByDescending(p => p.Price);
                    break;
                default:
                    productIQ = productIQ.OrderBy(p => p.Name);
                    break;
            }

            ViewBag.CurrentFilter = searchString;
            ViewBag.BestSellers = lProd.ToList();
            ViewBag.SortOrder = sortOrder;

            if (!String.IsNullOrEmpty(searchString)) {
                productIQ = _context.Product.Where(pr => pr.Name.ToUpper().Contains(searchString) || pr.SKU.ToUpper().Contains(searchString));
            }

            int pageSize = Configuration.GetValue("PageSize", 15);
            Products = await PaginatedList<Product>.CreateAsync(productIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
            ViewBag.Pagination = Products;

            return View(await productIQ.AsNoTracking().ToListAsync());
        }

        public IActionResult About() {
            ViewData["Message"] = "An project APP from a C# Course to sell all your things! ";
            ViewData["Teacher"] = "Nélio Alves from Udemy!";
            ViewData["Dev"] = "by: Lucas Herculano Rodrigues";

            return View();
        }

        public IActionResult Contact() {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
