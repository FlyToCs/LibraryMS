namespace LibraryMS.Domain.Contracts.Service_Contracts;

public interface IReviewService
{
    int Add(int userId, int bookId, int rating, string? comment);
    
}