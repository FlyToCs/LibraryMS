using LibraryMS.Application_Service.DTOs;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Enums;

namespace LibraryMS.Domain.Contracts.Service_Contracts;

public interface IReviewService
{
    void Add(int userId, int bookId, int rating, string? comment);
    void Edit(int reviewId, int rating, string? comment, int currentUserId);
    void Delete(int reviewId, int currentUserId);
    void ChangeStatus(int reviewId, ReviewStatusEnum newStatus, int userId);
    List<ReviewDto> GetPendingReviews();
    List<Review> GetApprovedReviewsByBookId(int bookId);
    decimal GetAverageRatingByBookId(int bookId);
    List<ReviewDto> GetMyReviews(int userId);
}