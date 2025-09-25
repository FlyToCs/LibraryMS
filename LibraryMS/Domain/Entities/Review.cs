using LibraryMS.Domain.Enums;

namespace LibraryMS.Domain.Entities;

public class Review
{
    public int Id { get; set; }
    public string? Comment { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }
    public ReviewStatusEnum Status { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public int BookId { get; set; }
    public Book Book{ get; set; }

    public Review(int userId, int bookId, int rating, string? comment)
    {
        UserId = userId;
        BookId = bookId;
        Rating = rating;
        Comment = comment;
        CreatedAt = DateTime.Now;
        Status = ReviewStatusEnum.Pending;
    }
}