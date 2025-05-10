using Application.Abstraction.IRepository;
using Application.Common;
using Domain.Entites;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class PurchaseDetailRepository : IPurchaseDetailRepository
    {
        private readonly CPGDbContext _context;

        public PurchaseDetailRepository(CPGDbContext context)
        {
            _context = context;
        }

        public async Task ChangeStatus(long detailId, PurchaseDetailStatus status, CancellationToken cancellationToken)
        {
            var model = await this.FindById(detailId, cancellationToken);
            if (model is not null)
            {
                model.Status = (byte)status;
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Create(PurchaseDetail model, CancellationToken cancellationToken)
        {
            _context.PurchaseDetails.Add(model);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<PurchaseDetail> FindById(long detailId, CancellationToken cancellationToken)
        {
            return await _context.PurchaseDetails?.Where(r => r.Id == detailId)?.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
