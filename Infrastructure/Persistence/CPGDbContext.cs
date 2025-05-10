using Application;
using Domain.Configuration;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class CPGDbContext : DbContext, ICPGDbContext
    {
        public CPGDbContext()
        {

        }

        public CPGDbContext(DbContextOptions<CPGDbContext> options)
        : base(options)
        {

        }

        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<MerchantWallet> MerchantWallets { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public DbSet<Otp> Otps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MerchantConfiguration());
            modelBuilder.ApplyConfiguration(new MerchantWalletConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseDetailConfiguration());
            modelBuilder.ApplyConfiguration(new OtpConfiguration());
        }
    }
}
