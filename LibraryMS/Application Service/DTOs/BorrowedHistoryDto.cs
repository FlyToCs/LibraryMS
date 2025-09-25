namespace LibraryMS.Application_Service.DTOs;

public class BorrowedHistoryDto
{
    public string Name { get; set; }
    public DateTime BorrowedDate { get; set; }
    public DateTime? ReturnDate { get; set; }

}