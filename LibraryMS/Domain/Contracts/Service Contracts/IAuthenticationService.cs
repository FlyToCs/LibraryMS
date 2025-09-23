using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Enums;

namespace LibraryMS.Domain.Contracts.Service_Contracts;

public interface IAuthenticationService
{
    User Register(string firstName, string lastName, string username, string password, string email, UserRoleEnum roll);
    User? Login(string username, string password);
}