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
    public class VendorsController : Controller
    {
        // intializes vendor dbcontext
        private readonly ProductContext _context;

        // Constructor
        public VendorsController(ProductContext context)
        {
            _context = context;
        }

        // Table of all vendors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vendors.ToListAsync());
        }

        // View for displaying retrieved vendor record
        public async Task<IActionResult> Details(int? id)
        {
            // if there's no id
            if (id == null)
            {
                // 404
                return NotFound();
            }

            // declare vendor to pass into view
            var vendor = await _context.Vendors
                .FirstOrDefaultAsync(m => m.VendorId == id);
            // if no vendor was found to pass
            if (vendor == null)
            {
                // 404
                return NotFound();
            }

            return View(vendor);
        }

        // Intial Create View
        public IActionResult Create()
        {
            return View();
        }

        // Create View after form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VendorId,Name,Address,RepLastName,RepFirstName,RepPhoneNumber,RepEmail")] Vendor vendor)
        {
            // if all fields entered correctly
            if (ModelState.IsValid)
            {
                // add vendor to database
                _context.Add(vendor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendor);
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

            // declare vendor to pass into view based on selected id
            var vendor = await _context.Vendors.FindAsync(id);
            // if no vendor was found to pass
            if (vendor == null)
            {
                // 404
                return NotFound();
            }
            return View(vendor);
        }

        // Edit View after form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VendorId,Name,Address,RepLastName,RepFirstName,RepPhoneNumber,RepEmail")] Vendor vendor)
        {
            // if id is not the same as the one intially passed
            if (id != vendor.VendorId)
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
                    _context.Update(vendor);
                    await _context.SaveChangesAsync();
                }
                // record update overlap
                catch (DbUpdateConcurrencyException)
                {
                    // if vendor doesn't exist
                    if (!VendorExists(vendor.VendorId))
                    {
                        // 404
                        return NotFound();
                    }
                    else // vendor exists
                    {
                        // throw exception
                        throw;
                    }
                }
                // redirects to Vendors/Index
                return RedirectToAction(nameof(Index));
            }
            return View(vendor);
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

            // declare vendor to pass into view
            var vendor = await _context.Vendors
                .FirstOrDefaultAsync(m => m.VendorId == id);
            // if no vendor was found to pass
            if (vendor == null)
            {
                // 404
                return NotFound();
            }

            return View(vendor);
        }

        // Vendor deletion
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // select vendor to delete by id
            var vendor = await _context.Vendors.FindAsync(id);
            // delete vendor
            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();
            // redirect to Vendors/Index
            return RedirectToAction(nameof(Index));
        }

        // Method to confirm that a vendor with the parameter id exists
        private bool VendorExists(int id)
        {
            return _context.Vendors.Any(e => e.VendorId == id);
        }
    }
}
