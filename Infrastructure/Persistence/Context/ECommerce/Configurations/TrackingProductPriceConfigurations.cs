using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Context.ECommerce.Configurations
{
    internal class TrackingProductPriceConfigurations : IEntityTypeConfiguration<TrackingProductPrice>
    {
        public void Configure(EntityTypeBuilder<TrackingProductPrice> builder)
        {
            builder.ToTable("TrackingProductPrices");
        }
    }
}
