using Application.Abstraction.IService;
using Application.Common;
using Application.Models;
using Helper;

namespace Infrastructure.Service
{
    public class LoanService : ILoanService
    {
        private readonly ILendingService _lendingService;
        private readonly IPurchaseService _purchaseService;
        private readonly IPurchaseDetailService _purchaseDetailService;

        public LoanService(ILendingService lendingService
            , IPurchaseService purchaseService
            , IPurchaseDetailService purchaseDetailService)
        {
            _lendingService = lendingService;
            _purchaseService = purchaseService;
            _purchaseDetailService = purchaseDetailService;
        }

        public async Task<BaseResponse<BalanceLoanResponseModel>> Balance(BalanceLoanRequestModel request, CancellationToken cancellationToken)
        {
            var result = new BaseResponse<BalanceLoanResponseModel>();

            var model = new BalanceLoanLendigRequestModel
            {
                Phonenumber = request.Phonenumber,
                NationalCode = request.NationalCode,
            };
            var lendingResponse = await _lendingService.BalanceLoan(model, cancellationToken);
            if (lendingResponse.HasError)
            {
                result.Error = lendingResponse.Error;
                return result;
            }

            result.Data = new BalanceLoanResponseModel
            {
                Balance = lendingResponse.Data.Balance
            };
            return result;
        }

        public async Task<BaseResponse<ConsumeLoanResponseModel>> Consume(ConsumeLoanRequestModel request, CancellationToken cancellationToken)
        {
            var result = new BaseResponse<ConsumeLoanResponseModel>();

            var purchase = await _purchaseService.Inquiry(request.TrackingCode, cancellationToken);
            if (purchase.HasError)
            {
                result.Error = purchase.Error;
                return result;
            }

            var detail = purchase.Data?.Details?.Where(r => r.Id == request.PurchaseDetailId && r.Status == PurchaseDetailStatus.None)?.FirstOrDefault();
            if (detail is null)
            {
                result.Error = CustomError.PurchaseDetailNotFound;
                return result;
            }

            if (purchase.Data.NationalCode != request.NationalCode
                || purchase.Data.Phonenumber != request.Phonenumber)
            {
                result.Error = CustomError.InvalidConsumeRequest;
                return result;
            }

            PurchaseDetailStatus purchaseDetailStatus;
            var model = new ConsumeLoanLendigRequestModel
            {
                Phonenumber = request.Phonenumber,
                NationalCode = request.NationalCode,
                MerchantCode = purchase.Data.MerchantCode,
                Amount = detail.Amount
            };
            var lendingResponse = await _lendingService.ConsumeLoan(model, cancellationToken);
            if (lendingResponse.HasError)
            {
                purchaseDetailStatus = PurchaseDetailStatus.Fail;
                result.Error = lendingResponse.Error;
            }
            else
            {
                purchaseDetailStatus = PurchaseDetailStatus.Success;
            }

            await _purchaseDetailService.ChangeStatus(detail.Id, purchaseDetailStatus, cancellationToken);
            return result;
        }
    }
}