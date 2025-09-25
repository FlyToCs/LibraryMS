namespace LibraryMS.Application_Service.DTOs;

public class BorrowBookDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime BorrowedDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}