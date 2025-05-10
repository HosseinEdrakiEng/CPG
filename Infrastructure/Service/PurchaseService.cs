using Application.Abstraction.IRepository;
using Application.Abstraction.IService;
using Application.Common;
using Application.Models;
using Domain.Entites;
using Helper;
using Mapster;

namespace Infrastructure.Service
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseService(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public async Task<BaseResponse<CreatePurchaseResponseModel>> Create(CreatePurchaseRequestModel request, CancellationToken cancellationToken)
        {
            var result = new BaseResponse<CreatePurchaseResponseModel>();

            var model = request.Adapt<Purchase>();
            await _purchaseRepository.Create(model, cancellationToken);

            result.Data = new CreatePurchaseResponseModel
            {
                TrackingCode = model.TrackingCode,
            };
            return result;
        }

        public async Task<BaseResponse<InquiryPurchaseResponseModel>> Inquiry(string trackingCode, CancellationToken cancellationToken)
        {
            var result = new BaseResponse<InquiryPurchaseResponseModel>();

            var purchase = await _purchaseRepository.FindTrackingCode(trackingCode, cancellationToken);
            if (purchase is null)
            {
                result.Error = CustomError.InvalidPurchase;
                return result;
            }


            result.Data = purchase.Adapt<InquiryPurchaseResponseModel>();
            result.Data.MerchantCode = purchase.Merchant?.MerchantCode;
            result.Data.Details = purchase.PurchaseDetails.Adapt<List<InquiryPurchaseDetailResponseModel>>();
            return result;
        }
    }
}
