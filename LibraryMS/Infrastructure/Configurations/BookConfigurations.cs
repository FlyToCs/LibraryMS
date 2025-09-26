using LibraryMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace LibraryMS.Infrastructure.Configurations;

public class BookConfigurations : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {


        builder.Property(b => b.Title)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(b => b.Description)
            .HasMaxLength(1000);

        builder.Property(b => b.Author)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasOne(b => b.BookCategory)
            .WithMany(c => c.Books)
            .HasForeignKey(b => b.BookCategoryId);

        builder.HasMany(b => b.BorrowedBooks)
            .WithOne(bb => bb.Book)
            .HasForeignKey(bb => bb.BookId);

        builder.HasMany(b => b.Reviews)
            .WithOne(r => r.Book)
            .HasForeignKey(r => r.BookId);

    }
}