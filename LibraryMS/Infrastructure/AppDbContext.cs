using LibraryMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryMS.Infrastructure;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=LibraryMS;User ID=sa; Password=123456;Trust Server Certificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Review>()
            .ToTable("Reviews") 
            .HasCheckConstraint("CK_Review_Rating", "Rating >= 1 AND Rating <= 5");
    }

    public DbSet<User> Users { get; set; }
    public DbSet<MemberProfile> MemberProfiles { get; set; }
    public DbSet<AdminProfile> AdminProfiles { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BorrowedBook> BorrowedBooks { get; set; }
    public DbSet<BookCategory> BookCategories { get; set; }
    public DbSet<Review> Reviews { get; set; }
}