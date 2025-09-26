using System.Security.Principal;

namespace LibraryMS.Domain.Entities;

public class WishList
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public int BookId { get; set; }
    public Book Book { get; set; }

    public WishList(int userId, int bookId)
    {
        UserId = userId;
        BookId = bookId;
        CreatedAt = DateTime.Now;
    }
}