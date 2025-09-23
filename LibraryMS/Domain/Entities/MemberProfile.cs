namespace LibraryMS.Domain.Entities;

public class MemberProfile : User
{
    public DateTime RegistrationDate { get; set; }
    public List<BorrowedBook> BorrowedBooks { get; set; }
}