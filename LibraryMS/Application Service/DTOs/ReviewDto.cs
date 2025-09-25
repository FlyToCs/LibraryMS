namespace LibraryMS.Application_Service.DTOs;

public class ReviewDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string? Comment { get; set; }
    public decimal Rating { get; set; }
    public string BookName { get; set; }

}