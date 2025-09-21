using LibraryMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryMS.Infrastructure;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=LibraryMS;User ID=sa;Trust Server Certificate=True");
    }

    public DbSet<Member> Members { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BorrowedBook> BorrowedBooks { get; set; }
    public DbSet<BookCategory> BookCategories { get; set; }
}