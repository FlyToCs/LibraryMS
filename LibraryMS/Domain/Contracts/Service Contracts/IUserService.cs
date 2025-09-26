using LibraryMS.Application_Service.DTOs;
using LibraryMS.Domain.Entities;

namespace LibraryMS.Domain.Contracts.Service_Contracts;

public interface IUserService
{
    int Add(User user);
    UserDto? GetById(int id);
    void Activate(int id);
    void Deactivate(int id);
    List<User> GetAll();
    List<UserDto> GetAllActive();
    List<UserDto> GetAllInActive();
    List<UserDto> GetUserHasPenaltyAmount();
    void Update(User user);
    void Delete(int id);
}