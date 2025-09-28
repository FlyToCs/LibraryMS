using System.Security.Cryptography.X509Certificates;
using LibraryMS.Application_Service.DTOs;
using LibraryMS.Domain.Contracts.Repository_Contracts;
using LibraryMS.Domain.Contracts.Service_Contracts;
using LibraryMS.Domain.Entities;
using LibraryMS.Infrastructure;
using LibraryMS.Infrastructure.Repositories;

namespace LibraryMS.Application_Service.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepo = new UserRepository();
    public int Add(User user)
    {
        if (string.IsNullOrWhiteSpace(user.Username))
            throw new ArgumentException("Username is required.");

        return _userRepo.Add(user);
    }

    public UserDto GetById(int id)
    {
        var user = _userRepo.GetById(id);
        if (user == null)
            throw new KeyNotFoundException($"User with id {id} not found.");
        return new UserDto()
        {
            Id = user.Id,
            FullName = $"{user.FirstName} {user.LastName}",
            Username = user.Username,
            Roll = user.UserRole,
            Status = user.IsActive,
            PenaltyAmount = user.PenaltyAmount
            
        };
    }

    public void Activate(int id)
    {
        _userRepo.Activate(id);
    }

    public void Deactivate(int id)
    {
        _userRepo.DeActivate(id);
    }

    public List<User> GetAll()
    {
        return _userRepo.GetAll();
    }

    public UserDto? GetUserByUserName(string userName)
    {
       return _userRepo.GetByUserName(userName);
    }

    public List<UserDto> GetAllActive()
    {
        var user =  _userRepo.GetAllActive();
        return user.Select(x => new UserDto()
        {
            Id = x.Id,
            FullName = $"{x.FirstName} {x.LastName}",
            Username = x.Username,
            Roll = x.UserRole,
            Status = x.IsActive
        }).ToList();
    }

    public List<UserDto> GetAllInActive()
    {
        var user =  _userRepo.GetAllInActive();
        return user.Select(x => new UserDto()
        {
            Id = x.Id,
            FullName = $"{x.FirstName} {x.LastName}",
            Username = x.Username,
            Roll = x.UserRole,
            Status = x.IsActive
        }).ToList();
    }

    public List<UserDto> GetUserHasPenaltyAmount()
    {
        var user =  _userRepo.GetUserHasPenaltyAmount();
        return user.Select(x=>new UserDto()
        {
            Id = x.Id,
            FullName = $"{x.FirstName} {x.LastName}",
            Username = x.Username,
            Roll = x.UserRole,
            Status = x.IsActive
        }).ToList();
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