using LibraryMS.Domain.Contracts.Service_Contracts;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Enums;
using LibraryMS.Domain.Exceptions;

namespace LibraryMS.Application_Service.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserService _userService = new UserService();

    public User Register(string firstName, string lastName, string username, string password, string email, UserRoleEnum roll)
    {
        var existUser = _userService.GetAll().FirstOrDefault(x => x.Username == username);
        if (existUser != null)
        {
            throw new Exception("Username already exists");
        }

        var newUser = new User()
        {
            FirstName = firstName,
            LastName = lastName,
            Username = username,
            Password = password,
            Email = email,
            UserRole = roll
        };
        _userService.Add(newUser);
        return newUser;
    }

    public User? Login(string username, string password)
    {
        var user = _userService.GetAll().FirstOrDefault(x => x.Username == username);
        if (user == null)
            throw new Exception("User not found");
        if (user.Password != password)
            throw new Exception("username or password is incorrect");
        
        if (!user.IsActive)
            throw new Exception("User is inactive");

        return user;
    }
}