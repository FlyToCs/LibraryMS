using LibraryMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryMS.Infrastructure.Configurations;

public class ReviewConfigurations : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.Property(r => r.Comment)
            .HasMaxLength(500);

        builder.Property(r => r.Rating)
            .IsRequired();

        builder.HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId);

        builder.HasOne(r => r.Book)
            .WithMany(b => b.Reviews)
            .HasForeignKey(r => r.BookId);

        builder.Property(r => r.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        builder.HasCheckConstraint("CK_Review_Rating", "Rating >= 1 AND Rating <= 5");
    }
}