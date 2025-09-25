using LibraryMS.Application_Service.DTOs;
using LibraryMS.Domain.Entities;

namespace LibraryMS.Domain.Contracts.Service_Contracts;

public interface IBookService
{ 
    int Add(string title, string description, string author, int bookCategory);
    Book GetById(int id);
    List<BookDto> GetAll();
    List<BookDto> GetBorrowedBooks();
    List<BookDto> GetUnBorrowedBooks();
    void Update(Book book);
    void Delete(int id);
}