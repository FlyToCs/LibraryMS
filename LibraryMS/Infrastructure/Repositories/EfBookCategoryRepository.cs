using LibraryMS.Domain.Contracts.Repository_Contracts;
using LibraryMS.Domain.Entities;

namespace LibraryMS.Infrastructure.Repositories;

public class EfBookCategoryRepository : IBookCategoryRepository
{

    public int Add(BookCategory bookCategory)
    {
        using (var _context = new AppDbContext())
        {
            _context.BookCategories.Add(bookCategory);
            _context.SaveChanges();
            return bookCategory.Id;
        }
    }

    public List<BookCategory> GetAll()
    {
        using (var _context = new AppDbContext())
        {
            return _context.BookCategories.ToList();
        }
    }

    public BookCategory? GetById(int id)
    {
        using (var _context = new AppDbContext())
        {
            return _context.BookCategories.FirstOrDefault(x => x.Id == id);
        }
    }

    public BookCategory? GetByName(string name)
    {
        using (var _context = new AppDbContext())
        {
            return _context.BookCategories.FirstOrDefault(x => x.Name == name);
        }
    }

    public bool Delete(int id)
    {
        using (var _context = new AppDbContext())
        {
            var bookCategory = _context.BookCategories.Find(id);
            if (bookCategory != null)
            {
                _context.BookCategories.Remove(bookCategory);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }

    public bool Update(BookCategory bookCategory)
    {
        using (var _context = new AppDbContext())
        {
            var oldBookCategory = _context.BookCategories.Find(bookCategory.Id);
            if (oldBookCategory != null)
            {
                oldBookCategory.Name = bookCategory.Name;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }

    public bool UpdateBookCategoryName(int id, string name)
    {
        using (var _context = new AppDbContext())
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
}