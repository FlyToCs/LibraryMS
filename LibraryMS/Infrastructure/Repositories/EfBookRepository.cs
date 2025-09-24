using LibraryMS.Domain.Contracts.Repository_Contracts;
using LibraryMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryMS.Infrastructure.Repositories;

public class EfBookRepository : IBookRepository
{
    // این خط باید حذف شود
    // private readonly AppDbContext _context = new AppDbContext();

    public int Add(Book book)
    {
        using (var _context = new AppDbContext())
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return book.Id;
        }
    }

    public List<Book> GetAll()
    {
        using (var _context = new AppDbContext())
        {
            return _context.Books
                .Include(x => x.BookCategory)
                .Include(x => x.BorrowedBooks)
                .ToList();
        }
    }

    public List<Book> GetBorrowedBooks()
    {
        using (var _context = new AppDbContext())
        {
            return _context.Books
                .Include(x => x.BookCategory)
                .Include(x => x.BorrowedBooks)
                .Where(x => x.IsBorrow == true)
                .ToList();
        }
    }

    public List<Book> GetUnBorrowedBooks()
    {
        using (var _context = new AppDbContext())
        {
            return _context.Books
                .Include(x => x.BookCategory)
                .Include(x => x.BorrowedBooks)
                .Where(x => x.IsBorrow == false)
                .ToList();
        }
    }

    public Book? GetById(int id)
    {
        using (var _context = new AppDbContext())
        {
            return _context.Books
                .Include(x => x.BookCategory)
                .Include(x => x.BorrowedBooks)
                .FirstOrDefault(x => x.Id == id);
        }
    }

    public void Update(Book book)
    {
        using (var _context = new AppDbContext())
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
    }

    public void Delete(int id)
    {
        using (var _context = new AppDbContext())
        {
            var existBook = _context.Books.Find(id);
            if (existBook != null)
            {
                _context.Books.Remove(existBook);
                _context.SaveChanges();
            }
        }
    }
}