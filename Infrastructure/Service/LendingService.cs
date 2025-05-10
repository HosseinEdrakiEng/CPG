using Application.Abstraction.IService;
using Application.Common;
using Application.Models;
using Helper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Infrastructure.Service
{
    public class LendingService : ILendingService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<LendingService> _logger;
        private readonly LendingConfig _config;

        public LendingService(IHttpClientFactory httpClientFactory
            , IOptions<LendingConfig> config
            , ILogger<LendingService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _config = config.Value;
            _logger = logger;
        }

        public async Task<BaseResponse<BalanceLoanLendigResponseModel>> BalanceLoan(BalanceLoanLendigRequestModel model, CancellationToken cancellationToken)
        {
            var result = new BaseResponse<BalanceLoanLendigResponseModel>();

            var headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" }
            };
            var apiResponse = await _httpClientFactory.ApiCall("Lending", model, HttpMethod.Get, $"{_config.GetBalanceUrl}/{model.Phonenumber}/{model.NationalCode}", headers, cancellationToken);

            _logger.LogInformation($"BalanceLoan log : '{apiResponse.SerializeAsJson()}'");

            if (!apiResponse.IsSuccessStatusCode)
            {
                result.Error = CustomError.BalanceLoanFail;
                return result;
            }

            result.Data = JsonSerializer.Deserialize<BalanceLoanLendigResponseModel>(apiResponse.Response);
            return result;
        }

        public async Task<BaseResponse<ConsumeLoanLendigResponseModel>> ConsumeLoan(ConsumeLoanLendigRequestModel model, CancellationToken cancellationToken)
        {
            var result = new BaseResponse<ConsumeLoanLendigResponseModel>();

            var headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" }
            };
            var apiResponse = await _httpClientFactory.ApiCall("Lending", model, HttpMethod.Put, _config.ConsumeLoanUrl, headers, cancellationToken);

            _logger.LogInformation($"ConsumeLoan log : '{apiResponse.SerializeAsJson()}'");

            if (!apiResponse.IsSuccessStatusCode)
            {
                result.Error = CustomError.ConsumeLoanFail;
                return result;
            }
            return result;
        }
    }
}
