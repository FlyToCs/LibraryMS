using LibraryMS.Domain.Entities;

namespace LibraryMS.Domain.Contracts.Repository_Contracts;

public interface IBookRepository
{
    int Add(Book book );
    List<Book> GetAll();
    List<Book> GetBorrowedBooks();
    List<Book> GetUnBorrowedBooks();
    Book? GetById(int id);
    void Update(Book book);
    void Delete(int id);
}