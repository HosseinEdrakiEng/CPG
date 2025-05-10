using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configuration
{
    public class MerchantConfiguration : IEntityTypeConfiguration<Merchant>
    {
        public void Configure(EntityTypeBuilder<Merchant> builder)
        {
            builder.ToTable("Merchants");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.UserId).HasMaxLength(100).IsRequired();

            builder.HasMany(o => o.MerchantWallets)
                   .WithOne(p => p.Merchant)
                   .HasForeignKey(p => p.MerchantId);

            builder.HasMany(o => o.Purchases)
                   .WithOne(p => p.Merchant)
                   .HasForeignKey(p => p.MerchantId);
        }
    }
}
