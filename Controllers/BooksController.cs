using BookStoreApp.Data;
using BookStoreApp.Models;
using BookStoreApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<BooksController> _logger;
        public BooksController(AppDbContext context, ILogger<BooksController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string searchTerm)
        {
            var contacts = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                contacts = contacts.Where(c =>
                    c.Title.Contains(searchTerm) ||
                    c.Author.Contains(searchTerm));
            }

            var viewModel = new BooksViewModel()
            {
                Books = contacts.ToList(),
                SearchTerm = searchTerm
            };

            return View(viewModel);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            _logger.LogInformation("Create book...");
            if (!ModelState.IsValid) return View(book);

            _context.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            
            var book = await _context.Books.FindAsync(id);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            _logger.LogInformation("Edit book...");
            if (id != book.Id || !ModelState.IsValid)
                return View(book);

            _context.Update(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Delete book...");
            var book = await _context.Books.FindAsync(id);
            return View(book);
        }


        public async Task<IActionResult> Details(int id)
        {
            _logger.LogInformation("Fetching Details of book...");

            var book = await _context.Books.FindAsync(id);
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _logger.LogInformation("DeleteConfirmed book...");
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
