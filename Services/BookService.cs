using BookStoreApp.Data;
using BookStoreApp.Models;

namespace BookStoreApp.Services
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public List<Book> GetAllBooks() => _context.Books.ToList();
    }
}
