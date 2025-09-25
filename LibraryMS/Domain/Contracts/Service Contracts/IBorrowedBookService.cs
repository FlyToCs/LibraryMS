using LibraryMS.Domain.Entities;

namespace LibraryMS.Domain.Contracts.Service_Contracts;

public interface IBorrowedBookService
{
    void BorrowBook(int userId, int bookId);
    void ReturnBook(int userId, int bookId);
    List<BorrowedBook> GetMyBorrowHistory(int userId);
    List<BorrowedBook> GetMyActiveBorrows(int userId);
}