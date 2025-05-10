using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configuration
{
    public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchases");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.ClientId).HasMaxLength(100);
            builder.Property(e => e.CreateTime);
            builder.Property(e => e.MerchantId);
            builder.Property(e => e.Phonenumber);
            builder.Property(e => e.TrackingCode).HasMaxLength(100).IsRequired(); ;
            builder.Property(e => e.NationalCode);
            builder.Property(e => e.OrderId).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Amount);
            builder.Property(e => e.Status).HasConversion<byte>().IsRequired();
            builder.Property(e => e.CallbackUrl).HasMaxLength(500).IsRequired();

            builder.HasMany(o => o.PurchaseDetails)
                   .WithOne(p => p.Purchase)
                   .HasForeignKey(p => p.PurchaseId);
        }
    }
}
