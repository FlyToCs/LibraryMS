using LibraryMS.Domain.Contracts.Repository_Contracts;
using LibraryMS.Domain.Entities;

namespace LibraryMS.Infrastructure.Repositories;

public class EfBookCategoryRepository : IBookCategoryRepository
{
    private readonly AppDbContext _context = new AppDbContext();
    public int Add(BookCategory bookCategory)
    {
        _context.BookCategories.Add(bookCategory);
        _context.SaveChanges();
        return bookCategory.Id;
    }

    public List<BookCategory> GetAll()
    {
        return _context.BookCategories.ToList();
    }

    public BookCategory? GetById(int id)
    {
        return _context.BookCategories.FirstOrDefault(x=>x.Id == id);

    }

    public BookCategory? GetByName(string name)
    {
        return _context.BookCategories.FirstOrDefault(x => x.Name == name);
    }

    public bool Delete(int id)
    {
        var bookCategory = _context.BookCategories.Find(id);
        if (bookCategory!=null)
        {
            _context.BookCategories.Remove(bookCategory);
            _context.SaveChanges();
            return true;
        }

        return false;
    }

    public bool Update(BookCategory bookCategory)
    {
        var oldBookCategory = _context.BookCategories.Find(bookCategory.Id);
        if (oldBookCategory != null)
        {
            oldBookCategory.Name = bookCategory.Name;
            oldBookCategory.Books = bookCategory.Books;
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public bool UpdateBookCategoryName(int id, string name)
    {
        var oldBookCategory = _context.BookCategories.Find(id);
        if (oldBookCategory != null)
        {
            oldBookCategory.Name = name;
            _context.SaveChanges();
            return true;
        }

        return false;
    }
}