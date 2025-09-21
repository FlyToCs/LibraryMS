namespace LibraryMS.Domain.Entities;

public class Member
{
    public DateTime RegistrationDate { get; set; }
    public List<BorrowedBook> BorrowedBooks { get; set; }
}