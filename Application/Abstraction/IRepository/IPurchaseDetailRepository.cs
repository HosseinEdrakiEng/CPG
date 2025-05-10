using Application.Common;
using Domain.Entites;

namespace Application.Abstraction.IRepository
{
    public interface IPurchaseDetailRepository
    {
        Task ChangeStatus(long detailId, PurchaseDetailStatus status, CancellationToken cancellationToken);
        Task Create(PurchaseDetail model, CancellationToken cancellationToken);
        Task<PurchaseDetail> FindById(long detailId, CancellationToken cancellationToken);
    }
}
