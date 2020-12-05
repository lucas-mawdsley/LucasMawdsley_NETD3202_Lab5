using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LucasMawdsley_NETD3202_Lab5.Models;
using LucasMawdsley_NETD3202_Lab5.Data;

namespace LucasMawdsley_NET3202_Lab5.Controllers
{
    public class ProductsController : Controller
    {
        // intializes product dbcontext
        private readonly ProductContext _context;

        // Constructor
        public ProductsController(ProductContext context)
        {
            _context = context;
        }

        // Table of all products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        // View for displaying retrieved product record
        public async Task<IActionResult> Details(int? id)
        {
            // if there's no id
            if (id == null)
            {
                // 404
                return NotFound();
            }

            // declare product to pass into view
            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            // if no product was found to pass
            if (product == null)
            {
                // 404
                return NotFound();
            }

            return View(product);
        }

        // Intial Create View
        public IActionResult Create()
        {
            // Creates a SelectList to populate dropdown menu for VendorId
            Product product = new Product();
            var queryData = new SelectList(_context.Vendors.Select(c => new {
                VendorId = c.VendorId, VendorName = c.Name }), "VendorId", "VendorName", 3
                ).ToList();
            product.VendorList = queryData;
            
            // returns view with "dropdownlist-primed" model
            return View(product);
        }

        // Create View after form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Make,Model,Description,Quantity,UnitPrice,VendorId")] Product product)
        {
            // if all fields entered correctly
            if (ModelState.IsValid)
            {
                // add product to database
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // Intial Edit View
        public async Task<IActionResult> Edit(int? id)
        {
            // if there's no id
            if (id == null)
            {
                // 404
                return NotFound();
            }

            // declare product to pass into view based on selected id
            var product = await _context.Products.FindAsync(id);
            // if no product was found to pass
            if (product == null)
            {
                // 404
                return NotFound();
            }

            // Creates a SelectList to populate dropdown menu for VendorId
            var queryData = new SelectList(_context.Vendors.Select(c => new {
                VendorId = c.VendorId, VendorName = c.Name }), "VendorId", "VendorName", 3
                ).ToList();
            product.VendorList = queryData;

            // returns view with "dropdownlist-primed" model
            return View(product);
        }

        // Edit View after form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Make,Model,Description,Quantity,UnitPrice,VendorId")] Product product)
        {
            // if id is not the same as the one intially passed
            if (id != product.ProductId)
            {
                // 404
                return NotFound();
            }

            // if all fields entered correctly
            if (ModelState.IsValid)
            {
                try
                {
                    // update database
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                // record update overlap
                catch (DbUpdateConcurrencyException)
                {
                    // if product doesn't exist
                    if (!ProductExists(product.ProductId))
                    {
                        // 404
                        return NotFound();
                    }
                    else // product exists
                    {
                        // throw exception
                        throw;
                    }
                }
                // redirects to Products/Index
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // Intial Delete View
        public async Task<IActionResult> Delete(int? id)
        {
            // if there's no id
            if (id == null)
            {
                // 404
                return NotFound();
            }

            // declare product to pass into view
            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            // if no product was found to pass
            if (product == null)
            {
                // 404
                return NotFound();
            }

            return View(product);
        }

        // Product deletion
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // select product to delete by id
            var product = await _context.Products.FindAsync(id);
            // delete product
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            // redirect to Products/Index
            return RedirectToAction(nameof(Index));
        }

        // Method to confirm that a product with the parameter id exists
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
