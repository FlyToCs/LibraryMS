using LibraryMS.Domain.Entities;

namespace LibraryMS.Domain.Contracts.Repository_Contracts;

public interface IReviewRepository
{
    int Add(Review review);
    Review? GetById(int id);
    void Update(Review review);
    bool Delete(int id);
    List<Review> GetApprovedReviewsByBookId(int bookId);
    List<Review> GetPendingReviews();
    Review? GetByUserAndBook(int userId, int bookId);
    decimal GetAverageRatingForBook(int bookId);
}