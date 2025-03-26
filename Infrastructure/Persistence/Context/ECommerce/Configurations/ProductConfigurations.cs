using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Context.ECommerce.Configurations
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(500);
            builder.Property(p => p.RowVersion).IsRowVersion();
            builder.HasMany(p => p.Rates).WithOne().HasForeignKey(r => r.ProductId);
            builder.ToTable("Products").HasQueryFilter(p => p.IsDeleted == false);
        }
    }
}
