using LibraryMS.Domain.Entities;

namespace LibraryMS.Domain.Contracts.Service_Contracts;

public interface IAuthenticationService
{
    User Register(string firstName, string lastName, string username, string password, string email);
    User? Login(string username, string password);
}