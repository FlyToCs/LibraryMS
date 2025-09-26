using LibraryMS.Domain.Contracts.Repository_Contracts;
using LibraryMS.Domain.Entities;
using LibraryMS.Infrastructure.Persistence;

namespace LibraryMS.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{

    public int Add(User user)
    {
        using var appDbContext = new AppDbContext();
        appDbContext.Users.Add(user);
        appDbContext.SaveChanges();
        return user.Id;
    }

    public User? GetById(int id)
    {
        using var appDbContext = new AppDbContext();
        return appDbContext.Users.FirstOrDefault(x => x.Id == id);
    }

    public User? GetByUserName(string userName)
    {
        using var appDbContext = new AppDbContext();
        return appDbContext.Users.FirstOrDefault(x => x.Username == userName);
    }

    public List<User> GetAll()
    {
        using var appDbContext = new AppDbContext();
        return appDbContext.Users.ToList();
    }

    public List<User> GetAllActive()
    {
        using var appDbContext = new AppDbContext();
        return appDbContext.Users.Where(x => x.IsActive == true).ToList();
    }

    public List<User> GetAllInActive()
    {
        using var appDbContext = new AppDbContext();
        return appDbContext.Users.Where(x => x.IsActive == false).ToList();
    }

    public List<User> GetUserHasPenaltyAmount()
    {
        using var appDbContext = new AppDbContext();
        return appDbContext.Users.Where(x => x.PenaltyAmount > 0).ToList();
    }

    public bool Activate(int id)
    {
        using var appDbContext = new AppDbContext();
        var user = appDbContext.Users.FirstOrDefault(x => x.Id == id);
        if (user != null)
        {
            user.IsActive = true;
            appDbContext.SaveChanges();
            return true;
        }
        return false;
    }

    public bool DeActivate(int id)
    {
        using var appDbContext = new AppDbContext();
        var user = appDbContext.Users.FirstOrDefault(x => x.Id == id);
        if (user != null)
        {
            user.IsActive = false;
            appDbContext.SaveChanges();
            return true;
        }
        return false;
    }

    public void Update(User user)
    {
        using var appDbContext = new AppDbContext();
        var findUser = appDbContext.Users.FirstOrDefault(x => x.Id == user.Id);
        if (findUser != null)
        {
            findUser.FirstName = user.FirstName;
            findUser.LastName = user.LastName;
            findUser.Email = user.Email;
            findUser.IsActive = user.IsActive;
            findUser.PenaltyAmount = user.PenaltyAmount;
            appDbContext.SaveChanges();
        }
    }

    public void Delete(int id)
    {
        using var appDbContext = new AppDbContext();
        var user = appDbContext.Users.FirstOrDefault(x=>x.Id == id);
        if (user != null)
        {
            appDbContext.Users.Remove(user);
            appDbContext.SaveChanges();
        }
    }
}