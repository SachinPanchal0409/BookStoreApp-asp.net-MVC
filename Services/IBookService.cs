using BookStoreApp.Models;

namespace BookStoreApp.Services
{
    public interface IBookService
    {
      List<Book> GetAllBooks();
    }
}
