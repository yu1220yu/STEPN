using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using STEPÑ.Data;
using STEPÑ.Models;

namespace STEPÑ.Controllers
{
    [AllowAnonymous]
    public class ProductsController : Controller
    {
        private readonly STEPÑContext _context;

        public ProductsController(STEPÑContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(int? categoryId)
        {
            List<DetailViewModel> dvm = new List<DetailViewModel>();
            List<Product> products = new List<Product>();

            if (categoryId != null)
            {
                var result = await _context.Category.SingleAsync(x => x.Id.Equals(categoryId));
                products = await _context.Entry(result).Collection(x => x.Products).Query().ToListAsync();
            }
            else
            {
                products = await _context.Product.Include(p => p.Category).ToListAsync();
            }

            foreach (var product in products)
            {
                DetailViewModel item = new DetailViewModel
                {
                    Product = product,
                };
                dvm.Add(item);
            }
            ViewBag.count = dvm.Count;

            return View(dvm);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            DetailViewModel dvm = new DetailViewModel();
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                dvm.Product = product;
            }    

            return View(dvm);
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
