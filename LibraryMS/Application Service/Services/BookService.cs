using LibraryMS.Domain.Contracts.Repository_Contracts;
using LibraryMS.Domain.Contracts.Service_Contracts;
using LibraryMS.Domain.Entities;
using LibraryMS.Infrastructure.Repositories;

namespace LibraryMS.Application_Service.Services;

public class BookService: IBookService
{
    private readonly IBookRepository _bookRepository = new EfBookRepository();
    public int Add(Book book)
    {
        _bookRepository.Add(book);
        return book.Id;
    }

    public Book GetById(int id)
    {
        var book =  _bookRepository.GetById(id);
        if (book == null)
            throw new Exception("No book found with this id");
        return book;
    }

    public List<Book> GetAll()
    {
        return _bookRepository.GetAll();
    }

    public List<Book> GetBorrowedBooks()
    {
        return _bookRepository.GetBorrowedBooks();
    }

    public List<Book> GetUnBorrowedBooks()
    {
        return _bookRepository.GetUnBorrowedBooks();
    }

    public void Update(Book book)
    {
        _bookRepository.Update(book);
    }

    public void Delete(int id)
    {
       _bookRepository.Delete(id);
    }
}