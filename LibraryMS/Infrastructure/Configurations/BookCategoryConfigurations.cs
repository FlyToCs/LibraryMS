using LibraryMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryMS.Infrastructure.Configurations;

public class BookCategoryConfigurations : IEntityTypeConfiguration<BookCategory>
{
    public void Configure(EntityTypeBuilder<BookCategory> builder)
    {
        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();
    }
}