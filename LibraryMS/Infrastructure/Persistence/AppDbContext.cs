using LibraryMS.Domain.Entities;
using LibraryMS.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LibraryMS.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configurationBuilder = new ConfigurationBuilder();
        var config = configurationBuilder.AddJsonFile("appsettings.json").Build();
        var configSection = config.GetSection("ConnectionStrings");
        var connectionString = configSection["DbConnection"];
        optionsBuilder.UseSqlServer(connectionString);

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfigurations());
        modelBuilder.ApplyConfiguration(new BookConfigurations());
        modelBuilder.ApplyConfiguration(new BookCategoryConfigurations());
        modelBuilder.ApplyConfiguration(new BorrowedBookConfigurations());
        modelBuilder.ApplyConfiguration(new ReviewConfigurations());
    }


    public DbSet<User> Users { get; set; }
    public DbSet<MemberProfile> MemberProfiles { get; set; }
    public DbSet<AdminProfile> AdminProfiles { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BorrowedBook> BorrowedBooks { get; set; }
    public DbSet<BookCategory> BookCategories { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<WishList> WishLists { get; set; }
}