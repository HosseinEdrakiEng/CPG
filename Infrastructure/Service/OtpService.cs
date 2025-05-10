using Application.Abstraction.IRepository;
using Application.Abstraction.IService;
using Application.Common;
using Application.Models;
using Domain.Entites;
using Helper;

namespace Infrastructure.Service
{
    public class OtpService : IOtpService
    {
        private readonly IOtpRepository _otpRepository;
        private readonly INotificationService _notificationService;

        public OtpService(IOtpRepository otpRepository
            , INotificationService notificationService)
        {
            _otpRepository = otpRepository;
            _notificationService = notificationService;
        }

        public async Task<BaseResponse<SendOtpResponseModel>> Send(string phoneNumnber, CancellationToken cancellationToken)
        {
            var result = new BaseResponse<SendOtpResponseModel>();
            var otpValue = Extention.GenerateRandomCode();

            var model = new Otp
            {
                Phonenumber = phoneNumnber,
                Value = otpValue,
            };
            await _otpRepository.Create(model, cancellationToken);

            var notify = await _notificationService.NotifyOtp(phoneNumnber, otpValue, cancellationToken);
            if (notify.HasError)
            {
                result.Error = notify.Error;
                return result;
            }

            result.Data = new SendOtpResponseModel
            {
                Token = model.Token
            };
            return result;
        }

        public async Task<BaseResponse<VerifyOtpResponseModel>> Verify(string phoneNumber, string token, string value, CancellationToken cancellationToken)
        {
            var result = new BaseResponse<VerifyOtpResponseModel>();

            var succ = await _otpRepository.FindOtp(phoneNumber, token, value, cancellationToken);
            if (!succ)
            {
                result.Error = CustomError.InvalidOtpCode;
                return result;
            }

            return result;
        }
    }
}
