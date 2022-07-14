using BooksApp.Models;

namespace BooksApp.Repositories
{
    public interface IBookRepository
    {
        IReadOnlyList<Book> GetAll();
        IReadOnlyList<Book> GetByShop(int id);
        Book GetByBookName( string bookName );
        Book GetById(int id);
        void Update( Book book );
        void Delete( Book book);
    }
}
