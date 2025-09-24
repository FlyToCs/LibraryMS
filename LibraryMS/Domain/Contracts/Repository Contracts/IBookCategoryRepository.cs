using LibraryMS.Domain.Entities;

namespace LibraryMS.Domain.Contracts.Repository_Contracts;

public interface IBookCategoryRepository
{
    int Add(BookCategory bookCategory);
    List<BookCategory> GetAll();
    BookCategory? GetById(int id);
    BookCategory? GetByName(string name);
    bool Delete(int id);
    bool Update(BookCategory bookCategory);
    bool UpdateBookCategoryName(int id, string name);
}