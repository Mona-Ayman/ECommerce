using Domain.Carts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Context.ECommerce.Configurations
{
    public class CartConfigurations : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> modelBuilder)
        {
            modelBuilder.ToTable("Carts");
            modelBuilder.HasMany(c => c.Items).WithOne().IsRequired();
            modelBuilder.Navigation(c => c.Items).AutoInclude();
            modelBuilder.HasIndex(c => c.UserId).IsUnique();
        }
    }
}
