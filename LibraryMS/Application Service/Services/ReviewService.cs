using LibraryMS.Application_Service.DTOs;
using LibraryMS.Domain.Contracts.Repository_Contracts;
using LibraryMS.Domain.Contracts.Service_Contracts;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Enums;
using LibraryMS.Infrastructure.Repositories;

namespace LibraryMS.Application_Service.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository = new EfReviewRepository();
    private readonly IUserRepository _userRepository = new EfUserRepository();
    private readonly IBookRepository _bookRepository = new EfBookRepository();
    public void Add(int userId, int bookId, int rating, string? comment)
    {
        var user = _userRepository.GetById(userId);
        var book = _bookRepository.GetById(bookId);
        if (user == null || book == null)
        {
            throw new Exception("User or Book not found.");
        }

        var existingReview = _reviewRepository.GetByUserAndBook(userId, bookId);
        if (existingReview != null)
        {
            throw new Exception("You have already submitted a review for this book.");
        }

        var newReview = new Review(userId, bookId, rating, comment);
        _reviewRepository.Add(newReview);
    }

    public void Edit(int reviewId, int rating, string? comment, int currentUserId)
    {

        var review = _reviewRepository.GetById(reviewId);
        var currentUser = _userRepository.GetById(currentUserId);
        if (review == null)
            throw new Exception("Review not found with that ID.");

        if (currentUser == null)
            throw new Exception("Current user not found.");

        bool isOwner = review.UserId == currentUser.Id;
        bool isAdmin = currentUser.UserRole == UserRoleEnum.Admin;


        if (!isOwner && !isAdmin)
            throw new Exception("You do not have permission to edit this review.");

        review.Rating = rating;
        review.Comment = comment;
        review.Status = ReviewStatusEnum.Pending;

        _reviewRepository.Update(review);
    }

    public void Delete(int reviewId, int currentUserId)
    {
        var review = _reviewRepository.GetById(reviewId);
        var currentUser = _userRepository.GetById(currentUserId);
        if (review == null)
            throw new Exception("Review not found with that ID.");

        if (currentUser == null)
            throw new Exception("Current user not found.");

        bool isOwner = review.UserId == currentUser.Id;
        bool isAdmin = currentUser.UserRole == UserRoleEnum.Admin;

        if (!isOwner && !isAdmin)
            throw new Exception("You do not have permission to edit this review.");

        _reviewRepository.Delete(reviewId);
    }


    public void ChangeStatus(int reviewId, ReviewStatusEnum newStatus, int userId)
    {

        var currentUser = _userRepository.GetById(userId);
        if (currentUser == null || currentUser.UserRole != UserRoleEnum.Admin)
            throw new Exception("You do not have permission to perform this action.");

        var review = _reviewRepository.GetById(reviewId);
        if (review == null)
            throw new Exception("Review not found with that ID.");

        review.Status = newStatus;
        _reviewRepository.Update(review);
    }

    public List<ReviewDto> GetPendingReviews()
    {
        var reviews = _reviewRepository.GetPendingReviews();
        return reviews.Select(x => new ReviewDto()
        {
            Id = x.Id,
            FullName = $"{x.User.FirstName} {x.User.LastName}",
            Comment = x.Comment,
            Rating = x.Rating,
            BookName = x.Book.Title
        }).ToList();  

    }

    public List<Review> GetApprovedReviewsByBookId(int bookId)
    {
        return _reviewRepository.GetApprovedReviewsByBookId(bookId);
    }

    public decimal GetAverageRatingByBookId(int bookId)
    {
        return _reviewRepository.GetAverageRatingForBook(bookId);
    }
    public List<ReviewDto> GetMyReviews(int userId)
    {
        var reviews =  _reviewRepository.GetByUserId(userId);
        return reviews.Select(x => new ReviewDto()
        {
            Id = x.Id,
            FullName = $"{x.User.FirstName} {x.User.LastName}",
            Comment = x.Comment,
            Rating = x.Rating,
            BookName = x.Book.Title
        }).ToList();
    }
}