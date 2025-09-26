using LibraryMS.Domain.Entities;

namespace LibraryMS.Domain.Contracts.Repository_Contracts;

public interface IWishListRepository
{
    int Add(WishList wishList);
    bool Delete(int id);
    WishList? Get(int id);
    List<WishList> GetAllByUserId(int userId);
}