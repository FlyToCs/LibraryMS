using LibraryMS.Application_Service.DTOs;
using LibraryMS.Domain.Contracts.Repository_Contracts;
using LibraryMS.Domain.Contracts.Service_Contracts;
using LibraryMS.Domain.Entities;
using LibraryMS.Infrastructure.Repositories;

namespace LibraryMS.Application_Service.Services;

public class BookService: IBookService
{
    private readonly IBookRepository _bookRepository = new BookRepository();
    private readonly IReviewService _reviewService = new ReviewService();
    public int Add(string title, string description, string author, int categoryId)
    {
        var newBook = new Book(title, description, author,categoryId);
        _bookRepository.Add(newBook);
        return newBook.Id;
    }

    public Book GetById(int id)
    {
        var book =  _bookRepository.GetById(id);
        if (book == null)
            throw new Exception("No book found with this id");
        return book;
    }

    public List<BookDto> GetAll()
    {
        var bookList =  _bookRepository.GetAll();
        return bookList.Select(x => new BookDto()
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            Author = x.Author,
            CategoryName = x.BookCategory.Name,
            AvgScore = _reviewService.GetAverageRatingByBookId(x.Id)
        }).ToList();
    }

    public List<BookDto> GetBorrowedBooks()
    {
        var bookList =  _bookRepository.GetBorrowedBooks();
        return bookList.Select(x => new BookDto()
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            Author = x.Author,
            CategoryName = x.BookCategory.Name,
            AvgScore = _reviewService.GetAverageRatingByBookId(x.Id)
        }).ToList();
    }

    public List<BookDto> GetUnBorrowedBooks()
    {
        var bookList  = _bookRepository.GetUnBorrowedBooks();
        return bookList.Select(x => new BookDto()
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            Author = x.Author,
            CategoryName = x.BookCategory.Name,
            AvgScore = _reviewService.GetAverageRatingByBookId(x.Id)
        }).ToList();
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