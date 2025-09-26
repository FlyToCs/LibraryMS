using LibraryMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryMS.Infrastructure.Configurations;

public class WishListConfigurations : IEntityTypeConfiguration<WishList>
{
    public void Configure(EntityTypeBuilder<WishList> builder)
    {
        throw new NotImplementedException();
    }
}