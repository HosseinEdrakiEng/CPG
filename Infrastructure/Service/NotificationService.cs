using Application.Abstraction.IService;
using Application.Common;
using Application.Models;
using Helper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Service
{
    public class NotificationService : INotificationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<NotificationService> _logger;
        private readonly NotificationConfig _config;

        public NotificationService(IHttpClientFactory httpClientFactory
            , ILogger<NotificationService> logger
            , IOptions<NotificationConfig> config)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _config = config.Value;
        }

        public async Task<BaseResponse<NotifyOtpResponseModel>> NotifyOtp(string phoneNumnber, string otpValue, CancellationToken cancellationToken)
        {
            var result = new BaseResponse<NotifyOtpResponseModel>();

            var model = new
            {
                PhoneNumber = phoneNumnber,
                Text = otpValue,
                TemplateId = "100000",
                Parameters = new Dictionary<string, string> { { "CODE", otpValue } }
            };

            var headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/json"},
            };

            var apiResponse = await _httpClientFactory.ApiCall("Notification", model, HttpMethod.Post, _config.SendUrl, headers, cancellationToken);
            _logger.LogInformation($"NotifyOtp log : '{apiResponse.SerializeAsJson()}'");

            if (!apiResponse.IsSuccessStatusCode)
            {
                result.Error = CustomError.NotifyOtpFail;
                return result;
            }

            return result;
        }
    }
}
