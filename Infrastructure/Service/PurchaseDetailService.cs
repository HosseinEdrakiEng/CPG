using Application.Abstraction.IRepository;
using Application.Abstraction.IService;
using Application.Common;
using Application.Models;
using Azure.Core;
using Domain.Entites;
using Helper;

namespace Infrastructure.Service
{
    public class PurchaseDetailService : IPurchaseDetailService
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IPurchaseDetailRepository _purchaseDetailRepository;

        public PurchaseDetailService(IPurchaseService purchaseService
            , IPurchaseDetailRepository purchaseDetailRepository)
        {
            _purchaseService = purchaseService;
            _purchaseDetailRepository = purchaseDetailRepository;
        }

        public async Task<BaseResponse<ChangeStatusResponseModel>> ChangeStatus(long purchaseDetailId, PurchaseDetailStatus status, CancellationToken cancellationToken)
        {
            var result = new BaseResponse<ChangeStatusResponseModel>();

            var model = await _purchaseDetailRepository.FindById(purchaseDetailId, cancellationToken);
            if (model is null)
            {
                result.Error = CustomError.PurchaseDetailNotFound;
                return result;
            }

            await _purchaseDetailRepository.ChangeStatus(purchaseDetailId, status, cancellationToken);

            return result;
        }

        public async Task<BaseResponse<CreatePurchaseDetailResponseModel>> Create(CreatePurchaseDetailRequestModel request, CancellationToken cancellationToken)
        {
            var result = new BaseResponse<CreatePurchaseDetailResponseModel>();

            var purchase = await _purchaseService.Inquiry(request.TrackingCode, cancellationToken);
            if (purchase.HasError)
            {
                result.Error = purchase.Error;
                return result;
            }

            var detail = purchase.Data?.Details?.Where(r => r.Status == PurchaseDetailStatus.Success)?.Sum(r => r.Amount) ?? 0;
            if (detail > request.Amount)
            {
                result.Error = CustomError.InvalidPurchaseAmount;
                return result;
            }

            var model = new PurchaseDetail
            {
                Amount = request.Amount,
                PurchaseId = purchase.Data.Id,
                Status = (byte)PurchaseDetailStatus.None,
                Type = (byte)request.Type,
                UserId = request.UserId
            };
            await _purchaseDetailRepository.Create(model, cancellationToken);
            result.Data = new CreatePurchaseDetailResponseModel
            {
                PurchaseDetailId = model.Id
            };
            return result;
        }
    }
}
