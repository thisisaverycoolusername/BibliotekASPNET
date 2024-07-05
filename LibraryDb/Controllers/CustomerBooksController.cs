using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryDb.Data;
using LibraryDb.Models;

namespace LibraryDb.Controllers
{
    public class CustomerBooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerBooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CustomerBooks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CustomerBooks.Include(c => c.Books).Include(c => c.Customers);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CustomerBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerBook = await _context.CustomerBooks
                .Include(c => c.Books)
                .Include(c => c.Customers)
                .FirstOrDefaultAsync(m => m.CustomerBookId == id);
            if (customerBook == null)
            {
                return NotFound();
            }

            return View(customerBook);
        }

        // GET: CustomerBooks/Create
        public IActionResult Create()
        {
            ViewData["FkBookId"] = new SelectList(_context.Books, "BookId", "Author");
            ViewData["FkCustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
            return View();
        }

        // POST: CustomerBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerBookId,FkCustomerId,FkBookId,StartDateOfLoan,EndDateOfLoan")] CustomerBook customerBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkBookId"] = new SelectList(_context.Books, "BookId", "Author", customerBook.FkBookId);
            ViewData["FkCustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", customerBook.FkCustomerId);
            return View(customerBook);
        }

        // GET: CustomerBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerBook = await _context.CustomerBooks.FindAsync(id);
            if (customerBook == null)
            {
                return NotFound();
            }
            ViewData["FkBookId"] = new SelectList(_context.Books, "BookId", "Author", customerBook.FkBookId);
            ViewData["FkCustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", customerBook.FkCustomerId);
            return View(customerBook);
        }

        // POST: CustomerBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerBookId,FkCustomerId,FkBookId,StartDateOfLoan,EndDateOfLoan")] CustomerBook customerBook)
        {
            if (id != customerBook.CustomerBookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerBookExists(customerBook.CustomerBookId))
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
            ViewData["FkBookId"] = new SelectList(_context.Books, "BookId", "Author", customerBook.FkBookId);
            ViewData["FkCustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", customerBook.FkCustomerId);
            return View(customerBook);
        }

        // GET: CustomerBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerBook = await _context.CustomerBooks
                .Include(c => c.Books)
                .Include(c => c.Customers)
                .FirstOrDefaultAsync(m => m.CustomerBookId == id);
            if (customerBook == null)
            {
                return NotFound();
            }

            return View(customerBook);
        }

        // POST: CustomerBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerBook = await _context.CustomerBooks.FindAsync(id);
            if (customerBook != null)
            {
                _context.CustomerBooks.Remove(customerBook);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerBookExists(int id)
        {
            return _context.CustomerBooks.Any(e => e.CustomerBookId == id);
        }
    }
}
