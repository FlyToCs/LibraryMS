using LibraryMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryMS.Infrastructure.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {

        builder.Property(u => u.FirstName)
                .HasMaxLength(50)
                .IsRequired();

        builder.Property(u => u.LastName)
                .HasMaxLength(50)
                .IsRequired();

        builder.Property(u => u.Email)
                .HasMaxLength(100)
                .IsRequired();

        builder.Property(u => u.Username)
                .HasMaxLength(50)
                .IsRequired();

        builder.Property(u => u.Password)
                .HasMaxLength(200)
                .IsRequired();

        builder.HasMany(u => u.BorrowedBooks)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);

        builder.HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);

    }
}