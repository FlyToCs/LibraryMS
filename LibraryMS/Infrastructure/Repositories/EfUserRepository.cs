using LibraryMS.Domain.Contracts.Repository_Contracts;
using LibraryMS.Domain.Entities;

namespace LibraryMS.Infrastructure.Repositories;

public class EfUserRepository : IUserRepository
{
    // این خط را حذف کنید چون دیگر به یک نمونه ثابت نیاز نداریم
    // private readonly AppDbContext _appDbContext = new AppDbContext();

    public int Add(User user)
    {
        // برای هر متد یک context جدید بسازید
        using (var _appDbContext = new AppDbContext())
        {
            _appDbContext.Users.Add(user);
            _appDbContext.SaveChanges();
            return user.Id;
        }
    }

    public User? GetById(int id)
    {
        using (var _appDbContext = new AppDbContext())
        {
            return _appDbContext.Users.Find(id);
        }
    }

    public User? GetByUserName(string userName)
    {
        using (var _appDbContext = new AppDbContext())
        {
            return _appDbContext.Users.SingleOrDefault(x => x.Username == userName);
        }
    }

    public List<User> GetAll()
    {
        using (var _appDbContext = new AppDbContext())
        {
            return _appDbContext.Users.ToList();
        }
    }

    public List<User> GetAllActive()
    {
        using (var _appDbContext = new AppDbContext())
        {
            return _appDbContext.Users.Where(x => x.IsActive == true).ToList();
        }
    }

    public List<User> GetAllInActive()
    {
        using (var _appDbContext = new AppDbContext())
        {
            return _appDbContext.Users.Where(x => x.IsActive == false).ToList();
        }
    }

    public bool Activate(int id)
    {
        using (var _appDbContext = new AppDbContext())
        {
            var user = _appDbContext.Users.Find(id);
            if (user != null)
            {
                user.IsActive = true;
                _appDbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }

    public bool DeActivate(int id)
    {
        using (var _appDbContext = new AppDbContext())
        {
            var user = _appDbContext.Users.Find(id);
            if (user != null)
            {
                user.IsActive = false;
                _appDbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }

    public void Update(User user)
    {
        using (var _appDbContext = new AppDbContext())
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
    }

    public void Delete(int id)
    {
        using (var _appDbContext = new AppDbContext())
        {
            var user = _appDbContext.Users.Find(id);
            if (user != null)
            {
                _appDbContext.Users.Remove(user);
                _appDbContext.SaveChanges();
            }
        }
    }
}