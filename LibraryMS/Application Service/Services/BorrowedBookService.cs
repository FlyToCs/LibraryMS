using LibraryMS.Application_Service.DTOs;
using LibraryMS.Domain.Contracts.Repository_Contracts;
using LibraryMS.Domain.Contracts.Service_Contracts;
using LibraryMS.Domain.Entities;
using LibraryMS.Infrastructure.Repositories;

namespace LibraryMS.Application_Service.Services;

public class BorrowedBookService : IBorrowedBookService
{
    private readonly IBorrowedBookRepository _borrowedBookRepository = new EfBorrowedBookRepository();
    private readonly IBookRepository _bookRepository = new EfBookRepository();
    private readonly IUserRepository _userRepository = new EfUserRepository();


    public void BorrowBook(int userId, int bookId)
    {
        var book = _bookRepository.GetById(bookId);
        var user = _userRepository.GetById(userId);
        if (book == null || user == null)
        {
            throw new Exception("book or user not found");
        }

        var existingBorrow = _borrowedBookRepository.GetActiveBorrowByBookId(bookId);
        if (existingBorrow != null)
        {
            throw new Exception("the book has borrowed or not exist");
        }


        var newBorrow = new BorrowedBook(userId, bookId);

        _borrowedBookRepository.Add(newBorrow);
    }

    public void ReturnBook(int userId, int bookId)
    {

        var borrowToReturn = _borrowedBookRepository.GetActiveBorrowByUserAndBook(userId, bookId);
        if (borrowToReturn == null)
        {
            throw new Exception("you didn't borrowed this book or returned it before");
        }
        borrowToReturn.ReturnDate = DateTime.Now;

        _borrowedBookRepository.Update(borrowToReturn);
    }

    public List<BorrowedHistoryDto> GetMyBorrowHistory(int userId)
    {
        var borrowedHistory=  _borrowedBookRepository.GetBorrowHistoryByUserId(userId);
        return borrowedHistory.Select(x => new BorrowedHistoryDto()
        {
            Name = x.Book.Title,
            BorrowedDate = x.BorrowDate,
            ReturnDate = x.ReturnDate
        }).ToList();
    }

    public List<BorrowBookDto> GetMyActiveBorrows(int userId)
    {
        var borrowedList =  _borrowedBookRepository.GetActiveBorrowsByUserId(userId);
        return borrowedList.Select(x => new BorrowBookDto()
        {
            Id = x.BookId,
            Title = x.Book.Title,
            BorrowedDate = x.BorrowDate,
            ReturnDate = x.ReturnDate
        }).ToList();
    }
}