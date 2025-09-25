using LibraryMS.Domain.Contracts.Repository_Contracts;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace LibraryMS.Infrastructure.Repositories;

public class EfReviewRepository : IReviewRepository
{

    public int Add(Review review)
    {
        using var context = new AppDbContext();

        context.Reviews.Add(review);
        context.SaveChanges();
        return review.Id;
    }

    public Review? GetById(int id)
    {
        using var context = new AppDbContext();
        return context.Reviews.Find(id);
    }

    public void Update(Review review) 
    {
        using var context = new AppDbContext();
        context.Reviews.Update(review);
        context.SaveChanges();
    }
    public bool Delete(int id)
    {
        using var context = new AppDbContext();
        var review = context.Reviews.Find(id);
        if (review != null)
        {
            context.Reviews.Remove(review);
            context.SaveChanges();
            return true;
        }

        return false;
    }

    public List<Review> GetApprovedReviewsByBookId(int bookId)
    {
        using var context = new AppDbContext();
        return context.Reviews
            .Where(review => review.BookId == bookId && review.Status == ReviewStatusEnum.Approved)
            .Include(review => review.User)
            .ToList();
    }

    public List<Review> GetPendingReviews()
    {
        using var context = new AppDbContext();
        return context.Reviews
            .Where(x => x.Status == ReviewStatusEnum.Pending)
            .Include(x => x.User) 
            .Include(x => x.Book) 
            .ToList();
    }

    public Review? GetByUserAndBook(int userId, int bookId)
    {
        using var context = new AppDbContext();
        return context.Reviews.FirstOrDefault(r => r.UserId == userId && r.BookId == bookId);
    }

    public decimal GetAverageRatingForBook(int bookId)
    {
        using var context = new AppDbContext();
        var approvedReviews = context.Reviews
            .Where(r => r.BookId == bookId && r.Status == ReviewStatusEnum.Approved);

        if (!approvedReviews.Any())
            return 0;
        
        return approvedReviews.Average(r => (decimal)r.Rating);
    }
}