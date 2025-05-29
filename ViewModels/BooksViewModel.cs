using BookStoreApp.Models;

namespace BookStoreApp.ViewModels
{
    public class BooksViewModel
    {
        public List<Book> Books { get; set; }
        public string SearchTerm { get; set; }
    }
}
