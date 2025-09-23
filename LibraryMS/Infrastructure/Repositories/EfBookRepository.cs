using LibraryMS.Domain.Contracts.Repository_Contracts;
using LibraryMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryMS.Infrastructure.Repositories;

public class EfBookRepository : IBookRepository
{
    private readonly AppDbContext _context = new AppDbContext();
    public int Add(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();
        return book.Id;
    }

    public List<Book> GetAll()
    {
        return _context.Books
            .Include(x => x.BookCategory)
            .Include(x => x.BorrowedBooks).ToList();
    }

    public List<Book> GetBorrowedBooks()
    {
        return _context.Books
            .Include(x => x.BookCategory)
            .Include(x => x.BorrowedBooks)
            .Where(x => x.IsBorrow == true).ToList();
    }

    public List<Book> GetUnBorrowedBooks()
    {
        return _context.Books
            .Include(x => x.BookCategory)
            .Include(x => x.BorrowedBooks)
            .Where(x => x.IsBorrow == false).ToList();
    }


    public Book? GetById(int id)
    {
        return _context.Books
            .Include(x => x.BookCategory)
            .Include(x => x.BorrowedBooks)
            .FirstOrDefault(x => x.Id == id);
    }

    public void Update(Book book)
    {
        var existBook = _context.Books.Find(book.Id);
        if (existBook != null)
        {
            existBook.Author = book.Author;
            existBook.Description = book.Description;
            existBook.Title = book.Title;
            existBook.BookCategoryId = book.BookCategoryId;
            _context.SaveChanges();
        }
    }

    public void Delete(int id)
    {
        var existBook = _context.Books.Find(id);
        if (existBook != null)
        {
            _context.Books.Remove(existBook);
            _context.SaveChanges();
        }
    }
}