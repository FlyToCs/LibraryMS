using LibraryMS.Domain.Entities;

namespace LibraryMS.Domain.Contracts.Service_Contracts;

public interface IUserService
{
    int Add(User user);
    User? GetById(int id);
    void Activate(int id);
    void Deactivate(int id);
    List<User> GetAll();
    List<User> GetAllActive();
    List<User> GetAllInActive();
    void Update(User user);
    void Delete(int id);
}