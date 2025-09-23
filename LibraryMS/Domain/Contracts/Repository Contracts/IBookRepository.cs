using LibraryMS.Domain.Entities;

namespace LibraryMS.Domain.Contracts.Repository_Contracts;

public interface IBookRepository
{
    int Add(Book book );
    List<Book> GetAll();
    List<Book> GetAllAvailable();
    Book? GetById(int id);
    void Update(Book book);
    void Delete(int id);
}