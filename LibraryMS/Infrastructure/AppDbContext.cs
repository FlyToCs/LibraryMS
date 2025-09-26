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
        base.OnModelCreating(modelBuilder);

        // User
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(u => u.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(u => u.LastName)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(u => u.Email)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(u => u.Username)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(u => u.Password)
                .HasMaxLength(200) 
                .IsRequired();

            entity.HasMany(u => u.BorrowedBooks)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);

            entity.HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);
        });

        // Book
        modelBuilder.Entity<Book>(entity =>
        {
            entity.Property(b => b.Title)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(b => b.Description)
                .HasMaxLength(1000);

            entity.Property(b => b.Author)
                .HasMaxLength(100)
                .IsRequired();

            entity.HasOne(b => b.BookCategory)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.BookCategoryId);

            entity.HasMany(b => b.BorrowedBooks)
                .WithOne(bb => bb.Book)
                .HasForeignKey(bb => bb.BookId);

            entity.HasMany(b => b.Reviews)
                .WithOne(r => r.Book)
                .HasForeignKey(r => r.BookId);
        });

        // BookCategory
        modelBuilder.Entity<BookCategory>(entity =>
        {
            entity.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();
        });

        // BorrowedBook
        modelBuilder.Entity<BorrowedBook>(entity =>
        {
            entity.HasOne(bb => bb.Book)
                .WithMany(b => b.BorrowedBooks)
                .HasForeignKey(bb => bb.BookId);

            entity.HasOne(bb => bb.User)
                .WithMany(u => u.BorrowedBooks)
                .HasForeignKey(bb => bb.UserId);

            entity.Property(bb => bb.BorrowDate)
                .HasDefaultValueSql("GETDATE()"); 
        });

        // Review
        modelBuilder.Entity<Review>(entity =>
        {
            entity.Property(r => r.Comment)
                .HasMaxLength(500);

            entity.Property(r => r.Rating)
                .IsRequired();

            entity.HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId);

            entity.HasOne(r => r.Book)
                .WithMany(b => b.Reviews)
                .HasForeignKey(r => r.BookId);

            entity.Property(r => r.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            entity.HasCheckConstraint("CK_Review_Rating", "Rating >= 1 AND Rating <= 5");
        });
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