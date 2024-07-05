using LibraryDb.Data;
using LibraryDb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LibraryDb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> BorrowBook(CustomerBook customerBook)
        {
            var customers = await _context.Customers.ToListAsync();
            ViewBag.CustomerList = new SelectList(customers, "CustomerId", "Name");

            var books = await _context.Books.ToListAsync();
            ViewBag.BookList = new SelectList(books, "BookId", "Title");

            if (ModelState.IsValid)
            {
                var selectedBook = await _context.Books.FindAsync(customerBook.FkBookId);
                if (selectedBook != null)
                {
                    _context.CustomerBooks.Add(customerBook);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(BorrowBook));
                }
                else
                {
                    ModelState.AddModelError("FkBookId", "Invalid book selected.");
                }
            }
            return View();
        }

        public async Task<IActionResult> FilterCustomer(int? customerId)
        {
            ViewBag.filterCustomerList = new SelectList(_context.Customers, "CustomerId", "Name");

            var filterCustomers = await _context.Customers
                                .FirstOrDefaultAsync(x => x.CustomerId == customerId);

            if (filterCustomers != null)
            {
                return RedirectToAction("ShowBorrowedBooks", new {customerId});
            }
            
            return View();
        }

        public IActionResult ShowBorrowedBooks (int? bookId)
        {
            var filterBooks = _context.CustomerBooks
                    .Include(x => x.Customers)
                    .Include(x => x.Books)
                    .ToList();

            return View(filterBooks);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
