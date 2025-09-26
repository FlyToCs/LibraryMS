using LibraryMS.Domain.Contracts.Repository_Contracts;
using LibraryMS.Domain.Entities;
using LibraryMS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryMS.Infrastructure.Repositories;

public class WishListRepository : IWishListRepository
{
    public int Add(WishList wishList)
    {
        using var context = new AppDbContext();
        context.WishLists.Add(wishList);
        context.SaveChanges();
        return wishList.Id;
    }

    public bool Delete(int id)
    {
        using var context = new AppDbContext();
        var wish = context.WishLists.FirstOrDefault(x => x.Id == id);
        if (wish != null)
        {
            context.WishLists.Remove(wish);
            context.SaveChanges();
            return true;
        }

        return false;
    }

    public WishList? Get(int id)
    {
        using var context = new AppDbContext();
        return context.WishLists
            .Include(x=>x.User)
            .Include(x=>x.Book)
            .FirstOrDefault(x => x.Id == id);
    }

    public List<WishList> GetAllByUserId(int userId)
    {
        using var context = new AppDbContext();
        return context.WishLists
            .Include(x => x.User)
            .Include(x => x.Book)
            .Where(x => x.UserId == userId).ToList();
    }
}