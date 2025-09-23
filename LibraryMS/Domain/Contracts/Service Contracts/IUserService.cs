using LibraryMS.Domain.Entities;

namespace LibraryMS.Domain.Contracts.Service_Contracts;

public interface IUserService
{
    int Add(User user);
    User? GetById(int id);
    List<User> GetAll();
    void Update(User user);
    void Delete(int id);
}