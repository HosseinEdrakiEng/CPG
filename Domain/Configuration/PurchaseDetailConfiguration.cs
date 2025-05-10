using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configuration
{
    public class PurchaseDetailConfiguration : IEntityTypeConfiguration<PurchaseDetail>
    {
        public void Configure(EntityTypeBuilder<PurchaseDetail> builder)
        {
            builder.ToTable("PurchaseDetails");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Amount).IsRequired();
            builder.Property(e => e.UserId).HasMaxLength(50).IsRequired();
            builder.Property(e => e.Status).HasConversion<byte>().IsRequired();
            builder.Property(e => e.Type).HasConversion<byte>().IsRequired();
        }
    }
}
