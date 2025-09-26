using LibraryMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryMS.Infrastructure.Configurations;

public class BorrowedBookConfigurations : IEntityTypeConfiguration<BorrowedBook>
{
    public void Configure(EntityTypeBuilder<BorrowedBook> builder)
    {
        builder.HasOne(bb => bb.Book)
            .WithMany(b => b.BorrowedBooks)
            .HasForeignKey(bb => bb.BookId);

        builder.HasOne(bb => bb.User)
            .WithMany(u => u.BorrowedBooks)
            .HasForeignKey(bb => bb.UserId);

        builder.Property(bb => bb.BorrowDate)
            .HasDefaultValueSql("GETDATE()");
    }
}