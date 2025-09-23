using LibraryMS.Domain.Enums;

namespace LibraryMS.Domain.Entities;

public class User 
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public UserRoleEnum UserRole { get; set; }
    public bool IsActive { get; set; }
    public List<BorrowedBook> BorrowedBooks { get; set; }

}