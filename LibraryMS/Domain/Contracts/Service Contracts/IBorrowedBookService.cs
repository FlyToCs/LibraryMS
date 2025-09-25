using LibraryMS.Application_Service.DTOs;
using LibraryMS.Domain.Entities;

namespace LibraryMS.Domain.Contracts.Service_Contracts;

public interface IBorrowedBookService
{
    void BorrowBook(int userId, int bookId);
    void ReturnBook(int userId, int bookId);
    List<BorrowedHistoryDto> GetMyBorrowHistory(int userId);
    List<BorrowBookDto> GetMyActiveBorrows(int userId);
}