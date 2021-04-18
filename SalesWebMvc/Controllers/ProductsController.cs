using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SalesWebMvc.Helpers;
using SalesWebMvc.Models;
using Microsoft.AspNetCore.Http;


namespace SalesWebMvc.Controllers {
    public class ProductsController : Controller {

        private readonly SalesWebMvcContext _context;
        private readonly IConfiguration Configuration;

        public ProductsController(SalesWebMvcContext context, IConfiguration configuration) {
            _context = context;
            Configuration = configuration;
        }

        // GET: Products
        public async Task<IActionResult> Index() {
            return View(await _context.Product.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null) {
                return NotFound();
            }

            //List<double> instList = product.listInstalments();

            //ViewBag.Add('Instalments',instList);

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SKU,Name,Price,Description,Instalments,Stock,Content,Active")] ImageProduct ImgPd) {

            Product product = new Product(ImgPd.Id,ImgPd.SKU,ImgPd.Name,ImgPd.Price,ImgPd.Description,ImgPd.Instalments,ImgPd.Stock,ImgPd.Active);

            if (ModelState.IsValid) {

                string Path = Configuration.GetValue("ImagePath", ".\\wwwroot\\");
                string imgName = ($"{product.SKU}.jpg");

                using (var stream = new FileStream(Path + imgName, FileMode.Create)) {
                   await ImgPd.Content.CopyToAsync(stream);
                }

                product.Image = @"\images\" + imgName;

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null) {
                return NotFound();
            }

            ImageProduct imageproduct = new ImageProduct(product.Id,product.SKU,product.Name,product.Price,product.Description,product.Instalments,product.Stock,product.Image,product.Active);

            return View(imageproduct);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SKU,Name,Price,Description,Instalments,Stock,Content,Active")] ImageProduct ImgPd) {

            Product product = new Product(ImgPd.Id, ImgPd.SKU, ImgPd.Name, ImgPd.Price, ImgPd.Description, ImgPd.Instalments, ImgPd.Stock, ImgPd.Active);

            if (id != product.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {

                    string Path = Configuration.GetValue("ImagePath", ".\\images\\");
                    string imgName = ($"{product.SKU}.jpg");

                    using (var stream = new FileStream(Path + imgName, FileMode.Create)) {
                        await ImgPd.Content.CopyToAsync(stream);
                    }

                    product.Image = @"\images\" + imgName;

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!ProductExists(product.Id)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null) {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id) {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
