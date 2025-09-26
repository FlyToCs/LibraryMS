using LibraryMS.Application_Service.DTOs;
using LibraryMS.Domain.Entities;

namespace LibraryMS.Domain.Contracts.Service_Contracts;

public interface IWishListService
{
    int Add(int userId, int bookId);
    void Delete(int id);
    List<WishListDot> GetAllByUserId(int userId);
}