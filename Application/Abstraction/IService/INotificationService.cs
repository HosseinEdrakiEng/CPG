
using Application.Models;
using Helper;

namespace Application.Abstraction.IService
{
    public interface INotificationService
    {
        Task<BaseResponse<NotifyOtpResponseModel>> NotifyOtp(string phoneNumnber, string otpValue, CancellationToken cancellationToken);
    }
}
