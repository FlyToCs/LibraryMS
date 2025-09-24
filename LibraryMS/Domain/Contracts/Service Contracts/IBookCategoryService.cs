using LibraryMS.Domain.Entities;

namespace LibraryMS.Domain.Contracts.Service_Contracts;

public interface IBookCategoryService
{
    int Add(BookCategory bookCategory);
    BookCategory GetById(int id);
    BookCategory GetByName(string name);
    void Update(BookCategory bookCategory);
    void DeleteById(int id);

}