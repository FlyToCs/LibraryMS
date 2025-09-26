using LibraryMS.Domain.Contracts.Repository_Contracts;
using LibraryMS.Domain.Entities;
using LibraryMS.Infrastructure.Persistence;

namespace LibraryMS.Infrastructure.Repositories;

public class EfBookCategoryRepository : IBookCategoryRepository
{

    public int Add(BookCategory bookCategory)
    {
        using var context = new AppDbContext();
        context.BookCategories.Add(bookCategory);
        context.SaveChanges();
        return bookCategory.Id;
    }

    public List<BookCategory> GetAll()
    {
        using var context = new AppDbContext();
        return context.BookCategories.ToList();
    }

    public BookCategory? GetById(int id)
    {
        using var context = new AppDbContext();
        return context.BookCategories.FirstOrDefault(x => x.Id == id);
    }

    public BookCategory? GetByName(string name)
    {
        using var context = new AppDbContext();
        return context.BookCategories.FirstOrDefault(x => x.Name == name);
    }

    public bool Delete(int id)
    {
        using var context = new AppDbContext();
        var bookCategory = context.BookCategories.FirstOrDefault(x => x.Id == id);
        if (bookCategory != null)
        {
            context.BookCategories.Remove(bookCategory);
            context.SaveChanges();
            return true;
        }
        return false;
    }

    public bool Update(BookCategory bookCategory)
    {
        using var context = new AppDbContext();
        var oldBookCategory = context.BookCategories.FirstOrDefault(x => x.Id == bookCategory.Id);
        if (oldBookCategory != null)
        {
            oldBookCategory.Name = bookCategory.Name;
            context.SaveChanges();
            return true;
        }
        return false;
    }

    public bool UpdateBookCategoryName(int id, string name)
    {
        using var context = new AppDbContext();
        var oldBookCategory = context.BookCategories.FirstOrDefault(x => x.Id == id);
        if (oldBookCategory != null)
        {
            oldBookCategory.Name = name;
            context.SaveChanges();
            return true;
        }
        return false;
    }
}