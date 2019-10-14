// I, Christian Serad, student number 000395777, certify that this material is my
// original work. No other person's work has been used without due
// acknowledgement and I have not made my work available to anyone else.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab1BVersion2.Data;
using Lab1BVersion2.Models;
using Microsoft.AspNetCore.Authorization;

namespace Lab1BVersion2.Controllers
{
    [Authorize(Roles = "Employee, Supervisor")]
    public class DealershipController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DealershipController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<string> Seed()
        {
            int numberOfDealership = 0;
            if (_context.Dealership.Count() == 0)
            {

                var dealership1 = new Dealership { Name = "Paul", Email = "Paul@gmail.com", PhoneNumber = "905000300" };
                var dealership2 = new Dealership { Name = "Romar", Email = "Romar@gmail.com", PhoneNumber = "906000300" };
                var dealership3 = new Dealership { Name = "Christian", Email = "Christian@gmail.com", PhoneNumber = "907000300" };


                _context.AddRange(dealership1, dealership2, dealership3);
                await _context.SaveChangesAsync();
                numberOfDealership = 3;
            }

            return $"{numberOfDealership} dealerships Added";
        }

        // GET: Dealership
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dealership.ToListAsync());
        }

        // GET: Dealership/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dealership = await _context.Dealership
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dealership == null)
            {
                return NotFound();
            }

            return View(dealership);
        }

        // GET: Dealership/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dealership/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Email,PhoneNumber")] Dealership dealership)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dealership);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dealership);
        }

        // GET: Dealership/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dealership = await _context.Dealership.FindAsync(id);
            if (dealership == null)
            {
                return NotFound();
            }
            return View(dealership);
        }

        // POST: Dealership/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Email,PhoneNumber")] Dealership dealership)
        {
            if (id != dealership.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dealership);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DealershipExists(dealership.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dealership);
        }

        // GET: Dealership/Delete/5
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dealership = await _context.Dealership
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dealership == null)
            {
                return NotFound();
            }

            return View(dealership);
        }

        // POST: Dealership/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dealership = await _context.Dealership.FindAsync(id);
            _context.Dealership.Remove(dealership);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DealershipExists(int id)
        {
            return _context.Dealership.Any(e => e.ID == id);
        }
    }
}
