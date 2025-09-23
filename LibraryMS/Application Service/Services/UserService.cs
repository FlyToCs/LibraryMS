using LibraryMS.Domain.Contracts.Repository_Contracts;
using LibraryMS.Domain.Contracts.Service_Contracts;
using LibraryMS.Domain.Entities;
using LibraryMS.Infrastructure;
using LibraryMS.Infrastructure.Repositories;

namespace LibraryMS.Application_Service.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepo = new EfUserRepository();
    public int Add(User user)
    {
        if (string.IsNullOrWhiteSpace(user.Username))
            throw new ArgumentException("Username is required.");

        return _userRepo.Add(user);
    }

    public User GetById(int id)
    {
        var user = _userRepo.GetById(id);
        if (user == null)
            throw new KeyNotFoundException($"User with id {id} not found.");
        return user;
    }

    public List<User> GetAll()
    {
        return _userRepo.GetAll();
    }

    public void Update(User user)
    {
        var existing = _userRepo.GetById(user.Id);
        if (existing == null)
            throw new KeyNotFoundException($"User with id {user.Id} not found.");

        _userRepo.Update(user);
    }

    public void Delete(int id)
    {
        var existing = _userRepo.GetById(id);
        if (existing == null)
            throw new KeyNotFoundException($"User with id {id} not found.");

        _userRepo.Delete(id);
    }
}