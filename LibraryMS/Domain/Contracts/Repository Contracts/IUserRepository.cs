using LibraryMS.Domain.Entities;

namespace LibraryMS.Domain.Contracts.Repository_Contracts;

public interface IUserRepository
{
    int Add(User user);
    User? GetById(int id);
    User? GetByUserName(string userName);
    List<User> GetAll();
    List<User> GetAllActive();
    List<User> GetAllInActive();
    void Update(User user);
    void Delete(int id);
}