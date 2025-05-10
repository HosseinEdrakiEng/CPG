using Domain.Entites;

namespace Application.Abstraction.IRepository
{
    public interface IPurchaseRepository
    {
        Task Create(Purchase model, CancellationToken cancellationToken);
        Task<Purchase> FindTrackingCode(string trackingCode, CancellationToken cancellationToken);
    }
}
