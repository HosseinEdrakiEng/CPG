using Domain.Entites;

namespace Application.Abstraction.IRepository
{
    public interface IOtpRepository
    {
        Task Create(Otp model, CancellationToken cancellationToken);
        Task<bool> FindOtp(string phoneNumber, string token, string value, CancellationToken cancellationToken);
    }
}
