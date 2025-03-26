using Domain.ProductRates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Context.ECommerce.Configurations
{
    public class ProductRateConfigurations : IEntityTypeConfiguration<ProductRate>
    {
        public void Configure(EntityTypeBuilder<ProductRate> builder)
        {
            builder.Property(p => p.Rate).IsRequired();
            builder.Property(p => p.UserId).IsRequired();
            builder.HasIndex(p => new { p.UserId, p.ProductId }).IsUnique();
            builder.ToTable("ProductRates");
        }
    }
}
