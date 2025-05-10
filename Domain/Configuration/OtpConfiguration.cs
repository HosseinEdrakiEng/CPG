using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configuration
{
    public class OtpConfiguration : IEntityTypeConfiguration<Otp>
    {
        public void Configure(EntityTypeBuilder<Otp> builder)
        {
            builder.ToTable("Otps");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Value).HasMaxLength(6).IsRequired();
            builder.Property(e => e.Token).HasMaxLength(100).IsRequired();
            builder.Property(e => e.CreateTime).IsRequired();
            builder.Property(e => e.ExpireTime).IsRequired();
            builder.Property(e => e.Phonenumber).HasMaxLength(20).IsRequired();
        }
    }
}
