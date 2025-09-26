using LibraryMS.Domain.Contracts.Repository_Contracts;
using LibraryMS.Domain.Entities;
using LibraryMS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryMS.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    public int Add(Book book)
    {
        using var context = new AppDbContext();
        context.Books.Add(book);
        context.SaveChanges();
        return book.Id;
    }

    public List<Book> GetAll()
    {
        using var context = new AppDbContext();
        return context.Books
            .Include(x => x.BookCategory)
            .Include(x => x.BorrowedBooks)
            .Include(x=>x.Reviews)
            .ToList();
    }

    public List<Book> GetBorrowedBooks()
    {
        using var context = new AppDbContext();
        return context.Books
            .Include(x => x.BookCategory)
            .Include(b => b.Reviews)
            .Where(book => book.BorrowedBooks.Any(borrow => borrow.ReturnDate == null))
            .ToList();
    }
    public List<Book> GetUnBorrowedBooks()
    {
        using var context = new AppDbContext();
        return context.Books
            .Include(x => x.BookCategory)
            .Include(b => b.Reviews)
            .Where(book => book.BorrowedBooks.All(borrow => borrow.ReturnDate != null))
            
            .ToList();

    }


    public Book? GetById(int id)
    {
        using var context = new AppDbContext();
        return context.Books
            .Include(x => x.BookCategory)
            .Include(x => x.BorrowedBooks)
            .FirstOrDefault(x => x.Id == id);
    }

    public void Update(Book book)
    {
        using var context = new AppDbContext();
        var existBook = context.Books.FirstOrDefault(x => x.Id == book.Id);
        if (existBook != null)
        {
            existBook.Author = book.Author;
            existBook.Description = book.Description;
            existBook.Title = book.Title;
            existBook.BookCategoryId = book.BookCategoryId;
            context.SaveChanges();
        }
    }

    public void Delete(int id)
    {
        using var context = new AppDbContext();
        var existBook = context.Books.FirstOrDefault(x=>x.Id == id);
        if (existBook != null)
        {
            context.Books.Remove(existBook);
            context.SaveChanges();
        }
    }
}