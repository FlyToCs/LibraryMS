using LibraryMS.Domain.Entities;

namespace LibraryMS.Domain.Contracts.Repository_Contracts;

public interface IBorrowedBookRepository
{
    void Add(BorrowedBook borrowedBook);
    void Update(BorrowedBook borrowedBook);
    BorrowedBook? GetActiveBorrowByBookId(int bookId);
    BorrowedBook? GetActiveBorrowByUserAndBook(int userId, int bookId);
    List<BorrowedBook> GetBorrowHistoryByUserId(int userId);
    List<BorrowedBook> GetActiveBorrowsByUserId(int userId);
}