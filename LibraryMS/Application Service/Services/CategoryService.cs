using LibraryMS.Domain.Contracts.Service_Contracts;
using LibraryMS.Domain.Entities;
using LibraryMS.Infrastructure.Repositories;

namespace LibraryMS.Application_Service.Services;

public class CategoryService : IBookCategoryService
{
    private readonly EfBookCategoryRepository _bookCategoryRepository = new EfBookCategoryRepository();
    public int Add(string name)
    {
        var newCategory = new BookCategory(name);
        return _bookCategoryRepository.Add(newCategory);
    }

    public List<BookCategory> GetAll()
    {
        return _bookCategoryRepository.GetAll();
    }

    public BookCategory GetById(int id)
    {
        var bookCategory =  _bookCategoryRepository.GetById(id);
        if (bookCategory == null)
            throw new Exception("not found");
        return bookCategory;
    }

    public BookCategory GetByName(string name)
    {
        var bookCategory = _bookCategoryRepository.GetByName(name);
        if (bookCategory == null)
            throw new Exception("not found");
        return bookCategory;
    }

    public void Update(BookCategory bookCategory)
    {
        var check = _bookCategoryRepository.Update(bookCategory);
        if (check == false)
            throw new Exception("No book found with this id. Update failed");
    }

    public void DeleteById(int id)
    {
        var isDelete = _bookCategoryRepository.Delete(id);
        if (isDelete == false)
        {
            throw new Exception("No book found with this id. Delete failed");
        }
    }
}