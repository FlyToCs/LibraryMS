using LibraryMS.Domain.Entities;

namespace LibraryMS.Domain.Contracts.Service_Contracts;

public interface IBookService
{ 
    int Add(string title, string description, string author, int bookCategory);
    Book GetById(int id);
    List<Book> GetAll();
    List<Book> GetBorrowedBooks();
    List<Book> GetUnBorrowedBooks();
    void Update(Book book);
    void Delete(int id);
}