using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configuration
{
    public class MerchantWalletConfiguration : IEntityTypeConfiguration<MerchantWallet>
    {
        public void Configure(EntityTypeBuilder<MerchantWallet> builder)
        {
            builder.ToTable("MerchantWallets");
            builder.Property(r => r.MerchantId);
            builder.Property(e => e.WalletId).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Type).IsRequired();
        }
    }
}
