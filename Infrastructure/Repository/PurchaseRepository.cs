using Application.Abstraction.IRepository;
using Domain.Entites;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly CPGDbContext _context;

        public PurchaseRepository(CPGDbContext context)
        {
            _context = context;
        }

        public async Task Create(Purchase model, CancellationToken cancellationToken)
        {
            await _context.Purchases.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task<Purchase> FindTrackingCode(string trackingCode, CancellationToken cancellationToken)
        {
            return await _context.Purchases.Include(r => r.PurchaseDetails)?.Include(r => r.Merchant)?.Where(r => r.TrackingCode == trackingCode)?.AsSplitQuery().FirstOrDefaultAsync();
        }
    }
}
