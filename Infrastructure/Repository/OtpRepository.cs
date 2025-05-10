using Application.Abstraction.IRepository;
using Domain.Entites;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class OtpRepository : IOtpRepository
    {
        private readonly CPGDbContext _context;

        public OtpRepository(CPGDbContext context)
        {
            _context = context;
        }

        public async Task Create(Otp model, CancellationToken cancellationToken)
        {
            _context.Otps.Add(model);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> FindOtp(string phoneNumber, string token, string value, CancellationToken cancellationToken)
        {
            return await _context.Otps.AnyAsync(r => r.Phonenumber == phoneNumber
            && r.Token == token
            && r.Value == value
            && r.ExpireTime >= DateTime.Now);
        }
    }
}
