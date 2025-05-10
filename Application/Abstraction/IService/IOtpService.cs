using Application.Models;
using Helper;

namespace Application.Abstraction.IService
{
    public interface IOtpService
    {
        Task<BaseResponse<SendOtpResponseModel>> Send(string phoneNumnber, CancellationToken cancellationToken);
        Task<BaseResponse<VerifyOtpResponseModel>> Verify(string phoneNumber, string token, string value, CancellationToken cancellationToken);
    }
}
