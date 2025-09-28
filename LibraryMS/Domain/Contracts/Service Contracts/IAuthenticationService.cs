using LibraryMS.Application_Service.DTOs;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Enums;

namespace LibraryMS.Domain.Contracts.Service_Contracts;

public interface IAuthenticationService
{
    User Register(string firstName, string lastName, string username, string password, string email, UserRoleEnum roll);
    UserDto? Login(string username, string password);
}