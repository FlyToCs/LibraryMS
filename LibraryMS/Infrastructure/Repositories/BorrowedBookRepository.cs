using LibraryMS.Domain.Contracts.Repository_Contracts;
using LibraryMS.Domain.Entities;
using LibraryMS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryMS.Infrastructure.Repositories;

public class BorrowedBookRepository : IBorrowedBookRepository
{
    public void Add(BorrowedBook borrowedBook)
    {
        using var context = new AppDbContext();
        context.BorrowedBooks.Add(borrowedBook);
        context.SaveChanges();
    }

    public void Update(BorrowedBook borrowedBook)
    {
        using var context = new AppDbContext();
        context.BorrowedBooks.Update(borrowedBook);
        context.SaveChanges();
    }

    public BorrowedBook? GetActiveBorrowByBookId(int bookId)
    {
        using var context = new AppDbContext();
        return context.BorrowedBooks
            .FirstOrDefault(b => b.BookId == bookId && b.ReturnDate == null);
    }

    public BorrowedBook? GetActiveBorrowByUserAndBook(int userId, int bookId)
    {
        using var context = new AppDbContext();
        return context.BorrowedBooks
            .FirstOrDefault(b => b.UserId == userId && b.BookId == bookId && b.ReturnDate == null);
    }

    public List<BorrowedBook> GetBorrowHistoryByUserId(int userId)
    {
        using var context = new AppDbContext();
        return context.BorrowedBooks
            .Where(b => b.UserId == userId)
            .Include(b => b.Book) 
            .ToList();
    }

    public List<BorrowedBook> GetActiveBorrowsByUserId(int userId)
    {
        using var context = new AppDbContext();
        return context.BorrowedBooks
            .Where(b => b.UserId == userId && b.ReturnDate == null) 
            .Include(b => b.Book) 
            .ToList();
    }
}