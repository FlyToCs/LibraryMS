using LibraryMS.Domain.Contracts.Repository_Contracts;
using LibraryMS.Domain.Entities;

namespace LibraryMS.Infrastructure.Repositories;

public class EfUserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext = new AppDbContext();
    public int Add(User user)
    {
        _appDbContext.Users.Add(user);
        _appDbContext.SaveChanges();
        return user.Id;
    }

    public User? GetById(int id)
    {
        return _appDbContext.Users.Find(id);
    }

    public User? GetByUserName(string userName)
    {
        return _appDbContext.Users.SingleOrDefault(x => x.Username == userName);
    }

    public List<User> GetAll()
    {
        return _appDbContext.Users.ToList();
    }

    public List<User> GetAllActive()
    {
        return _appDbContext.Users.Where(x=>x.IsActive == true).ToList();
    }

    public List<User> GetAllInActive()
    {
        return _appDbContext.Users.Where(x => x.IsActive == false).ToList();
    }


    // ask a qs 
    public void Update(User user)
    {
        var findUser = _appDbContext.Users.SingleOrDefault(x => x.Id == user.Id);
        if (findUser != null)
        {
            findUser.FirstName = user.FirstName;
            findUser.LastName = user.LastName;
            findUser.Email = user.Email;
            findUser.IsActive = user.IsActive;
            _appDbContext.SaveChanges();
        }
    }


    // public void Update(User user)
    // {
    //     _appDbContext.Users.Update(user);
    //     _appDbContext.SaveChanges();
    // }



    public void Delete(int id)
    {
        var user = _appDbContext.Users.Find(id);
        if (user != null)
        {
            _appDbContext.Users.Remove(user);
            _appDbContext.SaveChanges();
        }
    }
}